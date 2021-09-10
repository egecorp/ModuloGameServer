using System;
using System.Collections.Generic;
using System.Linq;


namespace ModuloGameServer.Models
{
    /// <summary>
    /// Статистика топ 100
    /// </summary>
    public class AnswerStatisticTop
    {
        /// <summary>
        /// Позиция текущего пользователя в мире
        /// </summary>
        public int UserWorldRating { set; get; }

        /// <summary>
        /// Топ пользователей
        /// </summary>
        public List<AnswerUserRating> Top { set; get; }

        /// <summary>
        /// Сколько пользователь в топ
        /// </summary>
        public const int TOP_USERS_COUNT = 10;

        public AnswerStatisticTop(int currentUserWorldRating, List<User> users)
        {
            UserWorldRating = currentUserWorldRating;
            int currentPosition = 0;
            Top = users.Select(x => new AnswerUserRating(x, ++currentPosition)).ToList();
        }
    }
}
