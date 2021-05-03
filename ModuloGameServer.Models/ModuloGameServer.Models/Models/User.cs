using System;

namespace ModuloGameServer.Models
{
    /*
     *   Пользователь может быть:
     *   анонимным - создаётся после регистрации устройства при отказе от регистрации
     *   неверифицированным - создаётся после ввода данных, но до ввода проверочного кода
     *   верефицированным - становится после ввода проверочного кода
     *   заблокированным - пока оставляем поле опционально, подумать как блокировать автоматически
     */

    /// <summary>
    /// Один пользователь, на него может быть зарегистрированно несколько устройств
    /// </summary>
    public class User
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
        /// Код верификации, 6 цифр
        /// </summary>
        public string VerifyCode { set; get; }

        /// <summary>
        /// Когда в последний раз запрашивалась верификация
        /// </summary>
        public DateTime? VerifyLastRequestStamp { set; get; }

        /// <summary>
        /// Пользователь объявил себя анонимом
        /// </summary>
        public bool IsAnonim { set; get; }

        /// <summary>
        /// Пользователь подтвердил верификацию
        /// </summary>
        public bool IsVerified { set; get; }


        /// <summary>
        /// Пользователь заблокирован
        /// </summary>
        public bool IsBlocked { set; get; }

        /// <summary>
        /// Конец автоматической блокировки пользователя
        /// </summary>
        public DateTime? BlockedUntil { set; get; }


        /// <summary>
        /// Динамические данные пользователя
        /// </summary>
        public DynamicUserInfo DynamicUserInfo { set; get; }


        /// <summary>
        /// Делимитор для отделения ника от ID
        /// </summary>
        public const string NICNAME_ID_DELIMITER = "&!&";



    }
}
