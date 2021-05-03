using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ModuloGameServer.Models
{

    public interface IDataSourceGame
    {

        /// <summary>
        /// Ищет пользователя по Ud
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Game> GetGame(int Id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет игру в базу
        /// </summary>
        Task AddGame(Game newGame, CancellationToken cancellationToken);

        /// <summary>
        /// Изменяет игру в базе
        /// </summary>
        Task ChangeGame(Game newGame, CancellationToken cancellationToken);



    }
}
