using System;
using System.Linq;
using System.Collections.Generic;

namespace ModuloGameServer.Models
{
    /// <summary>
    /// Ответ на поиск пользователей
    /// </summary>
    public class AnswerUserList
    {

        public const int MAX_LIST_COUNT = 100;

        public const int MAX_RECENT_COUNT = 10;

        /// <summary>
        /// Список найденых пользователей, ограниченный MAX_LIST_COUNT
        /// </summary>
        public List<AnswerAnotherUser> UserList { set; get; }


        /// <summary>
        /// Список последних пользователей, ограниченный MAX_RECENT_COUNT
        /// </summary>
        public List<AnswerAnotherUser> RecentUserList { set; get; }


        /// <summary>
        /// Количество всех пользователей
        /// </summary>
        public int Count { set; get; }
        
        public AnswerUserList(IEnumerable<User> userList, IEnumerable<User> recentUserList, User currentUser)
        {

            Count = userList.Count();

            UserList = userList.Take(MAX_LIST_COUNT).Select(x => new AnswerAnotherUser(x, currentUser)).ToList();

            RecentUserList = recentUserList.Take(MAX_RECENT_COUNT).Select(x => new AnswerAnotherUser(x, currentUser)).ToList();

        }

        /// <summary>
        /// Ошибка, передаваемая в ответе
        /// </summary>
        public string Error { set; get; }


    }
}
