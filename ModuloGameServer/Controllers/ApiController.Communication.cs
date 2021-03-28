using Microsoft.AspNetCore.Mvc;

namespace ModuloGameServer.Controllers
{
    public partial class ApiController : Controller
    {
        /// <summary>
        /// Получить список сообщений по текущей партии
        /// </summary>
        public string GetMessages()
        {
            return "Error";
        }

        /// <summary>
        /// Отправить короткое сообщение к текущей партии
        /// </summary>
        public string SendMessage()
        {
            return "Error";
        }

        /// <summary>
        /// Установить настроение в текущей партии
        /// </summary>
        public string SetMood()
        {
            return "Error";
        }

        /// <summary>
        /// Установить общее настроение (будет установлено по-умолчанию в каждой новой партии)
        /// </summary>
        public string SetGlobalMood()
        {
            return "Error";
        }

    }
}
