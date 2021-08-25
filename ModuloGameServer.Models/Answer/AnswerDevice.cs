using System;

namespace ModuloGameServer.Models
{
    /// <summary>
    /// Одно устройство пользователя
    /// Для него на самом устройстве генерируется токен устройства (логин)
    /// На сервере при регистрации генерируется токен сервера (пароль)
    /// </summary>
    public class AnswerDevice : Device
    {

        public AnswerDevice(Device d)
        {
            Id = d.Id;
            DeviceToken = d.DeviceToken;
            ServerToken = d.ServerToken;
            UserId = d.UserId;
            IsDisabled = d.IsDisabled;
            Caption = d.Caption;
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

        /// <summary>
        /// Устройство ожидает ввода кода подтверждения
        /// </summary>
        public bool WaitConfirmation { set; get; }

        /// <summary>
        /// EMail пользователя, для которого выслано подтверждение
        /// </summary>
        public string UserMail { set; get; }
    }
}
