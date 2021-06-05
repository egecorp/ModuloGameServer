using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ModuloGameServer.Models
{

    public class DataSourceUser : IDataSourceUser
    {
        private ModuloGameDBContext context;

        public const int MAX_RECENT_GAME_COUNT = 10;
        public DataSourceUser(ModuloGameDBContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Ищет пользователя по Ud
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<User> GetUser(int Id, CancellationToken cancellationToken)
        {
            return await context.Set<User>().FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }


        /// <summary>
        /// Ищет пользователя по его email
        /// </summary>
        public async Task<User> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            string emailToLower = email.ToLower();
            return await context.Set<User>().FirstOrDefaultAsync(x => (x.EMail ?? "") == email, cancellationToken);
        }


        /// <summary>
        /// Ищет пользователя по его нику
        /// </summary>
        public async Task<List<User>> FindUsersByNic(string nicName, CancellationToken cancellationToken)
        {
            string nicNameToLower = (nicName ?? "").ToLower();
            return await context.Set<User>().Where(x=> (x.NicName ?? "").ToLower().Contains(nicNameToLower)).ToListAsync(cancellationToken);
        }


        /// <summary>
        /// Добавляет пользователя в базу
        /// </summary>
        public async Task AddUser(User newUser, CancellationToken cancellationToken)
        {
            if (newUser.DynamicUserInfo == null)
            {
                newUser.DynamicUserInfo = new DynamicUserInfo() { CommonRating = DynamicUserInfo.COMMON_RATING_DEFAULT };
            }
            await context.Set<User>().AddAsync(newUser, cancellationToken);
            if (newUser.Id > 0)
            {
                newUser.DynamicUserInfo.UserId = newUser.Id;
                await context.Set<DynamicUserInfo>().AddAsync(newUser.DynamicUserInfo, cancellationToken);
            }

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Изменяет пользователя в базе
        /// </summary>
        public async Task ChangeUser(User newUser, CancellationToken cancellationToken)
        {
            context.Set<User>().Update(newUser);
            await context.SaveChangesAsync(cancellationToken);
        }


        public async Task<User> GetUserInfo(int UserId, CancellationToken cancellationToken)
        {
            User u = await context.Set<User>().Include(x => x.DynamicUserInfo).FirstOrDefaultAsync(x => x.Id == UserId, cancellationToken);

            if (u.DynamicUserInfo != null)
            {

                u.DynamicUserInfo.RecentGameList = await context
                                                        .Set<Game>()
                                                        .Include(x => x.User1)
                                                        .Include(x => x.User2)
                                                        .Where(x => ((x.User1Id == UserId) || (x.User2Id == UserId)) && (x.IsCancel || x.IsFinish || x.IsTimeout || x.IsDeclined || x.IsGiveUp))
                                                        .OrderBy(x => x.StartStamp)
                                                        .Take(MAX_RECENT_GAME_COUNT)
                                                        .ToListAsync(cancellationToken);


                u.DynamicUserInfo.ActiveGameList = await context
                                                        .Set<Game>()
                                                        .Include(x => x.User1)
                                                        .Include(x => x.User2)
                                                        .Where(x => ((x.User1Id == UserId) || (x.User2Id == UserId)) && !(x.IsCancel || x.IsFinish || x.IsTimeout || x.IsDeclined || x.IsGiveUp))
                                                        .OrderBy(x => x.StartStamp)
                                                        .ToListAsync(cancellationToken);
                
                List<int> gameIds = u.DynamicUserInfo.ActiveGameList.Select(x => x.Id).ToList();

                List<GameRound> gameRounds = await context.Set<GameRound>().Where(x => gameIds.Contains((x.GameId)))
                    .ToListAsync(cancellationToken);

                foreach (Game g in u.DynamicUserInfo.ActiveGameList)
                {
                    g.Rounds = gameRounds.Where(x => x.GameId == g.Id).ToList();
                }

            }


            return u;
        }


        public async Task<User> UpdateUserInfo(DynamicUserInfo dynamicUserInfo, CancellationToken cancellationToken)
        {
            
            context.Set<DynamicUserInfo>().Update(dynamicUserInfo);
            await context.SaveChangesAsync(cancellationToken);

            return await GetUser(dynamicUserInfo.UserId, cancellationToken);
        }


        public async Task<List<User>> GetCompetitors(int userId, CancellationToken cancellationToken)
        {
            List<User> users = await context
                .Set<Game>()
                .Where(x => (x.User1Id == userId) || (x.User2Id == userId))
                .OrderByDescending(x => x.StartStamp)
                .Select(x => (x.User1Id == userId) ? x.User2Id : x.User1Id)
                .Distinct()
                .Join(context.Set<User>(),
                    x => x.Value,
                    z => z.Id,
                    (x, z) => z)
                .ToListAsync(cancellationToken);


            return users;
        }


    }
}
