using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ModuloGameServer.Models
{

    public class DataSourceUser : IDataSourceUser
    {
        private ModuloGameDBContext context;

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
            return await context.Set<User>().FirstOrDefaultAsync(x => x.EMail == email, cancellationToken);
        }


        /// <summary>
        /// Добавляет пользователя в базу
        /// </summary>
        public async void AddUser(User newUser, CancellationToken cancellationToken)
        {
            await context.Set<User>().AddAsync(newUser, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Изменяет пользователя в базе
        /// </summary>
        public async void ChangeUser(User newUser, CancellationToken cancellationToken)
        {
            context.Set<User>().Update(newUser);
            await context.SaveChangesAsync(cancellationToken);
        }


    }
}
