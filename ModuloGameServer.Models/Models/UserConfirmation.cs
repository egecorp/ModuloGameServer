using Newtonsoft.Json;
using System;


namespace ModuloGameServer.Models
{
    /// <summary>
    /// Отправленное подтверждение пользователя
    /// </summary>
    public class UserConfirmation
    {
        /// <summary>
        /// Идинтификатор 
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// Id устройства
        /// </summary>
        public int DeviceId { set; get; }

        /// <summary>
        /// Id пользователя
        /// </summary>
        public int UserId{ set; get; }

        /// <summary>
        /// Код подтверждения
        /// </summary>
        public string Code { set; get; }

        /// <summary>
        /// Код подтверждения для ссылки
        /// </summary>
        public string LinkCode { set; get; }

        /// <summary>
        /// Тип подтверждения
        /// </summary>
        public ConfirmationType ConfirmationType { set; get; }

        /// <summary>
        /// Способ подтверждения
        /// </summary>
        public ConfirmationMethod ConfirmationMethod { set; get; }

        /// <summary>
        /// Дата отправки
        /// </summary>
        public DateTime? SendStamp { set; get; }

        /// <summary>
        /// Дата истечения
        /// </summary>
        public DateTime? ExpiredTime { set; get; }

        /// <summary>
        /// Пользователь выполнил подтверждение
        /// </summary>
        public bool IsConfirm { set; get; }

        /// <summary>
        /// Подтверждение было отменено
        /// </summary>
        public bool IsCancel { set; get; }


        /// <summary>
        /// Как часто можно запрашивать повтор отправки подтверждения
        /// </summary>
        public const int MIN_REQUEST_PERIOD_MINUTES = 1;

        /// <summary>
        /// Сколько раз можно запрашивать повтор отправки подтверждения за сутки
        /// </summary>
        public const int MAX_REQUEST_ATTEMPTS_DAILY = 5;

        /// <summary>
        /// Сколько раз можно запрашивать повтор отправки подтверждения за месяц
        /// </summary>
        public const int MAX_REQUEST_ATTEMPTS_MONTHLY = 10;
    }
}
