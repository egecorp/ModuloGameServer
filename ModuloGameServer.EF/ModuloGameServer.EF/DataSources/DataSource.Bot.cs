using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ModuloGameServer.Models
{

    public class DataSourceBot : BaseDataSource, IDataSourceBot
    {

        public DataSourceBot(ModuloGameDBContext context)
            :base(context)
        {
            
        }


        public async Task<Bot> GetBot(Guid botId, CancellationToken cancellationToken)
        {
            return await context.Set<Bot>().FirstOrDefaultAsync(x => x.BotId == botId, cancellationToken);
        }

        public async Task<List<Bot>> FindBotsByNicName(string nicName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(nicName)) return new List<Bot>();
            return await context.Set<Bot>().Where(x => x.NicName.ToLower().Contains(nicName.ToLower())).ToListAsync(cancellationToken);
        }


        /// <summary>
        /// Добавляет бота в базу
        /// </summary>
        public async Task<bool> AddBot(Bot newBot, CancellationToken cancellationToken)
        {
            await InTransaction(async () =>
            {
                try
                {
                    string nicName = newBot.NicName.ToLower().Trim();

                    bool existsBot = await context.Set<Bot>()
                        .AnyAsync(x => (x.NicName.ToLower() == nicName), cancellationToken);

                    if (existsBot)
                    {
                        throw new Exception("Bot with same NicName is already exists");
                    }

                    User newUser = new User()
                    {
                        NicName = newBot.NicName,
                        IsBot = true,
                        DynamicUserInfo = new DynamicUserInfo() { }
                    };








                    await context.Set<User>().AddAsync(newUser, cancellationToken);
                    await context.SaveChangesAsync(cancellationToken);

                    if (newUser.Id > 0)
                    {
                        //newUser.DynamicUserInfo.UserId = newUser.Id;
                        //await context.Set<DynamicUserInfo>().AddAsync(newUser.DynamicUserInfo, cancellationToken);

                        newBot.UserId = newUser.Id;
                        await context.Set<Bot>().AddAsync(newBot, cancellationToken);

                        await context.SaveChangesAsync(cancellationToken);
                    }

                    await context.SaveChangesAsync(cancellationToken);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }, cancellationToken);


            return await Task.FromResult<bool>(true);
        }

        /// <summary>
        /// Изменяет бота в базе
        /// </summary>
        public async Task ChangeBot(Bot existsBot, CancellationToken cancellationToken)
        {
            context.Set<Bot>().Update(existsBot);
            await context.SaveChangesAsync(cancellationToken);
        }

    }
}
