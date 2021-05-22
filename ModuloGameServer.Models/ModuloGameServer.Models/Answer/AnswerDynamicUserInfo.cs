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
        /// Список активный игр пользователя, в том числе ожидающих подтверждения со стороны пользователя или его соперника 
        /// </summary>
        public List<AnswerGame> ActiveGameList { set; get; }


        /// <summary>
        /// Список последних сыгранных, прерванных или отменённых игр пользователя
        /// </summary>
        public List<AnswerGame> RecentGameList { set; get; }


        public AnswerDynamicUserInfo(DynamicUserInfo du)
        {
            UserId = du.UserId;

            ActiveGameList = du.ActiveGameList?.Select(x => new AnswerGame(x, this.UserId)).ToList();

            RecentGameList = du.RecentGameList?.Select(x => new AnswerGame(x, this.UserId)).ToList();
        }


    }
}
