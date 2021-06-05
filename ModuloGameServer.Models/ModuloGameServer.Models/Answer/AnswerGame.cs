using System;
using System.Linq;
using System.Collections.Generic;

namespace ModuloGameServer.Models
{
    /// <summary>
    /// Одна игра
    /// </summary>
    public class AnswerGame
    {

        #region TotalFields 

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


        #endregion


        #region Extended data

        /// <summary>
        /// Игра началась
        /// </summary>
        public bool IsStart { set; get; }

        /// <summary>
        /// Игра завершилась
        /// </summary>
        public bool IsFinish { set; get; }

        /// <summary>
        /// Был таймаут
        /// </summary>
        public bool IsTimeout { set; get; }

        /// <summary>
        /// Игра была отменена 
        /// </summary>
        public bool IsCancel { set; get; }

        /// <summary>
        /// Один из игроков сдался
        /// </summary>
        public bool IsGiveUp { set; get; }

        /// <summary>
        /// Второй игрок отказал в игре
        /// </summary>
        public bool IsDeclined { set; get; }

        /// <summary>
        /// Время начала игры
        /// </summary>
        public DateTime StartStamp { set; get; }

        /// <summary>
        /// Количество минут на один раунд
        /// </summary>
        public int MinutesPerRound { set; get; }

        /// <summary>
        /// Первый игрок может использовать джокера
        /// </summary>
        public bool User1CanUseJoker { set; get; }

        /// <summary>
        /// Второй игрок может использовать джокера
        /// </summary>
        public bool User2CanUseJoker { set; get; }

        /// <summary>
        /// Сколько раундов сыграл первый игрок
        /// </summary>
        public int User1MaxRoundNumber { set; get; }

        /// <summary>
        /// Сколько раундов сыграл второй игрок
        /// </summary>
        public int User2MaxRoundNumber { set; get; }


        /// <summary>
        /// Счёт первого игрока
        /// </summary>
        public int User1Score { set; get; }

        /// <summary>
        /// Счёт второго игрока
        /// </summary>
        public int User2Score { set; get; }


        /// <summary>
        /// Список ходов
        /// </summary>
        public List<GameRound> Rounds { set; get; }

        #endregion


        /// <summary>
        /// Текущий раунд
        /// </summary>
        public  int RoundNumber { set; get; }

        #region Digits

        /// <summary>
        /// Раунд 1 , игрок 1, позиция 1
        /// </summary>
        public int D1_1_1 { set; get; }

        /// <summary>
        /// Раунд 1 , игрок 1, позиция 2
        /// </summary>
        public int D1_1_2 { set; get; }

        /// <summary>
        /// Раунд 1 , игрок 1, позиция 3
        /// </summary>
        public int D1_1_3 { set; get; }

        /// <summary>
        /// Раунд 1 , игрок 2, позиция 1
        /// </summary>
        public int D1_2_1 { set; get; }

        /// <summary>
        /// Раунд 1 , игрок 2, позиция 2
        /// </summary>
        public int D1_2_2 { set; get; }

        /// <summary>
        /// Раунд 1 , игрок 2, позиция 3
        /// </summary>
        public int D1_2_3 { set; get; }



        /// <summary>
        /// Раунд 2 , игрок 1, позиция 1
        /// </summary>
        public int D2_1_1 { set; get; }

        /// <summary>
        /// Раунд 2 , игрок 1, позиция 2
        /// </summary>
        public int D2_1_2 { set; get; }

        /// <summary>
        /// Раунд 2 , игрок 1, позиция 3
        /// </summary>
        public int D2_1_3 { set; get; }

        /// <summary>
        /// Раунд 2 , игрок 2, позиция 1
        /// </summary>
        public int D2_2_1 { set; get; }

        /// <summary>
        /// Раунд 2 , игрок 2, позиция 2
        /// </summary>
        public int D2_2_2 { set; get; }

        /// <summary>
        /// Раунд 2 , игрок 2, позиция 3
        /// </summary>
        public int D2_2_3 { set; get; }


        /// <summary>
        /// Раунд 3 , игрок 1, позиция 1
        /// </summary>
        public int D3_1_1 { set; get; }

        /// <summary>
        /// Раунд 3 , игрок 1, позиция 2
        /// </summary>
        public int D3_1_2 { set; get; }

        /// <summary>
        /// Раунд 3 , игрок 1, позиция 3
        /// </summary>
        public int D3_1_3 { set; get; }

