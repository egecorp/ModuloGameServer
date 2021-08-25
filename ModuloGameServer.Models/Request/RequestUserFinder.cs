using System;

namespace ModuloGameServer.Request
{
    /// <summary>
    /// Поиск одного пользователя
    /// </summary>
    public class RequestUserFinder
    {

        /// <summary>
        /// Рабочий токен для обращений к серверу
        /// </summary>
        public string DeviceWorkToken { set; get; }


        /// <summary>
        /// Ник пользователя
        /// </summary>
        public string NicName { set; get; }

        /// <summary>
        /// Пользователь бот
        /// </summary>
        public bool IsBot { set; get; }

        /// <summary>
        /// С пользователем уже была игра
        /// </summary>
        public bool IsGamed { set; get; }

        /// <summary>
        /// Пользователь в друзьях
        /// </summary>
        public bool IsFriend { set; get; }

        /// <summary>
        /// Ограничение по количеству пользователей
        /// </summary>
        public int Limit { set; get; }
        
        public RequestUserFinder()
        {

        }

    }
}
