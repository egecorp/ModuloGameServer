using ModuloGameServer.Models;
using System;

namespace ModuloGameServer.Request.Bot
{
    /// <summary>
    /// Один запрос от сервера к боту
    /// </summary>
    public class RequestBotCommand
    {
        /// <summary>
        /// Токен бота
        /// </summary>
        public string BotToken { set; get; }

        /// <summary>
        /// Команда
        /// </summary>
        public BotCommand Command { set; get; }

        /// <summary>
        /// Токен доступа к боту
        /// </summary>
        public string BotRequestToken { set; get; }

        /// <summary>
        /// Адрес конечной точки бота
        /// </summary>
        public RequestBotGame Game { set; get; }

        public RequestBotCommand()
        {

        }

    }
}
