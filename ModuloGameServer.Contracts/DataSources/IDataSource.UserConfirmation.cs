using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ModuloGameServer.Models
{

    public interface IDataSourceUserConfirmation
    {

        /// <summary>
        /// Добавляет подтверждение в базу
        /// </summary>
        Task AddUserConfirmation(UserConfirmation newUserConfirmation, CancellationToken cancellationToken);


        /// <summary>
        /// Изменить подтверждение в базе
        /// </summary>
        Task ChangeUserConfirmation(UserConfirmation newUserConfirmation, CancellationToken cancellationToken);


        /// <summary>
        /// Ищет подтверждение по его Id
        /// </summary>
        Task<UserConfirmation> GetUserConfirmation(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Ищет подтверждение по его коду ссылки
        /// </summary>
        Task<UserConfirmation> GetUserConfirmationByLinkCode(string code, CancellationToken cancellationToken);



    }
}
