using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ModuloGameServer.Models
{

    public interface IDataSourceUser
    {

        /// <summary>
        /// Добавляет пользователя в базу
        /// </summary>
        Task AddUser(User newUser, CancellationToken cancellationToken);


        /// <summary>
        /// Изменяет пользователя в базе
        /// </summary>
        Task ChangeUser(User newUser, CancellationToken cancellationToken);

        /// <summary>
        /// Ищет пользователя по его Id
        /// </summary>
        Task<User> GetUser(int Id, CancellationToken cancellationToken);

        /// <summary>
        /// Ищет пользователя по его email
        /// </summary>
        Task<User> GetUserByEmail(string email, CancellationToken cancellationToken);


        /// <summary>
        /// Ищет пользователя по его нику
        /// </summary>
        Task<List<User>> FindUsersByNic(string email, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает пользователя и динамическую информацию о нём
        /// </summary>
        Task<User> GetUserInfo(int UserId, CancellationToken cancellationToken);

        /// <summary>
        /// Обновляет динамическую информацию о пользователе и возвращает обновлённого пользователя
        /// </summary>
        Task<User> UpdateUserInfo(DynamicUserInfo dynamicUserInfo, CancellationToken cancellationToken);


        /// <summary>
        /// Ищет всех пользователей, с кем играл UserId
        /// </summary>
        Task<List<User>> GetCompetitors(int userId, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает позицию пользователя в мировом рейтинге
        /// </summary>
        Task<int> GetWorldPosition(int userId, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает топ usersCount пользователей
        /// </summary>
        Task<List<User>> GetTop(int usersCount, CancellationToken cancellationToken);
    }
}
