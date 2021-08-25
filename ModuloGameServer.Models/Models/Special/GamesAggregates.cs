using System;

namespace ModuloGameServer.Models
{

    /// <summary>
    /// Агрегатый класс по играм
    /// </summary>
    public class GamesAggregates
    {

        /// <summary>
        /// Количество побед над сопериком
        /// </summary>
        public int WinCount { set; get; }

        /// <summary>
        /// Количество поражений от соперника
        /// </summary>
        public int LoseCount { set; get; }

        /// <summary>
        /// Количество ничей с соперником
        /// </summary>
        public int DrawCount { set; get; }



    }
}
