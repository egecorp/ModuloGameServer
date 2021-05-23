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
        Task<Game> GetGame(int id, bool withRounds, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет игру в базу
        /// </summary>
        Task AddGame(Game newGame, CancellationToken cancellationToken);

        /// <summary>
        /// Изменяет игру в базе
        /// </summary>
        Task ChangeGame(Game newGame, CancellationToken cancellationToken);

        /// <summary>
        /// Обновляет статус игры без изменений
        /// </summary>
        Task UpdateGameStatus(Game game, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет сыгранный раунд одного из игроков в базу
        /// </summary>
        Task PlayRound(Game game, GameRound newGameRound, CancellationToken cancellationToken);

    }
}
