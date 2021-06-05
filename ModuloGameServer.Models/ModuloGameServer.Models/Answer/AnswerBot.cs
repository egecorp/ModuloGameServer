using System;

namespace ModuloGameServer.Models
{
    /// <summary>
    /// Одно устройство пользователя
    /// Для него на самом устройстве генерируется токен устройства (логин)
    /// На сервере при регистрации генерируется токен сервера (пароль)
    /// </summary>
    public class AnswerBot : Bot
    {
        // TODO подумать, что и когда реально нужно выдавать

        public AnswerBot(Bot d)
        {
            this.BotId = d.BotId;
            this.UserId = d.UserId;
            this.OwnerUserId = d.OwnerUserId;
            this.IsDisabled = d.IsDisabled;
            this.NicName = d.NicName;
            this.BotRequestToken = d.BotRequestToken;
            
        }

        /// <summary>
        /// Рабочий токен для обращений к серверу
        /// </summary>
        public string WorkToken { set; get; }

        /// <summary>
        /// Дата истечения времени рабочего токена
        /// </summary>
        public DateTime? WorkTokenExpired { set; get; }

        /// <summary>
        /// Ошибка, передаваемая в ответе
        /// </summary>
        public string Error { set; get; }


    }
}
