using System;

namespace ModuloGameServer.Request
{
    /// <summary>
    /// Одно устройство пользователя
    /// Для него на самом устройстве генерируется токен устройства (логин)
    /// На сервере при регистрации генерируется токен сервера (пароль)
    /// </summary>
    public class RequestDevice
    {

        /// <summary>
        /// Идинтификатор устройства
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// Токен устройства (логин)
        /// </summary>
        public Guid DeviceToken { set; get; }

        /// <summary>
        /// Токен сервера (пароль)
        /// </summary>
        public string ServerToken { set; get; }

        /// <summary>
        /// Связанный с устройством пользователь
        /// </summary>
        public int? UserId { set; get; }

        /// <summary>
        /// Неименование устройства пользователя
        /// </summary>
        public string Caption { set; get; }

        /// <summary>
        /// Рабочий токен для обращений к серверу
        /// </summary>
        public string WorkToken { set; get; }

        public RequestDevice()
        {

        }

    }
}
