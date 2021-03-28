using Microsoft.AspNetCore.Mvc;

namespace ModuloGameServer.Controllers
{
    public partial class ApiController : Controller
    {

        /// <summary>
        /// Получить правило показа рекламы
        /// </summary>
        public string GetADRule()
        {
            return "Error";
        }

        /// <summary>
        /// Получить следующую рекламу и время её действия
        /// </summary>
        public string GetNextAD()
        {
            return "Error";
        }

        /// <summary>
        /// Получить внеочередную рекламу для зарабатывания монет
        /// </summary>
        public string GetMoreAD()
        {
            return "Error";
        }


    }
}
