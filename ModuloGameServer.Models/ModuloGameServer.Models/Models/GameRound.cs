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



    }
}
