using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        /// Ищет пользователя по Ud
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Game> GetGame(int Id, bool withRounds, CancellationToken cancellationToken)
        {
            Game game = await context.Set<Game>().FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
            if ((game != null) && withRounds)
            {
                game.Rounds = await context.Set<GameRound>().Where(x => x.GameId == game.Id)
                    .ToListAsync(cancellationToken);
            }
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
                    catch(Exception e)
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
                    game.Rounds = await context.Set<GameRound>().Where(x => x.GameId == game.Id).ToListAsync(cancellationToken);
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
        public async Task PlayRound(Game game, GameRound newGameRound, CancellationToken cancellationToken)
        {
            await InTransaction(async () =>
            {
                try
                {
                    await context.Set<GameRound>().AddAsync(newGameRound);
                    await context.SaveChangesAsync(cancellationToken);
                    game.Rounds = await context.Set<GameRound>().Where(x => x.GameId == game.Id).ToListAsync(cancellationToken);
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
    }
}
