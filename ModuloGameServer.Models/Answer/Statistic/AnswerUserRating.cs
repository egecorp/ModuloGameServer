using System;
using System.Collections.Generic;
using System.Linq;


namespace ModuloGameServer.Models
{
    /// <summary>
    /// Пользователь в рейтинге
    /// </summary>
    public class AnswerUserRating
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// Ник пользователя
        /// </summary>
        public string NicName { set; get; }

        /// <summary>
        /// Рейтинг пользователя
        /// </summary>
        public int Rating { set; get; }

        /// <summary>
        /// Место в мире у пользователя
        /// </summary>
        public int WorldRating { set; get; }

        /// <summary>
        /// Персонаж пользователя с эмоцией
        /// </summary>
        public string Character { set; get; }


        public AnswerUserRating(User user, int worldIndex)
        {
            Id = user.Id;
            NicName = user.NicName;
            Rating= user.DynamicUserInfo.CommonRating;
            WorldRating = worldIndex;
            Character = user.DynamicUserInfo.Character + (string.IsNullOrWhiteSpace(user.DynamicUserInfo.Emotion)
                ? ""
                : (":" + user.DynamicUserInfo.Emotion));
        }
    }
}
