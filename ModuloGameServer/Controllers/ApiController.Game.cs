using Microsoft.AspNetCore.Mvc;

namespace ModuloGameServer.Controllers
{
    public partial class ApiController : Controller
    {
        /// <summary>
        /// Получить список активных партий или партий ожидающих решение или выбор участников 
        /// или законченная, но не закрытая партия
        /// </summary>
        public string GetActiveGames()
        {
            return "Error";
        }

        /// <summary>
        /// Получить данные о партии
        /// </summary>
        public string GetGameInfo()
        {
            return "Error";
        }

        /// <summary>
        /// Получить список завершённых партий
        /// </summary>
        public string GetHistoryGames()
        {
            return "Error";
        }


        /// <summary>
        /// Создать партию, в том числе в выбранным соперником
        /// Если соперник не выбран, партия переходит в режим Ожидание
        /// </summary>
        public string CreateGame()
        {
            return "Error";
        }

        /// <summary>
        /// Принять предложение партии
        /// </summary>
        public string AcceptGame()
        {
            return "Error";
        }

        /// <summary>
        /// Отказаться от предложения партии
        /// </summary>
        public string RefuseGame()
        {
            return "Error";
        }

        /// <summary>
        /// Завершить партию и отозвать приглашение при его наличии
        /// </summary>
        public string CancelGame()
        {
            return "Error";
        }


        /// <summary>
        /// Сделать один ход
        /// </summary>
        public string DoRound()
        {
            return "Error";
        }

        /// <summary>
        /// Сдаться
        /// </summary>
        public string GiveUp()
        {
            return "Error";
        }

    }
}
