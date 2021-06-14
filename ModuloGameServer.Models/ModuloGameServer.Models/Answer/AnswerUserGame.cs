using System;
using System.Linq;
using System.Collections.Generic;

namespace ModuloGameServer.Models
{
    /// <summary>
    /// Одна игра с полной статистикой
    /// </summary>
    public class AnswerUserGame
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
        /// Имя игрока
        /// </summary>
        public string MyUserName { set; get; }

        /// <summary>
        /// Имя соперника
        /// </summary>
        public string CompetitorUserName { set; get; }




        /// <summary>
        /// Номер игрока
        /// </summary>
        public string MyUserNicNumber { set; get; }


        /// <summary>
        /// Номер соперника
        /// </summary>
        public string CompetitorUserNicNumber { set; get; }



        /// <summary>
        /// Персонаж игрока
        /// </summary>
        public string MyUserCharacter { set; get; }

        /// <summary>
        /// Персонаж соперника
        /// </summary>
        public string CompetitorUserCharacter { set; get; }

        /// <summary>
        /// Состояние игры на данный момент
        /// </summary>
        public GAME_STATUS GameStatus { set; get; }

        
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
        /// Игрок может использовать джокера
        /// </summary>
        public bool MyUserCanUseJoker { set; get; }

        /// <summary>
        /// Соперник может использовать джокера
        /// </summary>
        public bool CompetitorUserCanUseJoker { set; get; }

        /// <summary>
        /// Сколько раундов сыграл игрок
        /// </summary>
        public int MyUserMaxRoundNumber { set; get; }

        /// <summary>
        /// Сколько раундов сыграл соперник
        /// </summary>
        public int CompetitorUserMaxRoundNumber { set; get; }


        /// <summary>
        /// Счёт игрока
        /// </summary>
        public int MyUserScore { set; get; }

        /// <summary>
        /// Счёт соперника
        /// </summary>
        public int CompetitorUserScore { set; get; }
        

        /// <summary>
        /// Текущий раунд
        /// </summary>
        public int RoundNumber { set; get; }

        /// <summary>
        /// Игра не завершена
        /// </summary>
        public bool IsActive { set; get; }

        /// <summary>
        /// Игрок сейчас делает ход
        /// </summary>
        public bool IsMyUserPlaying { set; get; }



        #region Digits

        /// <summary>
        /// Раунд 1 , игрок 1, позиция 1
        /// </summary>
        public int MyDigit11 { set; get; }

        /// <summary>
        /// Раунд 1 , игрок 1, позиция 2
        /// </summary>
        public int MyDigit12 { set; get; }

        /// <summary>
        /// Раунд 1 , игрок 1, позиция 3
        /// </summary>
        public int MyDigit13 { set; get; }

        /// <summary>
        /// Раунд 1 , игрок 2, позиция 1
        /// </summary>
        public int CompetitorDigit11 { set; get; }

        /// <summary>
        /// Раунд 1 , игрок 2, позиция 2
        /// </summary>
        public int CompetitorDigit12 { set; get; }

        /// <summary>
        /// Раунд 1 , игрок 2, позиция 3
        /// </summary>
        public int CompetitorDigit13 { set; get; }



        /// <summary>
        /// Раунд 2 , игрок 1, позиция 1
        /// </summary>
        public int MyDigit21 { set; get; }

        /// <summary>
        /// Раунд 2 , игрок 1, позиция 2
        /// </summary>
        public int MyDigit22 { set; get; }

        /// <summary>
        /// Раунд 2 , игрок 1, позиция 3
        /// </summary>
        public int MyDigit23 { set; get; }

        /// <summary>
        /// Раунд 2 , игрок 2, позиция 1
        /// </summary>
        public int CompetitorDigit21 { set; get; }

        /// <summary>
        /// Раунд 2 , игрок 2, позиция 2
        /// </summary>
        public int CompetitorDigit22 { set; get; }

        /// <summary>
        /// Раунд 2 , игрок 2, позиция 3
        /// </summary>
        public int CompetitorDigit23 { set; get; }


        /// <summary>
        /// Раунд 3 , игрок 1, позиция 1
        /// </summary>
        public int MyDigit31 { set; get; }

        /// <summary>
        /// Раунд 3 , игрок 1, позиция 2
        /// </summary>
        public int MyDigit32 { set; get; }

        /// <summary>
        /// Раунд 3 , игрок 1, позиция 3
        /// </summary>
        public int MyDigit33 { set; get; }

        /// <summary>
        /// Раунд 3 , игрок 2, позиция 1
        /// </summary>
        public int CompetitorDigit31 { set; get; }

        /// <summary>
        /// Раунд 3 , игрок 2, позиция 2
        /// </summary>
        public int CompetitorDigit32 { set; get; }

        /// <summary>
        /// Раунд 3 , игрок 2, позиция 3
        /// </summary>
        public int CompetitorDigit33 { set; get; }



        /// <summary>
        /// Раунд 4 , игрок 1, позиция 1
        /// </summary>
        public int MyDigit41 { set; get; }

