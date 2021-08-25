using System;
using System.Collections.Generic;
using System.Linq;


namespace ModuloGameServer.Models
{
    /// <summary>
    /// Статистика текущего пользователя
    /// </summary>
    public class AnswerStatisticUser
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
        /// Персонаж пользователя
        /// </summary>
        public string Character { set; get; }

        /// <summary>
        /// Количество побед
        /// </summary>
        public int WinCount { set; get; }

        /// <summary>
        /// Количество поражений
        /// </summary>
        public int LoseCount { set; get; }

        /// <summary>
        /// Количество ничей
        /// </summary>
        public int DrawCount { set; get; }

        /// <summary>
        /// Достижения пользователя
        /// </summary>
        public List<AnswerUserAchievement> Achievements { set; get; }

        public AnswerStatisticUser(User user, GamesAggregates gamesAggregates, IEnumerable<AnswerUserAchievement> userAchievements)
        {
            Id = user.Id;
            NicName = user.NicName;
            // TODO падать, если null
            Rating= user.DynamicUserInfo?.CommonRating ?? 0;
            // TODO доделать
            WorldRating = 1500;
            // TODO доделать
            Character = null;
            WinCount = gamesAggregates.WinCount;
            LoseCount = gamesAggregates.LoseCount;
            DrawCount = gamesAggregates.DrawCount;
            Achievements = userAchievements?.ToList();
        }
    }
}
