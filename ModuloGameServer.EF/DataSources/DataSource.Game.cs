using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ModuloGameServer.Models
{

    public class DataSourceGame : BaseDataSource, IDataSourceGame
    {
        public DataSourceGame(ModuloGameDBContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Ищет пользователя по Id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Game> GetGame(int Id, CancellationToken cancellationToken)
        {
            Game game = await context.Set<Game>().Include(x => x.User1).Include(x => x.User1.DynamicUserInfo)
                .Include(x => x.User2).Include(x => x.User2.DynamicUserInfo)
                .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
            return game;
        }


        /// <summary>
        /// Добавляет игру в базу
        /// </summary>
        public async Task AddGame(Game newGame, CancellationToken cancellationToken)
        {
            await InTransaction(async () =>
                {
                    try
                    {
                        bool existsGameIsExists = await context.Set<Game>()
                                .AnyAsync(x =>
                                (
                                    (x.User1Id == newGame.User1Id) && (x.User2Id == newGame.User2Id) ||
                                    (x.User1Id == newGame.User2Id) && (x.User2Id == newGame.User1Id)
                                )
                                && !(x.IsCancel || x.IsFinish || x.IsTimeout || x.IsDeclined || x.IsGiveUp)
                            );

                        if (existsGameIsExists)
                        {
                            // TODO продумать как выходить отсюда
                            throw new Exception("Game with this users is already exists");
                        }

                        newGame.UpdateStatus();
                        await context.Set<Game>().AddAsync(newGame, cancellationToken);
                        await context.SaveChangesAsync(cancellationToken);
                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return false;
                    }
                }, cancellationToken);
        }

        /// <summary>
        /// Изменяет игру в базе
        /// </summary>
        public async Task ChangeGame(Game newGame, CancellationToken cancellationToken)
        {
            newGame.UpdateStatus();
            context.Set<Game>().Update(newGame);
            await context.SaveChangesAsync(cancellationToken);
        }


        public async Task UpdateGameStatus(Game game, CancellationToken cancellationToken)
        {
            await InTransaction(async () =>
            {
                try
                {
                    game.UpdateStatus();
                    await context.SaveChangesAsync(cancellationToken);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }, cancellationToken);
        }

        public async Task PlayRound(Game game, CancellationToken cancellationToken)
        {
            await InTransaction(async () =>
            {
                try
                {
                    await context.SaveChangesAsync(cancellationToken);
                    game.UpdateStatus();
                    await context.SaveChangesAsync(cancellationToken);

                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }, cancellationToken);

        }

        public async Task<List<Game>> GetBotsGames(CancellationToken cancellationToken)
        {
            List<Game> games = await context.Set<Game>()
                .Where(x => x.IsUser2Bot
                            && !(x.IsCancel || x.IsFinish || x.IsTimeout || x.IsDeclined || x.IsGiveUp))
                .ToListAsync(cancellationToken);

            return games;
        }

        /// <summary>
        /// Ищет неначатые анонимные игры 
        /// </summary>
        public async Task<List<Game>> GetRandomGames(CancellationToken cancellationToken)
        {
            List<Game> games = await context.Set<Game>()
                .Where(x => (x.IsCancel == false) && !x.User2Id.HasValue)
                .ToListAsync(cancellationToken);

            return games;
        }

        public async Task<GamesAggregates> GetGamesAggregates(int mainUserId, int? competitorUserId,
            CancellationToken cancellationToken)
        {
            var gamesAsUser1 = context.Set<Game>()
                .Where(x => (x.User1Id == mainUserId)
                            && (!competitorUserId.HasValue || (competitorUserId == x.User2Id)));

            var gamesAsUser2 = context.Set<Game>()
                .Where(x => (x.User2Id == mainUserId)
                            && (!competitorUserId.HasValue || (competitorUserId == x.User1Id)));

            // TODO подумать, включать ли сюда недоигранные прерванные игры
            int winCount = await gamesAsUser1.Where(x => x.Status == GAME_STATUS.GAME_FINISH_USER1_WIN)
                               .CountAsync(cancellationToken) +
                           await gamesAsUser2.Where(x => x.Status == GAME_STATUS.GAME_FINISH_USER2_WIN)
                                .CountAsync(cancellationToken);

            int loseCount = await gamesAsUser1.Where(x => x.Status == GAME_STATUS.GAME_FINISH_USER2_WIN)
                                .CountAsync(cancellationToken) +
                            await gamesAsUser2.Where(x => x.Status == GAME_STATUS.GAME_FINISH_USER1_WIN)
                                .CountAsync(cancellationToken);

            int drawCount = await gamesAsUser1.Where(x => x.Status == GAME_STATUS.GAME_FINISH_USER2_DRAW)
                                .CountAsync(cancellationToken) +
                            await gamesAsUser2.Where(x => x.Status == GAME_STATUS.GAME_FINISH_USER2_DRAW)
                                .CountAsync(cancellationToken);

            return new GamesAggregates { WinCount = winCount, LoseCount = loseCount, DrawCount = drawCount };
        }

        // TODO перенести в нужное место
        public const int MAX_GAMES_PER_REQUEST = 10;

        public async Task<(List<Game>, int)> GetGamesList(int mainUserId, int competitorUserId, int page,
            CancellationToken cancellationToken)
        {
            var gamesAsUser1 = context.Set<Game>()
                .Where(x => (x.User1Id == mainUserId) && (competitorUserId == x.User2Id));

            var gamesAsUser2 = context.Set<Game>()
                .Where(x => (x.User2Id == mainUserId) && (competitorUserId == x.User1Id));

            int globalCount = await gamesAsUser1.CountAsync(cancellationToken) + await gamesAsUser2.CountAsync(cancellationToken);

            List<Game> result;
            if (page * MAX_GAMES_PER_REQUEST > globalCount)
            {
                result = new List<Game>();
            }
            else
            {
                result = await gamesAsUser1.Union(gamesAsUser2)
                    .OrderBy(x => x.StartStamp).Skip(page * MAX_GAMES_PER_REQUEST).Take(MAX_GAMES_PER_REQUEST)
                    .ToListAsync(cancellationToken);

            }

            return (result, globalCount);
        }            

    }
}
