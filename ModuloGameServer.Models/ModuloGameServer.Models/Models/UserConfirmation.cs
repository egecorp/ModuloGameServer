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

    }
}
