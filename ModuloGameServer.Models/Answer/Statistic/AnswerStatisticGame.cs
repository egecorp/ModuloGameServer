using System;
using System.Linq;
using System.Collections.Generic;

namespace ModuloGameServer.Models
{
    /// <summary>
    /// Одна игра в статистике
    /// </summary>
    public class AnswerStatisticGame
    {

        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// Состояние игры на данный момент
        /// </summary>
        public GAME_STATUS GameStatus { set; get; }

        /// <summary>
        /// Когда игра началась
        /// </summary>
        public DateTime GameStamp { set; get; }

        /// <summary>
        /// Счёт первого игрока
        /// </summary>
        public int User1Score { set; get; }

        /// <summary>
        /// Счёт второго игрока
        /// </summary>
        public int User2Score { set; get; }

        public AnswerStatisticGame(Game game, int myUserId)
        {
            Id = game.Id;
           
            User1Score = game.User1Score;
            User2Score = game.User2Score;

            GameStamp = game.StartStamp;

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
