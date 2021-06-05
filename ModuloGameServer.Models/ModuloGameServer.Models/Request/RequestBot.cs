using System;

namespace ModuloGameServer.Request
{
    /// <summary>
    /// Один бот
    /// </summary>
    public class RequestBot
    {
        /// <summary>
        /// Пользователь -  владелец бота
        /// </summary>
        public int OwnerUserId { set; get; }

        /// <summary>
        /// Наименование (ник) бота
        /// </summary>
        public string NicName { set; get; }

        /// <summary>
        /// Токен доступа к боту
        /// </summary>
        public string BotRequestToken { set; get; }

        /// <summary>
        /// Адрес конечной точки бота
        /// </summary>
        public string Url { set; get; }

        public RequestBot()
        {

        }

    }
}
