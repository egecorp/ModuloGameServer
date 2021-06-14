using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModuloGameServer.Models
{
    /// <summary>
    /// Одна игра
    /// </summary>
    public class Game
    {
        public const int JOKER = 11;

        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// Время начала игры
        /// </summary>
        public DateTime StartStamp { set; get; }


        /// <summary>
        /// Время последнего хода
        /// </summary>
        public DateTime? LastRoundStamp { set; get; }

        /// <summary>
        /// Количество минут на один раунд
        /// </summary>
        public int MinutesPerRound { set; get; }

        /// <summary>
        /// Первый игрок
        /// </summary>
        public int? User1Id { set; get; }

        /// <summary>
        /// Второй игрок
        /// </summary>
        public int? User2Id { set; get; }

        /// <summary>
        /// Игра началась
        /// </summary>
        public bool IsStart { set; get; }

        /// <summary>
        /// Игра завершилась с полным набором раундов
        /// </summary>
        public bool IsFinish { set; get; }

        /// <summary>
        /// Второй пользователь отказался играть
        /// </summary>
        public bool IsDeclined { set; get; }

        /// <summary>
        /// Был таймаут
        /// </summary>
        public bool IsTimeout { set; get; }

        /// <summary>
        /// Игра была отменена 
        /// </summary>
        public bool IsCancel { set; get; }

        /// <summary>
        /// Один из пользователей сдался
        /// </summary>
        public bool IsGiveUp { set; get; }


        /// <summary>
        /// Сколько раундов сыграл первый игрок, расчитывается при изменении
        /// </summary>
        public int User1MaxRoundNumber { set; get; }

        /// <summary>
        /// Сколько раундов сыграл второй игрок, расчитывается при изменении
        /// </summary>
        public int User2MaxRoundNumber { set; get; }

        /// <summary>
        /// Статус игры, расчитывается при изменении
        /// </summary>
        public GAME_STATUS Status { set; get; }



        /// <summary>
        /// Количество очков первого пользователя
        /// </summary>
        public int User1Score { set; get; }

        /// <summary>
        /// Количество очков второго пользователя
        /// </summary>
        public int User2Score { set; get; }


        /// <summary>
        /// Первый игрок
        /// </summary>
        [JsonIgnore]
        public virtual User User1 { set; get; }


        /// <summary>
        /// Второй игрок
        /// </summary>
        [JsonIgnore]
        public virtual User User2 { set; get; }

        /// <summary>
        /// Текущий раунд, который играется
        /// 0 - игра не началась
        /// 1..5 - первый или второй игрок играет 1..5 раунд
        /// 6 - игра окончена
        /// </summary>
        public int RoundNumber { set; get; }

        /// <summary>
        /// Первый игрок сдался
        /// </summary>
        public bool IsUser1GiveUp { set; get; }

        /// <summary>
        /// Первый игрок сдался
        /// </summary>
        public bool IsUser2GiveUp { set; get; }

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



        //public virtual GameResult Result { set; get; }        

        public GAME_STATUS UpdateStatus()
        {
            if (!this.IsStart)
            {
                if (this.IsTimeout)
                {
                    if (!this.User2Id.HasValue) return this.Status = GAME_STATUS.GAME_RANDOM_TIMEOUT;
                    return this.Status = GAME_STATUS.GAME_TIMEOUT;
                }

                if (this.IsDeclined) return this.Status = GAME_STATUS.GAME_DECLINE_USER2;


                if (this.IsCancel)
                {
                    if (!this.User2Id.HasValue) return this.Status = GAME_STATUS.GAME_RANDOM_CANCEL;
                    return this.Status = GAME_STATUS.GAME_CANCEL;
                }

                if (!this.User2Id.HasValue) return this.Status = GAME_STATUS.GAME_RANDOM_CREATING;


                return this.Status = GAME_STATUS.GAME_WAIT_USER2;
            }


            if ((User1MaxRoundNumber == 0) && (User2MaxRoundNumber == 0))
            {
                if (this.IsTimeout) return this.Status = GAME_STATUS.GAME_ROUND_1_TIMEOUT;
                return this.Status = GAME_STATUS.GAME_ROUND_1_NOUSER;
            }

            int currentRoundNumber = Math.Max(User1MaxRoundNumber, User2MaxRoundNumber);

            this.User1Score = GetUser1Score();
            this.User2Score = GetUser2Score();

            if (this.IsFinish)
            {
                if (this.User1Score > this.User2Score) return this.Status = GAME_STATUS.GAME_FINISH_USER1_WIN;
                if (this.User1Score < this.User2Score) return this.Status = GAME_STATUS.GAME_FINISH_USER2_WIN;
                return this.Status = GAME_STATUS.GAME_FINISH_USER2_DRAW;
            }

            if (this.IsTimeout)
            {
                switch (currentRoundNumber)
                {
                    case 5: return Status = GAME_STATUS.GAME_ROUND_5_TIMEOUT;
                    case 4: return Status = GAME_STATUS.GAME_ROUND_4_TIMEOUT;
                    case 3: return Status = GAME_STATUS.GAME_ROUND_3_TIMEOUT;
                    case 2: return Status = GAME_STATUS.GAME_ROUND_2_TIMEOUT;
                    default: return Status = GAME_STATUS.GAME_ROUND_1_TIMEOUT;
                }
            }

            if (this.IsGiveUp)
            {
                if (this.IsUser1GiveUp)
                {
                    switch (User1MaxRoundNumber)
                    {
                        case 5: return Status = GAME_STATUS.GAME_ROUND_5_USER1_GIVEUP;
                        case 4: return Status = GAME_STATUS.GAME_ROUND_4_USER1_GIVEUP;
                        case 3: return Status = GAME_STATUS.GAME_ROUND_3_USER1_GIVEUP;
                        case 2: return Status = GAME_STATUS.GAME_ROUND_2_USER1_GIVEUP;
                        default: return Status = GAME_STATUS.GAME_ROUND_1_USER1_GIVEUP;
                    }
                }
                else
                {
                    switch (User2MaxRoundNumber)
                    {
                        case 5: return Status = GAME_STATUS.GAME_ROUND_5_USER2_GIVEUP;
                        case 4: return Status = GAME_STATUS.GAME_ROUND_4_USER2_GIVEUP;
                        case 3: return Status = GAME_STATUS.GAME_ROUND_3_USER2_GIVEUP;
                        case 2: return Status = GAME_STATUS.GAME_ROUND_2_USER2_GIVEUP;
                        default: return Status = GAME_STATUS.GAME_ROUND_1_USER2_GIVEUP;
                    }
                }
            }

            switch (currentRoundNumber)
            {
                case 5:
                    if (User1MaxRoundNumber == User2MaxRoundNumber)
                    {
                        this.IsFinish = true;
                        if (this.User1Score > this.User2Score) return this.Status = GAME_STATUS.GAME_FINISH_USER1_WIN;
                        if (this.User1Score < this.User2Score) return this.Status = GAME_STATUS.GAME_FINISH_USER2_WIN;
                        return this.Status = GAME_STATUS.GAME_FINISH_USER2_DRAW;
                    }

                    if (User1MaxRoundNumber == 5) return Status = GAME_STATUS.GAME_ROUND_5_USER1_DONE;
                    if (User2MaxRoundNumber == 5) return Status = GAME_STATUS.GAME_ROUND_5_USER2_DONE;
                    return Status = GAME_STATUS.GAME_ROUND_5_NOUSER;
                case 4:
                    if (User1MaxRoundNumber == User2MaxRoundNumber)
                        return Status = GAME_STATUS.GAME_ROUND_5_NOUSER;
                    if (User1MaxRoundNumber == 4) return Status = GAME_STATUS.GAME_ROUND_4_USER1_DONE;
                    if (User2MaxRoundNumber == 4) return Status = GAME_STATUS.GAME_ROUND_4_USER2_DONE;
                    return Status = GAME_STATUS.GAME_ROUND_4_NOUSER;
                case 3:
                    if (User1MaxRoundNumber == User2MaxRoundNumber)
                        return Status = GAME_STATUS.GAME_ROUND_4_NOUSER;
                    if (User1MaxRoundNumber == 3) return Status = GAME_STATUS.GAME_ROUND_3_USER1_DONE;
                    if (User2MaxRoundNumber == 3) return Status = GAME_STATUS.GAME_ROUND_3_USER2_DONE;
                    return Status = GAME_STATUS.GAME_ROUND_3_NOUSER;
                case 2:
                    if (User1MaxRoundNumber == User2MaxRoundNumber)
                        return Status = GAME_STATUS.GAME_ROUND_3_NOUSER;
                    if (User1MaxRoundNumber == 2) return Status = GAME_STATUS.GAME_ROUND_2_USER1_DONE;
                    if (User2MaxRoundNumber == 2) return Status = GAME_STATUS.GAME_ROUND_2_USER2_DONE;
                    return Status = GAME_STATUS.GAME_ROUND_2_NOUSER;
                default:
                    if (User1MaxRoundNumber == User2MaxRoundNumber)
                        return Status = GAME_STATUS.GAME_ROUND_2_NOUSER;
                    if (User1MaxRoundNumber == 1) return Status = GAME_STATUS.GAME_ROUND_1_USER1_DONE;
                    if (User2MaxRoundNumber == 1) return Status = GAME_STATUS.GAME_ROUND_1_USER2_DONE;
                    return Status = GAME_STATUS.GAME_ROUND_1_NOUSER;
            }

        }

        public int GetCurrentNumber()
        {
            switch (Status)
            {
                case GAME_STATUS.GAME_ROUND_1_NOUSER:
                case GAME_STATUS.GAME_ROUND_1_USER1_DONE:
                case GAME_STATUS.GAME_ROUND_1_USER2_DONE:
                    return  1;
                case GAME_STATUS.GAME_ROUND_2_NOUSER:
                case GAME_STATUS.GAME_ROUND_2_USER1_DONE:
                case GAME_STATUS.GAME_ROUND_2_USER2_DONE:
                    return  2;
                case GAME_STATUS.GAME_ROUND_3_NOUSER:
                case GAME_STATUS.GAME_ROUND_3_USER1_DONE:
                case GAME_STATUS.GAME_ROUND_3_USER2_DONE:
                    return 3;
                case GAME_STATUS.GAME_ROUND_4_NOUSER:
                case GAME_STATUS.GAME_ROUND_4_USER1_DONE:
                case GAME_STATUS.GAME_ROUND_4_USER2_DONE:
                    return 4;
                case GAME_STATUS.GAME_ROUND_5_NOUSER:
                case GAME_STATUS.GAME_ROUND_5_USER1_DONE:
                case GAME_STATUS.GAME_ROUND_5_USER2_DONE:
                    return 5;
                default:
                    return  0;
            }
        }
        public bool CanUseJoker(bool firstGamer)
        {
            int roundNumber = GetCurrentNumber();
            if (roundNumber <= 3) return false;

            List<int> digits = new List<int>();
            if (firstGamer)
            {
                if ((D1_1_1 != 0) && !digits.Contains(D1_1_1)) digits.Add(D1_1_1);
                if ((D1_1_2 != 0) && !digits.Contains(D1_1_2)) digits.Add(D1_1_2);
                if ((D1_1_3 != 0) && !digits.Contains(D1_1_3)) digits.Add(D1_1_3);
                if ((D2_1_1 != 0) && !digits.Contains(D2_1_1)) digits.Add(D2_1_1);
                if ((D2_1_2 != 0) && !digits.Contains(D2_1_2)) digits.Add(D2_1_2);
                if ((D2_1_3 != 0) && !digits.Contains(D2_1_3)) digits.Add(D2_1_3);
                if ((D3_1_1 != 0) && !digits.Contains(D3_1_1)) digits.Add(D3_1_1);
                if ((D3_1_2 != 0) && !digits.Contains(D3_1_2)) digits.Add(D3_1_2);
                if ((D3_1_3 != 0) && !digits.Contains(D3_1_3)) digits.Add(D3_1_3);
                if (roundNumber == 5)
                {
                    if ((D4_1_1 != 0) && !digits.Contains(D4_1_1)) digits.Add(D4_1_1);
                    if ((D4_1_2 != 0) && !digits.Contains(D4_1_2)) digits.Add(D4_1_2);
                    if ((D4_1_3 != 0) && !digits.Contains(D4_1_3)) digits.Add(D4_1_3);
                }
            }
            else
            {
                if ((D1_2_1 != 0) && !digits.Contains(D1_2_1)) digits.Add(D1_2_1);
                if ((D1_2_2 != 0) && !digits.Contains(D1_2_2)) digits.Add(D1_2_2);
                if ((D1_2_3 != 0) && !digits.Contains(D1_2_3)) digits.Add(D1_2_3);
                if ((D2_2_1 != 0) && !digits.Contains(D2_2_1)) digits.Add(D2_2_1);
                if ((D2_2_2 != 0) && !digits.Contains(D2_2_2)) digits.Add(D2_2_2);
                if ((D2_2_3 != 0) && !digits.Contains(D2_2_3)) digits.Add(D2_2_3);
                if ((D3_2_1 != 0) && !digits.Contains(D3_2_1)) digits.Add(D3_2_1);
                if ((D3_2_2 != 0) && !digits.Contains(D3_2_2)) digits.Add(D3_2_2);
                if ((D3_2_3 != 0) && !digits.Contains(D3_2_3)) digits.Add(D3_2_3);
                if (roundNumber == 5)
                {
                    if ((D4_2_1 != 0) && !digits.Contains(D4_2_1)) digits.Add(D4_2_1);
                    if ((D4_2_2 != 0) && !digits.Contains(D4_2_2)) digits.Add(D4_2_2);
                    if ((D4_2_3 != 0) && !digits.Contains(D4_2_3)) digits.Add(D4_2_3);
                }
            }

            return digits.DefaultIfEmpty(0).Sum() == 44;
        }



        public void PlayRound(bool isFirstGamer, int roundNumber, int d1, int d2, int d3)
        {
            if (!CanPlayRound(isFirstGamer, roundNumber))
            {
                throw new Exception("Cannot play round");
            }

            if ((d1 == d2) || (d1 == d3) || (d2 == d3))
            {
                throw new Exception("Repeated digits");
            }
            
            if ((d1 != 11) && ((d1 < 2) || (d1 > 9)) ||
                (d2 != 11) && ((d2 < 2) || (d2 > 9)) || 
                (d3 != 11) && ((d3 < 2) || (d3 > 9))
               )
            {
                throw new Exception("Invalid digits");
            }

            bool needCheckJoker = ((d1 == 11) || (d2 == 11) || (d3 == 11));

            if (needCheckJoker)
            {
                if (!CanUseJoker(isFirstGamer))
                {
                    throw new Exception("Cannot use joker");
                }
            }

            switch (roundNumber)
            {
                case 1:
                    if (isFirstGamer)
                    {
                        D1_1_1 = d1;
                        D1_1_2 = d2;
                        D1_1_3 = d3;
                        User1MaxRoundNumber = 1;
                    }
                    else
                    {
                        D1_2_1 = d1;
                        D1_2_2 = d2;
                        D1_2_3 = d3;
                        User2MaxRoundNumber = 1;
                    }

                    break;
                case 2:
                    if (isFirstGamer)
                    {
                        D2_1_1 = d1;
                        D2_1_2 = d2;
                        D2_1_3 = d3;
                        User1MaxRoundNumber = 2;
                    }
                    else
                    {
                        D2_2_1 = d1;
                        D2_2_2 = d2;
                        D2_2_3 = d3;
                        User2MaxRoundNumber = 2;
                    }

                    break;
                case 3:
                    if (isFirstGamer)
                    {
                        D3_1_1 = d1;
                        D3_1_2 = d2;
                        D3_1_3 = d3;
                        User1MaxRoundNumber = 3;
                    }
                    else
                    {
                        D3_2_1 = d1;
                        D3_2_2 = d2;
                        D3_2_3 = d3;
                        User2MaxRoundNumber = 3;
                    }

                    break;
                case 4:
                    if (isFirstGamer)
                    {
                        D4_1_1 = d1;
                        D4_1_2 = d2;
                        D4_1_3 = d3;
                        User1MaxRoundNumber = 4;
                    }
                    else
                    {
                        D4_2_1 = d1;
                        D4_2_2 = d2;
                        D4_2_3 = d3;
                        User2MaxRoundNumber = 4;
                    }

                    break;
                case 5:
                    if (isFirstGamer)
                    {
                        D5_1_1 = d1;
                        D5_1_2 = d2;
                        D5_1_3 = d3;
                        User1MaxRoundNumber = 5;
                    }
                    else
                    {
                        D5_2_1 = d1;
                        D5_2_2 = d2;
                        D5_2_3 = d3;
                        User2MaxRoundNumber = 5;
                    }
                    break;
            }
        }



        public bool CanPlayRound(bool isFirstUser, int roundNumber)
        {
            switch (roundNumber)
            {
                case 1:
                    if (isFirstUser)
                    {
                        return (User1MaxRoundNumber == 0) && ((User2MaxRoundNumber == 0) || (User2MaxRoundNumber == 1));
                    }
                    else
                    {
                        return (User2MaxRoundNumber == 0) && ((User1MaxRoundNumber == 0) || (User1MaxRoundNumber == 1));
                    }
                case 2:
                    if (isFirstUser)
                    {
                        return (User1MaxRoundNumber == 1) && ((User2MaxRoundNumber == 1) || (User2MaxRoundNumber == 2));
                    }
                    else
                    {
                        return (User2MaxRoundNumber == 1) && ((User1MaxRoundNumber == 1) || (User1MaxRoundNumber == 2));
                    }
                case 3:
                    if (isFirstUser)
                    {
                        return (User1MaxRoundNumber == 2) && ((User2MaxRoundNumber == 2) || (User2MaxRoundNumber == 3));
                    }
                    else
                    {
                        return (User2MaxRoundNumber == 2) && ((User1MaxRoundNumber == 2) || (User1MaxRoundNumber == 3));
                    }
                case 4:
                    if (isFirstUser)
                    {
                        return (User1MaxRoundNumber == 3) && ((User2MaxRoundNumber == 3) || (User2MaxRoundNumber == 4));
                    }
                    else
                    {
                        return (User2MaxRoundNumber == 3) && ((User1MaxRoundNumber == 3) || (User1MaxRoundNumber == 4));
                    }
                case 5:
                    if (isFirstUser)
                    {
                        return (User1MaxRoundNumber == 4) && ((User2MaxRoundNumber == 4) || (User2MaxRoundNumber == 5));
                    }
                    else
                    {
                        return (User2MaxRoundNumber == 4) && ((User1MaxRoundNumber == 4) || (User1MaxRoundNumber == 5));
                    }
            }

            return false;
        }

        private int GetScore(int digitUser1, int digitUser2)
        {
            if ((digitUser1 != 11) && ((digitUser1 < 2) || (digitUser1 > 9))) return 0;
            if ((digitUser2 != 11) && ((digitUser2 < 2) || (digitUser2 > 9))) return 0;

            if ((digitUser1 > digitUser2) && (digitUser1 % digitUser2 != 0)) return digitUser1 % digitUser2;
            if ((digitUser1 < digitUser2) && (digitUser2 % digitUser1 == 0)) return (int)(digitUser2 / digitUser1);

            return 0;
        }

        private int GetUser1Score()
        {
            return GetScore(D1_1_1, D1_2_1) +
                   GetScore(D1_1_2, D1_2_2) +
                   GetScore(D1_1_3, D1_2_3) +
                   GetScore(D2_1_1, D2_2_1) +
                   GetScore(D2_1_2, D2_2_2) +
                   GetScore(D2_1_3, D2_2_3) +
                   GetScore(D3_1_1, D3_2_1) +
                   GetScore(D3_1_2, D3_2_2) +
                   GetScore(D3_1_3, D3_2_3) +
                   GetScore(D4_1_1, D4_2_1) +
                   GetScore(D4_1_2, D4_2_2) +
                   GetScore(D4_1_3, D4_2_3) +
                   GetScore(D5_1_1, D5_2_1) +
                   GetScore(D5_1_2, D5_2_2) +
                   GetScore(D5_1_3, D5_2_3);
        }

        private int GetUser2Score()
        {
            return GetScore(D1_2_1, D1_1_1) +
                   GetScore(D1_2_2, D1_1_2) +
                   GetScore(D1_2_3, D1_1_3) +
                   GetScore(D2_2_1, D2_1_1) +
                   GetScore(D2_2_2, D2_1_2) +
                   GetScore(D2_2_3, D2_1_3) +
                   GetScore(D3_2_1, D3_1_1) +
                   GetScore(D3_2_2, D3_1_2) +
                   GetScore(D3_2_3, D3_1_3) +
                   GetScore(D4_2_1, D4_1_1) +
                   GetScore(D4_2_2, D4_1_2) +
                   GetScore(D4_2_3, D4_1_3) +
                   GetScore(D5_2_1, D5_1_1) +
                   GetScore(D5_2_2, D5_1_2) +
                   GetScore(D5_2_3, D5_1_3);
        }

    }
}