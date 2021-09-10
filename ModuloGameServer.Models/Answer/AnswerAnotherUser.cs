using System;


namespace ModuloGameServer.Models
{
    /// <summary>
    /// Один пользователь чужой
    /// </summary>
    public class AnswerAnotherUser
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
        /// Пользователь в друзьях у текущего пользователя
        /// </summary>
        public bool IsFriend { set; get; }

        /// <summary>
        /// Пользователь уже играл с текущим пользователем
        /// </summary>
        public bool IsPlayed { set; get; }

        /// <summary>
        /// Пользователь является ботом
        /// </summary>
        public bool IsBot { set; get; }


        /// <summary>
        /// Персонаж пользователя c эмоцией
        /// </summary>
        public string Character { set; get; }

        

        public AnswerAnotherUser(User u, User currentUser)
        {
            Id = u.Id;
            NicName = u.NicName;

            Character = u.DynamicUserInfo.Character + (string.IsNullOrWhiteSpace(u.DynamicUserInfo.Emotion)
                ? ""
                : (":" + u.DynamicUserInfo.Emotion));


        //TODO вставить проверку на дружбу и на игры
        //currentUser.DynamicUserInfo.
            IsFriend = false;
            IsPlayed = false;
            IsBot = false;
        }

        /// <summary>
        /// Ошибка, передаваемая в ответе
        /// </summary>
        public string Error { set; get; }


    }
}
