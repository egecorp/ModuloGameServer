using Newtonsoft.Json;
using System;


namespace ModuloGameServer.Models
{
    /// <summary>
    /// Один бот
    /// </summary>
    public class Bot
    {
        /// <summary>
        /// Идинтификатор бота, генерируется на сервере
        /// </summary>
        public Guid BotId { set; get; }

        /// <summary>
        /// Токен запросов в сторону бота, передаётся при создании
        /// </summary>
        public string BotRequestToken { set; get; }

        /// <summary>
        /// Адрес конечной точки бота
        /// </summary>
        public string Url { set; get; }

        /// <summary>
        /// Токен сервера (пароль), нужен для запросов к серверу
        /// </summary>
        public string ServerToken { set; get; }

        /// <summary>
        /// Связанный с ботом пользователь
        /// </summary>
        public int UserId { set; get; }

        /// <summary>
        /// Наименование (ник) бота
        /// </summary>
        public string NicName { set; get; }

        /// <summary>
        /// Бот отключен 
        /// </summary>
        public bool IsDisabled { set; get; }

        /// <summary>
        /// Пользователь -  владелец бота
        /// </summary>
        public int OwnerUserId { set; get; }

        /*
        /// <summary>
        /// Привязанный пользователь
        /// </summary>
        [JsonIgnore]
        public virtual User User { set; get; }

        /// <summary>
        /// Пользователь - владелец
        /// </summary>
        [JsonIgnore]
        public virtual User OwnerUser { set; get; }*/

    }
}
