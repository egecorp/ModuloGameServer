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

        /// <summary>
        /// Список найденых пользователей, ограниченный MAX_LIST_COUNT
        /// </summary>
        public List<AnswerAnotherUser> UserList { set; get; }

        /// <summary>
        /// Количество всех пользователей
        /// </summary>
        public int Count { set; get; }
        
        public AnswerUserList(IEnumerable<User> userList, User currentUser)
        {

            Count = userList.Count();

            UserList = userList.Take(MAX_LIST_COUNT).Select(x => new AnswerAnotherUser(x, currentUser)).ToList();

        }

        /// <summary>
        /// Ошибка, передаваемая в ответе
        /// </summary>
        public string Error { set; get; }


    }
}
