using System;
using System.Linq;
using System.Collections.Generic;

namespace ModuloGameServer.Models
{
    /// <summary>
    /// Одна игра
    /// </summary>
    public class AnswerListGame
    {

        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// Первый игрок
        /// </summary>
        public int? User1Id { set; get; }

        /// <summary>
        /// Второй игрок
        /// </summary>
        public int? User2Id { set; get; }

        /// <summary>
        /// Имя первого игрока
        /// </summary>
        public string User1Name { set; get; }

        /// <summary>
        /// Имя второго игрока
        /// </summary>
        public string User2Name { set; get; }

        /// <summary>
        /// Номер первого игрока
        /// </summary>
        public string User1NicNumber { set; get; }


        /// <summary>
        /// Номер второго игрока
        /// </summary>
        public string User2NicNumber { set; get; }

        /// <summary>
        /// Персонаж первого игрока
        /// </summary>
        public string User1Character { set; get; }

        /// <summary>
        /// Персонаж второго игрока
        /// </summary>
        public string User2Character { set; get; }

        /// <summary>
        /// Состояние игры на данный момент
        /// </summary>
        public GAME_STATUS GameStatus { set; get; }


        /// <summary>
        /// Счёт первого игрока
        /// </summary>
        public int User1Score { set; get; }

        /// <summary>
        /// Счёт второго игрока
        /// </summary>
        public int User2Score { set; get; }


