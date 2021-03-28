using System;

namespace ModuloGameServer.Models
{
    /// <summary>
    /// Одна игра
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// Время начала игры
        /// </summary>
        public DateTime StartStamp { set; get; }

        /// <summary>
        /// Количество минут на один раунд
        /// </summary>
        public int MinutesPerRound { set; get; }

        /// <summary>
        /// Первый игрок
        /// </summary>
        public int? User1Id { set; get; }

        /// <summary>
        /// Второй игрок
        /// </summary>
        public int? User2Id { set; get; }

        /// <summary>
        /// Игра началась
        /// </summary>
        public bool IsStart { set; get; }

        /// <summary>
        /// Игра завершилась
        /// </summary>
        public bool IsFinish { set; get; }

        /// <summary>
        /// Был таймаут
        /// </summary>
        public bool IsTimeout { set; get; }

        /// <summary>
        /// Игра была отменена 
        /// </summary>
        public bool IsCancel { set; get; }

    }
}
