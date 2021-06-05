using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ModuloGameServer.Models
{

    public interface IDataSourceBot
    {


        /// <summary>
        /// Добавляет бота в базу и создаёт связанного пользователя
        /// </summary>
        Task<bool> AddBot(Bot newBot, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает бота по его Id
        /// </summary>
        Task<Bot> GetBot(Guid botId, CancellationToken cancellationToken);

        /// <summary>
        /// Ищет ботов по нику
        /// </summary>
        Task<List<Bot>> FindBotsByNicName(string nicName, CancellationToken cancellationToken);

        /// <summary>
        /// Изменяет бота в базе
        /// </summary>
        Task ChangeBot(Bot existsBot, CancellationToken cancellationToken);

    }
}
