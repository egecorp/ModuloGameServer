using System.Threading;
using System.Threading.Tasks;

namespace ModuloGameServer.Models
{

    public interface IDataSourceUser
    {

        /// <summary>
        /// Добавляет пользователя в базу
        /// </summary>
        void AddUser(User newUser, CancellationToken cancellationToken);


        /// <summary>
        /// Изменяет пользователя в базе
        /// </summary>
        void ChangeUser(User newUser, CancellationToken cancellationToken);

        /// <summary>
        /// Ищет пользователя по его Id
        /// </summary>
        Task<User> GetUser(int Id, CancellationToken cancellationToken);

        /// <summary>
        /// Ищет пользователя по его email
        /// </summary>
        Task<User> GetUserByEmail(string email, CancellationToken cancellationToken);

    }
}
