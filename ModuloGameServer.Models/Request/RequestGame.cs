using System;

namespace ModuloGameServer.Request
{
    /// <summary>
    /// Одна игра
    /// </summary>
    public class RequestGame
    {

        /// <summary>
        /// Рабочий токен для обращений к серверу
        /// </summary>
        public string DeviceWorkToken { set; get; }


        /// <summary>
        /// Id соперника
        /// </summary>
        public int? CompetitorUserId { set; get; }
        
        /// <summary>
        /// Создать игру со случайным соперником
        /// </summary>
        public bool IsRandomCompetitor { set; get; }

        
        /// <summary>
        /// Id игры
        /// </summary>
        public int? Id { set; get; }

        /// <summary>
        /// Какой ход делается
        /// </summary>
        public int? RoundNumber { set; get; }

        /// <summary>
        /// Первое число
        /// </summary>
        public int? Digit1 { set; get; }

        /// <summary>
        /// Второе число
        /// </summary>
        public int? Digit2 { set; get; }
        
        /// <summary>
        /// Третье число
        /// </summary>
        public int? Digit3 { set; get; }

        public RequestGame()
        {

        }

    }
}
