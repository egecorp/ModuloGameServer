using System;


namespace ModuloGameServer.Models
{
    /// <summary>
    /// Достижение пользователя
    /// </summary>
    public class AnswerUserAchievement
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// Наименование достижения
        /// </summary>
        public string AchievementName { set; get; }

        /// <summary>
        /// Дата получения достижения
        /// </summary>
        public DateTime GotDate { set; get; }

        /// <summary>
        /// Иконка достижения
        /// </summary>
        public string Picture { set; get; }

        
    }
}
