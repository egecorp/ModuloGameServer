using System;
using System.Linq;
using System.Collections.Generic;

namespace ModuloGameServer.Models
{
    /// <summary>
    /// Данные пользователя
    /// </summary>
    public class AnswerDynamicUserInfo
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId { set; get; }

        /// <summary>
        /// Общий рейтинг пользователя
        /// </summary>
        public int CommonRating { set; get; }

        /// <summary>
        /// Список активный игр пользователя, в том числе ожидающих подтверждения со стороны пользователя или его соперника 
        /// </summary>
        public List<AnswerListGame> ActiveGameList { set; get; }


        /// <summary>
        /// Список последних сыгранных, прерванных или отменённых игр пользователя
        /// </summary>
        public List<AnswerListGame> RecentGameList { set; get; }


        public AnswerDynamicUserInfo(DynamicUserInfo du)
        {
            UserId = du.UserId;

            CommonRating = du.CommonRating;

            ActiveGameList = du.ActiveGameList?.Select(x => new AnswerListGame(x, UserId)).ToList();

            RecentGameList = du.RecentGameList?.Select(x => new AnswerListGame(x, UserId)).ToList();
        }


    }
}
