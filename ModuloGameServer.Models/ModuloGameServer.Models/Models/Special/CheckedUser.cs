using System;

namespace ModuloGameServer.Models
{

    /// <summary>
    /// Пользователь или ошибка
    /// </summary>
    public class CheckedUser
    {
        /// <summary>
        /// Пользователь
        /// </summary>
        public User User { set; get; }

        /// <summary>
        /// Текст ошибки
        /// </summary>
        public string ErrorString { set; get; }

        /// <summary>
        /// Динамические данные пользователя
        /// </summary>
        public DynamicUserInfo DynamicUserInfo { set; get; }



    }
}
