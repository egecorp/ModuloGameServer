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
        public async Task<Game> GetGame(int Id, CancellationToken cancellationToken)
        {
            return await context.Set<Game>().FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
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
                                && !(x.IsCancel || x.IsFinish || x.IsTimeout)
                            );

                        if (existsGameIsExists)
                        {
                            // TODO продумать как выходить отсюда
                            throw new Exception("Game with this users is already exists");
                        }

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
            context.Set<Game>().Update(newGame);
            await context.SaveChangesAsync(cancellationToken);
        }


    }
}
