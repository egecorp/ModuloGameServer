using System;

namespace ModuloGameServer.Request
{
    /// <summary>
    /// Один пользователь
    /// </summary>
    public class RequestUser
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// Ник пользователя
        /// </summary>
        public string NicName { set; get; }

        /// <summary>
        /// Дата рождения пользователя
        /// </summary>
        public DateTime? Birthday { set; get; }

        /// <summary>
        /// Почта пользователя
        /// </summary>
        public string EMail { set; get; }

        /// <summary>
        /// Номер телефона пользователя
        /// </summary>
        public string TNumber { set; get; }

        /// <summary>
        /// Страна пользователя
        /// </summary>
        public string Country { set; get; }


        /// <summary>
        /// Идентификатор устройства
        /// </summary>
//        public int DeviceId { set; get; }

        /// <summary>
        /// Рабочий токен для обращений к серверу
        /// </summary>
        public string DeviceWorkToken { set; get; }

        public RequestUser()
        {

        }

    }
}