        /// <summary>
        /// Раунд 3 , игрок 2, позиция 1
        /// </summary>
        public int D3_2_1 { set; get; }

        /// <summary>
        /// Раунд 3 , игрок 2, позиция 2
        /// </summary>
        public int D3_2_2 { set; get; }

        /// <summary>
        /// Раунд 3 , игрок 2, позиция 3
        /// </summary>
        public int D3_2_3 { set; get; }



        /// <summary>
        /// Раунд 4 , игрок 1, позиция 1
        /// </summary>
        public int D4_1_1 { set; get; }

        /// <summary>
        /// Раунд 4 , игрок 1, позиция 2
        /// </summary>
        public int D4_1_2 { set; get; }

        /// <summary>
        /// Раунд 4 , игрок 1, позиция 3
        /// </summary>
        public int D4_1_3 { set; get; }

        /// <summary>
        /// Раунд 4 , игрок 2, позиция 1
        /// </summary>
        public int D4_2_1 { set; get; }

        /// <summary>
        /// Раунд 4 , игрок 2, позиция 2
        /// </summary>
        public int D4_2_2 { set; get; }

        /// <summary>
        /// Раунд 4 , игрок 2, позиция 3
        /// </summary>
        public int D4_2_3 { set; get; }



        /// <summary>
        /// Раунд 5 , игрок 1, позиция 1
        /// </summary>
        public int D5_1_1 { set; get; }

        /// <summary>
        /// Раунд 5 , игрок 1, позиция 2
        /// </summary>
        public int D5_1_2 { set; get; }

        /// <summary>
        /// Раунд 5 , игрок 1, позиция 3
        /// </summary>
        public int D5_1_3 { set; get; }

        /// <summary>
        /// Раунд 5 , игрок 2, позиция 1
        /// </summary>
        public int D5_2_1 { set; get; }

        /// <summary>
        /// Раунд 5 , игрок 2, позиция 2
        /// </summary>
        public int D5_2_2 { set; get; }

        /// <summary>
        /// Раунд 5 , игрок 2, позиция 3
        /// </summary>
        public int D5_2_3 { set; get; }


        #endregion

