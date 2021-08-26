using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ModuloGameServer.Models
{

    public interface IDataSourceGame
    {

        /// <summary>
        /// Ищет игру по Ud
        /// </summary>
        Task<Game> GetGame(int id, CancellationToken cancellationToken);

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
        Task PlayRound(Game game, CancellationToken cancellationToken);

        /// <summary>
        /// Ищет активные игры с ботом
        /// </summary>
        Task<List<Game>> GetBotsGames(CancellationToken cancellationToken);

        /// <summary>
        /// Выдаёт агрегирующий результат по играм
        /// </summary>
        /// <param name="mainUserId">Игрок</param>
        /// <param name="competitorUserId">Соперник, null если нужен результат по всем играм</param>
        Task<GamesAggregates> GetGamesAggregates(int mainUserId, int? competitorUserId,
            CancellationToken cancellationToken);

        /// <summary>
        /// Выдаёт список игр между двумя пользователям с пагинацией и общее количество игр
        /// </summary>
        Task<(List<Game>, int)> GetGamesList(int mainUserId, int competitorUserId, int page, CancellationToken cancellationToken);
    }
}
