using Newtonsoft.Json;

namespace ModuloGameServer.Models
{
    /// <summary>
    /// Ход одного игрока
    /// </summary>
    public class GameRound
    {

        /// <summary>
        /// Id
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// Идентификатор игры
        /// </summary>
        public int GameId { set; get; }

        /// <summary>
        /// Какой по счёту ход
        /// </summary>
        public int RoundNumber { set; get; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId { set; get; }

        /// <summary>
        /// Первая цифра
        /// </summary>
        public int Digit1 { set; get; }

        /// <summary>
        /// Вторая цифра
        /// </summary>
        public int Digit2 { set; get; }

        /// <summary>
        /// Третья цифра
        /// </summary>
        public int Digit3 { set; get; }

        /// <summary>
        /// На этом ходу пользователь сдался
        /// </summary>
        public bool UserGiveUp { set; get; }


        /// <summary>
        /// В рамках какой игры ход
        /// </summary>
        [JsonIgnore] 
        public virtual Game Game { set; get; }

        /// <summary>
        /// Какой пользователь совершает ход
        /// </summary>
        [JsonIgnore] 
        public virtual User User { set; get; }


        public int GetScore(GameRound r)
        {
            int result = 0;
            if (r == null) return result;
            if ((Digit1 > r.Digit1) && (Digit1 % r.Digit1 != 0)) result += Digit1 % r.Digit1;
            if ((Digit1 < r.Digit1) && (r.Digit1 % Digit1 == 0)) result += (int)(Digit1 / r.Digit1);

            if ((Digit2 > r.Digit2) && (Digit2 % r.Digit2 != 0)) result += Digit2 % r.Digit2;
            if ((Digit2 < r.Digit2) && (r.Digit2 % Digit2 == 0)) result += (int)(Digit2 / r.Digit2);

            if ((Digit3 > r.Digit3) && (Digit3 % r.Digit3 != 0)) result += Digit3 % r.Digit3;
            if ((Digit3 < r.Digit3) && (r.Digit3 % Digit3 == 0)) result += (int)(Digit3 / r.Digit3);

            return result;
        }
    }
}
