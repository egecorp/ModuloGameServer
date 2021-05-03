using System;

namespace ModuloGameServer.Models
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

        
        public RequestGame()
        {

        }

    }
}
