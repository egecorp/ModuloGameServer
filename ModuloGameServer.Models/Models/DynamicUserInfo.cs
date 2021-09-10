using System;
using System.Collections.Generic;

namespace ModuloGameServer.Models
{
    /// <summary>
    /// Динамическая информация о пользователе - аватарка, список игр
    /// </summary>
    public class DynamicUserInfo
    {
        /// <summary>
        /// Пользователя
        /// </summary>
        public int UserId { set; get; }

        /// <summary>
        /// Общий рейтинг пользователя
        /// </summary>
        public int CommonRating { set; get; }

        /// <summary>
        /// Персонаж пользователя
        /// </summary>
        public string Character { set; get; }

        /// <summary>
        /// Эмоция персонажа пользователя
        /// </summary>
        public string Emotion { set; get; }

        /// <summary>
        /// Список активный игр пользователя, в том числе ожидающих подтверждения со стороны пользователя или его соперника 
        /// </summary>
        public List<Game> ActiveGameList { set; get; }


        /// <summary>
        /// Список последних сыгранных, прерванных или отменённых игр пользователя
        /// </summary>
        public List<Game> RecentGameList { set; get; }

        public const int COMMON_RATING_DEFAULT = 1500;
    }
}
