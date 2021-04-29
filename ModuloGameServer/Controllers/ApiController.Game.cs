using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModuloGameServer.Models;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ModuloGameServer.Controllers
{
    public partial class ApiController : Controller
    {
        /// <summary>
        /// Получить список активных партий или партий ожидающих решение или выбор участников 
        /// или законченная, но не закрытая партия
        /// </summary>
        public async Task<string> GetGameList([FromBody] RequestDevice rd, CancellationToken cancellationToken)
        {
            try
            {
                if (rd == null) return await JsonErrorAsync("No request");

                int? deviceId = Tokens.GetDeviceId(rd.WorkToken);

                if (!deviceId.HasValue) return await JsonErrorAsync("Access denied");

                Device existDevice = await DBService.DataSourceDevice.GetDevice(deviceId.Value, cancellationToken);

                if (existDevice == null) return await JsonErrorAsync("Access denied");

                if (existDevice.IsDisabled == true) return await JsonErrorAsync("Device is disabled");

                if (!existDevice.UserId.HasValue) return await JsonErrorAsync("No user");

                User u = await DBService.DataSourceUser.GetUserInfo(existDevice.UserId.Value, cancellationToken);

                if (u == null) return await JsonErrorAsync("User not found");

                if (u.DynamicUserInfo == null)
                {
                    return await JsonErrorAsync("UserInfo not found");
                }

                AnswerUser au = new AnswerUser(u);

                return JsonConvert.SerializeObject(au);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message + Environment.NewLine + e.StackTrace);
                return await JsonErrorAsync("Server Error");
            }

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
