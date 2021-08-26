using System;
using System.Collections.Generic;
using System.Linq;

namespace ModuloGameServer.Models
{
    /// <summary>
    /// Статистика соперника
    /// </summary>
    public class AnswerStatisticCompetitor
    {
        public const int GAMES_ON_A_REQUEST = 10;

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int MyId { set; get; }

        /// <summary>
        /// Ник пользователя
        /// </summary>
        public string MyNicName { set; get; }

        /// <summary>
        /// Рейтинг пользователя
        /// </summary>
        public int MyRating { set; get; }

        /// <summary>
        /// Место в мире у пользователя
        /// </summary>
        public int MyWorldRating { set; get; }

        /// <summary>
        /// Персонаж пользователя
        /// </summary>
        public string MyCharacter { set; get; }




        /// <summary>
        /// Идентификатор соперника
        /// </summary>
        public int CompetitorId { set; get; }

        /// <summary>
        /// Ник соперника
        /// </summary>
        public string CompetitorName { set; get; }

        /// <summary>
        /// Рейтинг соперника
        /// </summary>
        public int CompetitorRating { set; get; }

        /// <summary>
        /// Место в мире у соперника
        /// </summary>
        public int CompetitorWorldRating { set; get; }

        /// <summary>
        /// Персонаж соперника
        /// </summary>
        public string CompetitorCharacter { set; get; }

        /// <summary>
        /// Количество побед над сопериком
        /// </summary>
        public int WinCount { set; get; }

        /// <summary>
        /// Количество поражений от соперника
        /// </summary>
        public int LoseCount { set; get; }

        /// <summary>
        /// Количество ничей с соперником
        /// </summary>
        public int DrawCount { set; get; }

        /// <summary>
        /// Последние игры с соперника
        /// </summary>
        public List<AnswerStatisticGame> Games { set; get; }

        /// <summary>
        /// Количество игр с соперником
        /// </summary>
        public int GamesCount { set; get; }


        public AnswerStatisticCompetitor(User user, User competitor, GamesAggregates gamesAggregates, IEnumerable<AnswerStatisticGame> games, int gamesCount)
        {
            MyId = user.Id;
            MyNicName = user.NicName;

            // TODO доделать
            MyWorldRating = 15;
            // TODO падать, если null
            MyRating = user.DynamicUserInfo?.CommonRating ?? 0;
            // TODO доделать
            MyCharacter = null;


            CompetitorId = competitor.Id;
            CompetitorName = competitor.NicName;
            
            // TODO доделать
            CompetitorWorldRating = 24;
            // TODO падать, если null
            CompetitorRating = competitor.DynamicUserInfo?.CommonRating ?? 0;
            // TODO доделать
            CompetitorCharacter = null;

            WinCount = gamesAggregates.WinCount;
            LoseCount = gamesAggregates.LoseCount;
            DrawCount = gamesAggregates.DrawCount;

            Games = games?.Take(GAMES_ON_A_REQUEST).ToList();
            GamesCount = gamesCount;
        }
    }
}
