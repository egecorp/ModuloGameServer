using System;


namespace ModuloGameServer.Models
{
    /// <summary>
    /// Один пользователь
    /// </summary>
    public class AnswerUser
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

        public DynamicUserInfo DynamicUserInfo { set; get; }

        public AnswerUser(User u)
        {
            Id = u.Id;
            NicName = u.NicName;
            Birthday = u.Birthday;
            EMail = u.EMail;
            TNumber = u.TNumber;
            //VerifyLastRequestStamp = u.VerifyLastRequestStamp;
            IsAnonim = u.IsAnonim;
            IsVerified = u.IsVerified;
            IsBlocked = u.IsBlocked;
            BlockedUntil = u.BlockedUntil;
            DynamicUserInfo = u.DynamicUserInfo;
        }

        /// <summary>
        /// Ошибка, передаваемая в ответе
        /// </summary>
        public string Error { set; get; }


    }
}