        public AnswerGame(Game game, int MyUserId)
        {
            Id = game.Id;
            StartStamp = game.StartStamp;
            MinutesPerRound = game.MinutesPerRound;
            User1Id = game.User1Id;
            User2Id = game.User2Id;
            IsStart = game.IsStart;
            IsFinish = game.IsFinish;
            IsTimeout = game.IsTimeout;
            IsCancel = game.IsCancel;
            IsGiveUp = game.IsGiveUp;
            IsDeclined = game.IsDeclined;

            User1Name = game.User1?.NicName;
            User2Name = game.User2?.NicName;

            User1MaxRoundNumber = game.User1MaxRoundNumber;
            User2MaxRoundNumber = game.User2MaxRoundNumber;

            User1Score = game.User1Score;
            User2Score = game.User2Score;

            //TODO посмотреть, как лучше сделать
            //RoundNumber = game.RoundNumber;

            switch (game.Status)
            {
                case GAME_STATUS.GAME_ROUND_1_NOUSER:
                case GAME_STATUS.GAME_ROUND_1_USER1_DONE:
                case GAME_STATUS.GAME_ROUND_1_USER2_DONE:
                    RoundNumber = 1;
                    break;

                case GAME_STATUS.GAME_ROUND_2_NOUSER:
                case GAME_STATUS.GAME_ROUND_2_USER1_DONE:
                case GAME_STATUS.GAME_ROUND_2_USER2_DONE:
                    RoundNumber = 2;
                    break;

                case GAME_STATUS.GAME_ROUND_3_NOUSER:
                case GAME_STATUS.GAME_ROUND_3_USER1_DONE:
                case GAME_STATUS.GAME_ROUND_3_USER2_DONE:
                    RoundNumber = 3;
                    break;

                case GAME_STATUS.GAME_ROUND_4_NOUSER:
                case GAME_STATUS.GAME_ROUND_4_USER1_DONE:
                case GAME_STATUS.GAME_ROUND_4_USER2_DONE:
                    RoundNumber = 4;
                    break;

                case GAME_STATUS.GAME_ROUND_5_NOUSER:
                case GAME_STATUS.GAME_ROUND_5_USER1_DONE:
                case GAME_STATUS.GAME_ROUND_5_USER2_DONE:
                    RoundNumber = 5;
                    break;
                default:
                    RoundNumber = 0;
                    break;
            }



            if (MyUserId == game.User2Id)
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

                D1_1_1 = game.D1_2_1;
                D1_1_2 = game.D1_2_2;
                D1_1_3 = game.D1_2_3;
                D1_2_1 = game.D1_1_1;
                D1_2_2 = game.D1_1_2;
                D1_2_3 = game.D1_1_3;

                D2_1_1 = game.D2_2_1;
                D2_1_2 = game.D2_2_2;
                D2_1_3 = game.D2_2_3;
                D2_2_1 = game.D2_1_1;
                D2_2_2 = game.D2_1_2;
                D2_2_3 = game.D2_1_3;

                D3_1_1 = game.D3_2_1;
                D3_1_2 = game.D3_2_2;
                D3_1_3 = game.D3_2_3;
                D3_2_1 = game.D3_1_1;
                D3_2_2 = game.D3_1_2;
                D3_2_3 = game.D3_1_3;

                D4_1_1 = game.D4_2_1;
                D4_1_2 = game.D4_2_2;
                D4_1_3 = game.D4_2_3;
                D4_2_1 = game.D4_1_1;
                D4_2_2 = game.D4_1_2;
                D4_2_3 = game.D4_1_3;

                D5_1_1 = game.D5_2_1;
                D5_1_2 = game.D5_2_2;
                D5_1_3 = game.D5_2_3;
                D5_2_1 = game.D5_1_1;
                D5_2_2 = game.D5_1_2;
                D5_2_3 = game.D5_1_3;
            }
            else
            {
                this.GameStatus = game.Status;


                D1_1_1 = game.D1_1_1;
                D1_1_2 = game.D1_1_2;
                D1_1_3 = game.D1_1_3;
                D1_2_1 = game.D1_2_1;
                D1_2_2 = game.D1_2_2;
                D1_2_3 = game.D1_2_3;

                D2_1_1 = game.D2_1_1;
                D2_1_2 = game.D2_1_2;
                D2_1_3 = game.D2_1_3;
                D2_2_1 = game.D2_2_1;
                D2_2_2 = game.D2_2_2;
                D2_2_3 = game.D2_2_3;

                D3_1_1 = game.D3_1_1;
                D3_1_2 = game.D3_1_2;
                D3_1_3 = game.D3_1_3;
                D3_2_1 = game.D3_2_1;
                D3_2_2 = game.D3_2_2;
                D3_2_3 = game.D3_2_3;

                D4_1_1 = game.D4_1_1;
                D4_1_2 = game.D4_1_2;
                D4_1_3 = game.D4_1_3;
                D4_2_1 = game.D4_2_1;
                D4_2_2 = game.D4_2_2;
                D4_2_3 = game.D4_2_3;

                D5_1_1 = game.D5_1_1;
                D5_1_2 = game.D5_1_2;
                D5_1_3 = game.D5_1_3;
                D5_2_1 = game.D5_2_1;
                D5_2_2 = game.D5_2_2;
                D5_2_3 = game.D5_2_3;
            }

            this.Rounds = null;
            
        }
        public AnswerGame(Game game, int MyUserId, bool FullInformation):
            this(game, MyUserId)
        {

            if (game.Rounds == null) return;

            if (MyUserId == game.User1Id)
            {
                if (this.User1MaxRoundNumber < this.User2MaxRoundNumber)
                {
                    this.Rounds = game.Rounds.Where(x => x.RoundNumber <= this.User1MaxRoundNumber).ToList();
                }
                else
                {
                    this.Rounds = game.Rounds.ToList();
                }
            }
            else if (MyUserId == game.User2Id)
            {
                if (this.User2MaxRoundNumber < this.User1MaxRoundNumber)
                {
                    this.Rounds = game.Rounds.Where(x => x.RoundNumber <= this.User2MaxRoundNumber).ToList();
                }
                else
                {
                    this.Rounds = game.Rounds.ToList();
                }
            }
            else
            {
                game.Rounds = game.Rounds.Where(x => (x.RoundNumber <= this.User1MaxRoundNumber) && (x.RoundNumber <= this.User2MaxRoundNumber)).ToList();
            }

            User1CanUseJoker = game.CanUseJoker(true);
            User2CanUseJoker = game.CanUseJoker(false);
        }
    }
}