        public AnswerListGame(Game game, int myUserId)
        {
            Id = game.Id;
            User1Id = game.User1Id;
            User2Id = game.User2Id;
            User1Name = game.User1?.NicName;
            User2Name = game.User2?.NicName;

            User1Character = game.User1?.DynamicUserInfo.Character +
                             (string.IsNullOrWhiteSpace(game.User1?.DynamicUserInfo.Emotion)
                                 ? ""
                                 : (":" + game.User1?.DynamicUserInfo.Emotion));
            User2Character = game.User2?.DynamicUserInfo.Character +
                             (string.IsNullOrWhiteSpace(game.User2?.DynamicUserInfo.Emotion)
                                 ? ""
                                 : (":" + game.User2?.DynamicUserInfo.Emotion));

            if ((User1Name ?? "").Contains("&!&"))
            {
                var nameParts = User1Name.Split("&!&");
                if (nameParts.Length > 1)
                {
                    User1Name = nameParts[0];
                    User1NicNumber = nameParts[1] ?? "0";
                }
            }

            if ((User2Name ?? "").Contains("&!&"))
            {
                var nameParts = User2Name.Split("&!&");
                if (nameParts.Length > 1)
                {
                    User2Name = nameParts[0];
                    User2NicNumber = nameParts[1] ?? "0";
                }
            }
            
            User1Score = game.User1Score;
            User2Score = game.User2Score;
            

            if (myUserId == game.User2Id)
            {
                switch (game.Status)
                {
                    case GAME_STATUS.GAME_WAIT_USER1:
                        this.GameStatus = GAME_STATUS.GAME_WAIT_USER2;
                        break;
                    case GAME_STATUS.GAME_WAIT_USER2:
                        this.GameStatus = GAME_STATUS.GAME_WAIT_USER1;
                        break;
                    case GAME_STATUS.GAME_DECLINE_USER1:
                        this.GameStatus = GAME_STATUS.GAME_DECLINE_USER2;
                        break;
                    case GAME_STATUS.GAME_DECLINE_USER2:
                        this.GameStatus = GAME_STATUS.GAME_DECLINE_USER1;
                        break;

                    case GAME_STATUS.GAME_ROUND_1_USER1_DONE:
                        this.GameStatus = GAME_STATUS.GAME_ROUND_1_USER2_DONE;
                        break;
                    case GAME_STATUS.GAME_ROUND_1_USER2_DONE:
                        this.GameStatus = GAME_STATUS.GAME_ROUND_1_USER1_DONE;
                        break;
                    case GAME_STATUS.GAME_ROUND_1_USER1_GIVEUP:
                        this.GameStatus = GAME_STATUS.GAME_ROUND_1_USER2_GIVEUP;
                        break;
                    case GAME_STATUS.GAME_ROUND_1_USER2_GIVEUP:
                        this.GameStatus = GAME_STATUS.GAME_ROUND_1_USER1_GIVEUP;
                        break;

                    case GAME_STATUS.GAME_ROUND_2_USER1_DONE:
                        this.GameStatus = GAME_STATUS.GAME_ROUND_2_USER2_DONE;
                        break;
                    case GAME_STATUS.GAME_ROUND_2_USER2_DONE:
                        this.GameStatus = GAME_STATUS.GAME_ROUND_2_USER1_DONE;
                        break;
                    case GAME_STATUS.GAME_ROUND_2_USER1_GIVEUP:
                        this.GameStatus = GAME_STATUS.GAME_ROUND_2_USER2_GIVEUP;
                        break;
                    case GAME_STATUS.GAME_ROUND_2_USER2_GIVEUP:
                        this.GameStatus = GAME_STATUS.GAME_ROUND_2_USER1_GIVEUP;
                        break;

                    case GAME_STATUS.GAME_ROUND_3_USER1_DONE:
                        this.GameStatus = GAME_STATUS.GAME_ROUND_3_USER2_DONE;
                        break;
                    case GAME_STATUS.GAME_ROUND_3_USER2_DONE:
                        this.GameStatus = GAME_STATUS.GAME_ROUND_3_USER1_DONE;
                        break;
                    case GAME_STATUS.GAME_ROUND_3_USER1_GIVEUP:
                        this.GameStatus = GAME_STATUS.GAME_ROUND_3_USER2_GIVEUP;
                        break;
                    case GAME_STATUS.GAME_ROUND_3_USER2_GIVEUP:
                        this.GameStatus = GAME_STATUS.GAME_ROUND_3_USER1_GIVEUP;
                        break;

                    case GAME_STATUS.GAME_ROUND_4_USER1_DONE:
                        this.GameStatus = GAME_STATUS.GAME_ROUND_4_USER2_DONE;
                        break;
                    case GAME_STATUS.GAME_ROUND_4_USER2_DONE:
                        this.GameStatus = GAME_STATUS.GAME_ROUND_4_USER1_DONE;
                        break;
                    case GAME_STATUS.GAME_ROUND_4_USER1_GIVEUP:
                        this.GameStatus = GAME_STATUS.GAME_ROUND_4_USER2_GIVEUP;
                        break;
                    case GAME_STATUS.GAME_ROUND_4_USER2_GIVEUP:
                        this.GameStatus = GAME_STATUS.GAME_ROUND_4_USER1_GIVEUP;
                        break;

                    case GAME_STATUS.GAME_ROUND_5_USER1_DONE:
                        this.GameStatus = GAME_STATUS.GAME_ROUND_5_USER2_DONE;
                        break;
                    case GAME_STATUS.GAME_ROUND_5_USER2_DONE:
                        this.GameStatus = GAME_STATUS.GAME_ROUND_5_USER1_DONE;
                        break;
                    case GAME_STATUS.GAME_ROUND_5_USER1_GIVEUP:
                        this.GameStatus = GAME_STATUS.GAME_ROUND_5_USER2_GIVEUP;
                        break;
                    case GAME_STATUS.GAME_ROUND_5_USER2_GIVEUP:
                        this.GameStatus = GAME_STATUS.GAME_ROUND_5_USER1_GIVEUP;
                        break;


                    case GAME_STATUS.GAME_FINISH_USER1_WIN:
                        this.GameStatus = GAME_STATUS.GAME_FINISH_USER2_WIN;
                        break;
                    case GAME_STATUS.GAME_FINISH_USER2_WIN:
                        this.GameStatus = GAME_STATUS.GAME_FINISH_USER1_WIN;
                        break;

                    default:
                        this.GameStatus = game.Status;
                        break;
                }

            
            }
            else
            {
                this.GameStatus = game.Status;


            }

        }
    }
}
