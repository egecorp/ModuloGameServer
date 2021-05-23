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
        /// Список ходов
        /// </summary>
        public virtual List<GameRound> Rounds { set; get; }

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



        //public virtual GameResult Result { set; get; }        

        public GAME_STATUS UpdateStatus()
        {
            this.User1MaxRoundNumber = 0;
            this.User2MaxRoundNumber = 0;
            this.User1Score = 0;
            this.User2Score = 0;
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


            if ((this.Rounds == null) || (this.Rounds.Count == 0))
            {
                if (this.IsTimeout) return this.Status = GAME_STATUS.GAME_ROUND_1_TIMEOUT;
                return this.Status = GAME_STATUS.GAME_ROUND_1_NOUSER;
            }

            this.User1MaxRoundNumber = this.Rounds.Where(x => x.UserId == this.User1Id).DefaultIfEmpty(new GameRound() { RoundNumber = 0 }).Max(x => x.RoundNumber);
            this.User2MaxRoundNumber = this.Rounds.Where(x => x.UserId == this.User2Id).DefaultIfEmpty(new GameRound() { RoundNumber = 0 }).Max(x => x.RoundNumber);

            int currentRoundNumber = Math.Max(User1MaxRoundNumber, User2MaxRoundNumber);

            GameRound user1MaxRound = this.Rounds.FirstOrDefault(x => (x.UserId == this.User1Id) && (x.RoundNumber == User1MaxRoundNumber));
            GameRound user2MaxRound = this.Rounds.FirstOrDefault(x => (x.UserId == this.User2Id) && (x.RoundNumber == User2MaxRoundNumber));

            if ((user1MaxRound?.UserGiveUp == true) || (user2MaxRound?.UserGiveUp == true)) this.IsGiveUp = true;

            this.User1Score = this.Rounds.Where(x => x.UserId == this.User1Id).Sum(x => x.GetScore(this.Rounds.SingleOrDefault(y => (y.UserId == this.User2Id) && (y.RoundNumber == x.RoundNumber))));
            this.User2Score = this.Rounds.Where(x => x.UserId == this.User2Id).Sum(x => x.GetScore(this.Rounds.SingleOrDefault(y => (y.UserId == this.User1Id) && (y.RoundNumber == x.RoundNumber))));

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
                if ((user1MaxRound?.RoundNumber == currentRoundNumber) && (user1MaxRound.UserGiveUp))
                {
                    switch (currentRoundNumber)
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
                    switch (currentRoundNumber)
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
                    if (user1MaxRound?.RoundNumber == user2MaxRound?.RoundNumber)
                    {
                        this.IsFinish = true;
                        if (this.User1Score > this.User2Score) return this.Status = GAME_STATUS.GAME_FINISH_USER1_WIN;
                        if (this.User1Score < this.User2Score) return this.Status = GAME_STATUS.GAME_FINISH_USER2_WIN;
                        return this.Status = GAME_STATUS.GAME_FINISH_USER2_DRAW;
                    }

                    if (user1MaxRound?.RoundNumber == 5) return Status = GAME_STATUS.GAME_ROUND_5_USER1_DONE;
                    if (user2MaxRound?.RoundNumber == 5) return Status = GAME_STATUS.GAME_ROUND_5_USER2_DONE;
                    return Status = GAME_STATUS.GAME_ROUND_5_NOUSER;
                case 4:
                    if (user1MaxRound?.RoundNumber == user2MaxRound?.RoundNumber) return Status = GAME_STATUS.GAME_ROUND_5_NOUSER;
                    if (user1MaxRound?.RoundNumber == 4) return Status = GAME_STATUS.GAME_ROUND_4_USER1_DONE;
                    if (user2MaxRound?.RoundNumber == 4) return Status = GAME_STATUS.GAME_ROUND_4_USER2_DONE;
                    return Status = GAME_STATUS.GAME_ROUND_4_NOUSER;
                case 3:
                    if (user1MaxRound?.RoundNumber == user2MaxRound?.RoundNumber) return Status = GAME_STATUS.GAME_ROUND_4_NOUSER;
                    if (user1MaxRound?.RoundNumber == 3) return Status = GAME_STATUS.GAME_ROUND_3_USER1_DONE;
                    if (user2MaxRound?.RoundNumber == 3) return Status = GAME_STATUS.GAME_ROUND_3_USER2_DONE;
                    return Status = GAME_STATUS.GAME_ROUND_3_NOUSER;
                case 2:
                    if (user1MaxRound?.RoundNumber == user2MaxRound?.RoundNumber) return Status = GAME_STATUS.GAME_ROUND_3_NOUSER;
                    if (user1MaxRound?.RoundNumber == 2) return Status = GAME_STATUS.GAME_ROUND_2_USER1_DONE;
                    if (user2MaxRound?.RoundNumber == 2) return Status = GAME_STATUS.GAME_ROUND_2_USER2_DONE;
                    return Status = GAME_STATUS.GAME_ROUND_2_NOUSER;
                default:
                    if (user1MaxRound?.RoundNumber == user2MaxRound?.RoundNumber) return Status = GAME_STATUS.GAME_ROUND_2_NOUSER;
                    if (user1MaxRound?.RoundNumber == 1) return Status = GAME_STATUS.GAME_ROUND_1_USER1_DONE;
                    if (user2MaxRound?.RoundNumber == 1) return Status = GAME_STATUS.GAME_ROUND_1_USER2_DONE;
                    return Status = GAME_STATUS.GAME_ROUND_1_NOUSER;
            }

        }


        public bool CanUseJoker(bool firstGamer)
        {
            if (this.Rounds == null) return false;
            List<int> digits = new List<int>();

            foreach (GameRound round in this.Rounds.Where(x => (firstGamer && (x.UserId == User1Id)) || (!firstGamer && (x.UserId == User2Id))))
            {
                if (!digits.Contains(round.Digit1)) digits.Add(round.Digit1);
                if (!digits.Contains(round.Digit2)) digits.Add(round.Digit2);
                if (!digits.Contains(round.Digit3)) digits.Add(round.Digit3);
            }


            return digits.DefaultIfEmpty(0).Sum() == 44;

        }

    }
}