        /// <summary>
        /// Раунд 4 , игрок 1, позиция 2
        /// </summary>
        public int MyDigit42 { set; get; }

        /// <summary>
        /// Раунд 4 , игрок 1, позиция 3
        /// </summary>
        public int MyDigit43 { set; get; }

        /// <summary>
        /// Раунд 4 , игрок 2, позиция 1
        /// </summary>
        public int CompetitorDigit41 { set; get; }

        /// <summary>
        /// Раунд 4 , игрок 2, позиция 2
        /// </summary>
        public int CompetitorDigit42 { set; get; }

        /// <summary>
        /// Раунд 4 , игрок 2, позиция 3
        /// </summary>
        public int CompetitorDigit43 { set; get; }



        /// <summary>
        /// Раунд 5 , игрок 1, позиция 1
        /// </summary>
        public int MyDigit51 { set; get; }

        /// <summary>
        /// Раунд 5 , игрок 1, позиция 2
        /// </summary>
        public int MyDigit52 { set; get; }

        /// <summary>
        /// Раунд 5 , игрок 1, позиция 3
        /// </summary>
        public int MyDigit53 { set; get; }

        /// <summary>
        /// Раунд 5 , игрок 2, позиция 1
        /// </summary>
        public int CompetitorDigit51 { set; get; }

        /// <summary>
        /// Раунд 5 , игрок 2, позиция 2
        /// </summary>
        public int CompetitorDigit52 { set; get; }

        /// <summary>
        /// Раунд 5 , игрок 2, позиция 3
        /// </summary>
        public int CompetitorDigit53 { set; get; }


        #endregion





