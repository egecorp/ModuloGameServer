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
        private const int DEFAULT_ROUND_TIME = 60 * 48;
        /// <summary>
        /// Получить список активных партий или партий ожидающих решение или выбор участников 
        /// или законченная, но не закрытая партия
        /// </summary>
        public async Task<string> GetGameList([FromBody] RequestDevice rd, CancellationToken cancellationToken)
        {
            try
            {
                if (rd == null) return await JsonErrorAsync("No request");
                CheckedUser cu = await CheckCurrentUser(rd.WorkToken, cancellationToken);
                if (cu.ErrorString != null) return cu.ErrorString;

                if (cu.DynamicUserInfo == null)
                {
                    return await JsonErrorAsync("UserInfo not found");
                }

                AnswerUser au = new AnswerUser(cu.User);

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
        /// Если соперник не выбран, партия переходит в режим Ожидание (IsActive = false)
        /// </summary>
        public async Task<string> CreateGame([FromBody] RequestGame rg, CancellationToken cancellationToken)
        {
            try
            {
                if (rg == null) return await JsonErrorAsync("No request");

                CheckedUser cu = await CheckCurrentUser(rg.DeviceWorkToken, cancellationToken);
                if (cu.ErrorString != null) return cu.ErrorString;

                User competitor = null;

                if (!rg.IsRandomCompetitor && !rg.CompetitorUserId.HasValue) return await JsonErrorAsync("No CompetitorUserId");
                if (rg.CompetitorUserId.HasValue)
                {
                    competitor = await DBService.DataSourceUser.GetUser(rg.CompetitorUserId.Value, cancellationToken);
                    if (competitor == null) return await JsonErrorAsync("Competitive user not found");


                }

                Game newGame = new Game()
                {
                    MinutesPerRound = DEFAULT_ROUND_TIME,
                    StartStamp = DateTime.Now,
                    User1Id = cu.User.Id
                };

                if (competitor != null) newGame.User2Id = competitor.Id;

                await DBService.DataSourceGame.AddGame(newGame, cancellationToken);
                if (newGame.Id < 0)
                {
                    return await JsonErrorAsync("Game didn't be created");
                }

                AnswerGame ag = new AnswerGame(newGame);
                return JsonConvert.SerializeObject(ag);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message + Environment.NewLine + e.StackTrace);
                return await JsonErrorAsync("Server Error");
            }
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
