namespace ModuloGameServer.Models
{
    /// <summary>
    /// Результат одной игры
    /// </summary>
    public class UserResult
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
        /// Идентификатор пользователя
        /// </summary>
        public int UserId { set; get; }

        /// <summary>
        /// Пользователь выиграл
        /// </summary>
        public bool IsWin { set; get; }

        /// <summary>
        /// Пользователь сыграл вничью
        /// </summary>
        public bool IsDraw { set; get; }

        /// <summary>
        /// Количество очков за матч
        /// </summary>
        public decimal TotalCount { set; get; }


    }
}