        public AnswerUserGame(Game game, int myUserId)
        {
            Id = game.Id;
            StartStamp = game.StartStamp;
            MinutesPerRound = game.MinutesPerRound;

            IsStart = game.IsStart;
            IsFinish = game.IsFinish;
            IsTimeout = game.IsTimeout;
            IsCancel = game.IsCancel;
            IsGiveUp = game.IsGiveUp;
            IsDeclined = game.IsDeclined;

            IsActive = IsStart && !IsFinish && !IsTimeout && !IsCancel && !IsGiveUp && !IsDeclined;

            bool firstUserCanUseJoker = game.CanUseJoker(true);
            bool secondUserCanUseJoker = game.CanUseJoker(false);

            if (myUserId == game.User2Id)
            {
                User1Id = game.User2Id;
                User2Id = game.User1Id;
                MyUserName = game.User2?.NicName;
                CompetitorUserName = game.User1?.NicName;
                MyUserMaxRoundNumber = game.User2MaxRoundNumber;
                CompetitorUserMaxRoundNumber = game.User1MaxRoundNumber;
                MyUserScore = game.User2Score;
                CompetitorUserScore = game.User1Score;

            }
            else
            {
                User1Id = game.User1Id;
                User2Id = game.User2Id;
                MyUserName = game.User1?.NicName;
                CompetitorUserName = game.User2?.NicName;
                MyUserMaxRoundNumber = game.User1MaxRoundNumber;
                CompetitorUserMaxRoundNumber = game.User2MaxRoundNumber;
                MyUserScore = game.User1Score;
                CompetitorUserScore = game.User2Score;

            }


            if ((MyUserName ?? "").Contains("&!&"))
            {
                var nameParts = MyUserName.Split("&!&");
                if (nameParts.Length > 1)
                {
                    MyUserName = nameParts[0];
                    MyUserNicNumber = nameParts[1] ?? "0";
                }
            }

            if ((CompetitorUserName ?? "").Contains("&!&"))
            {
                var nameParts = CompetitorUserName.Split("&!&");
                if (nameParts.Length > 1)
                {
                    CompetitorUserName = nameParts[0];
                    CompetitorUserNicNumber = nameParts[1] ?? "0";
                }
            }

            
            RoundNumber = game.GetCurrentNumber();
            IsMyUserPlaying = false;

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

                MyDigit11 = game.D1_2_1;
                MyDigit12 = game.D1_2_2;
                MyDigit13 = game.D1_2_3;
                MyDigit21 = game.D2_2_1;
                MyDigit22 = game.D2_2_2;
                MyDigit23 = game.D2_2_3;
                MyDigit31 = game.D3_2_1;
                MyDigit32 = game.D3_2_2;
                MyDigit33 = game.D3_2_3;
                MyDigit41 = game.D4_2_1;
                MyDigit42 = game.D4_2_2;
                MyDigit43 = game.D4_2_3;
                MyDigit51 = game.D5_2_1;
                MyDigit52 = game.D5_2_2;
                MyDigit53 = game.D5_2_3;

                CompetitorDigit11 = game.D1_1_1;
                CompetitorDigit12 = game.D1_1_2;
                CompetitorDigit13 = game.D1_1_3;
                CompetitorDigit21 = game.D2_1_1;
                CompetitorDigit22 = game.D2_1_2;
                CompetitorDigit23 = game.D2_1_3;
                CompetitorDigit31 = game.D3_1_1;
                CompetitorDigit32 = game.D3_1_2;
                CompetitorDigit33 = game.D3_1_3;
                CompetitorDigit41 = game.D4_1_1;
                CompetitorDigit42 = game.D4_1_2;
                CompetitorDigit43 = game.D4_1_3;
                CompetitorDigit51 = game.D5_1_1;
                CompetitorDigit52 = game.D5_1_2;
                CompetitorDigit53 = game.D5_1_3;

                switch (game.Status)
                {
                    case GAME_STATUS.GAME_ROUND_1_NOUSER:
                    case GAME_STATUS.GAME_ROUND_1_USER1_DONE:
                    case GAME_STATUS.GAME_ROUND_2_NOUSER:
                    case GAME_STATUS.GAME_ROUND_2_USER1_DONE:
                    case GAME_STATUS.GAME_ROUND_3_NOUSER:
                    case GAME_STATUS.GAME_ROUND_3_USER1_DONE:
                    case GAME_STATUS.GAME_ROUND_4_NOUSER:
                    case GAME_STATUS.GAME_ROUND_4_USER1_DONE:
                    case GAME_STATUS.GAME_ROUND_5_NOUSER:
                    case GAME_STATUS.GAME_ROUND_5_USER1_DONE:
                        IsMyUserPlaying = true;
                        break;
                }

                MyUserCanUseJoker = secondUserCanUseJoker;
                CompetitorUserCanUseJoker = firstUserCanUseJoker;
            }
            else
            {
                this.GameStatus = game.Status;


                MyDigit11 = game.D1_1_1;
                MyDigit12 = game.D1_1_2;
                MyDigit13 = game.D1_1_3;
                MyDigit21 = game.D2_1_1;
                MyDigit22 = game.D2_1_2;
                MyDigit23 = game.D2_1_3;
                MyDigit31 = game.D3_1_1;
                MyDigit32 = game.D3_1_2;
                MyDigit33 = game.D3_1_3;
                MyDigit41 = game.D4_1_1;
                MyDigit42 = game.D4_1_2;
                MyDigit43 = game.D4_1_3;
                MyDigit51 = game.D5_1_1;
                MyDigit52 = game.D5_1_2;
                MyDigit53 = game.D5_1_3;

                CompetitorDigit11 = game.D1_2_1;
                CompetitorDigit12 = game.D1_2_2;
                CompetitorDigit13 = game.D1_2_3;
                CompetitorDigit21 = game.D2_2_1;
                CompetitorDigit22 = game.D2_2_2;
                CompetitorDigit23 = game.D2_2_3;
                CompetitorDigit31 = game.D3_2_1;
                CompetitorDigit32 = game.D3_2_2;
                CompetitorDigit33 = game.D3_2_3;
                CompetitorDigit41 = game.D4_2_1;
                CompetitorDigit42 = game.D4_2_2;
                CompetitorDigit43 = game.D4_2_3;
                CompetitorDigit51 = game.D5_2_1;
                CompetitorDigit52 = game.D5_2_2;
                CompetitorDigit53 = game.D5_2_3;

                switch (game.Status)
                {
                    case GAME_STATUS.GAME_ROUND_1_NOUSER:
                    case GAME_STATUS.GAME_ROUND_1_USER2_DONE:
                    case GAME_STATUS.GAME_ROUND_2_NOUSER:
                    case GAME_STATUS.GAME_ROUND_2_USER2_DONE:
                    case GAME_STATUS.GAME_ROUND_3_NOUSER:
                    case GAME_STATUS.GAME_ROUND_3_USER2_DONE:
                    case GAME_STATUS.GAME_ROUND_4_NOUSER:
                    case GAME_STATUS.GAME_ROUND_4_USER2_DONE:
                    case GAME_STATUS.GAME_ROUND_5_NOUSER:
                    case GAME_STATUS.GAME_ROUND_5_USER2_DONE:
                        IsMyUserPlaying = true;
                        break;
                }

                MyUserCanUseJoker = firstUserCanUseJoker;
                CompetitorUserCanUseJoker = secondUserCanUseJoker;
            }



            if (RoundNumber <= 5)
            {
                this.CompetitorDigit51 = this.CompetitorDigit52 = this.CompetitorDigit53 = 0;
            }
            if (RoundNumber <= 4)
            {
                this.CompetitorDigit41 = this.CompetitorDigit42 = this.CompetitorDigit43 = 0;
            }
            if (RoundNumber <= 3)
            {
                this.CompetitorDigit31 = this.CompetitorDigit32 = this.CompetitorDigit33 = 0;
            }
            if (RoundNumber <= 2)
            {
                this.CompetitorDigit21 = this.CompetitorDigit22 = this.CompetitorDigit23 = 0;
            }
            if (RoundNumber <= 1)
            {
                this.CompetitorDigit11 = this.CompetitorDigit12 = this.CompetitorDigit13 = 0;
            }
       


        }
    }
}
