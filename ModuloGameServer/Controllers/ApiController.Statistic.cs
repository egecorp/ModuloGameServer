using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModuloGameServer.Models;
using ModuloGameServer.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ModuloGameServer.Controllers
{
    public partial class ApiController : Controller
    {

        /// <summary>
        /// Получить статистику пользователя
        /// </summary>
        public async Task<string> GetUserStatistic([FromBody] RequestUser ru, CancellationToken cancellationToken)
        {
            try
            {
                if (ru == null) return await JsonErrorAsync(SERVER_ERROR.BAD_DATA);
                CheckedUser cu = await CheckCurrentUser(ru.DeviceWorkToken, cancellationToken);
                if (cu.ErrorString != null) return cu.ErrorString;

                GamesAggregates resultGamesAggregates =
                    await DBService.DataSourceGame.GetGamesAggregates(cu.User.Id, null, cancellationToken);

                cu.User.DynamicUserInfo = cu.DynamicUserInfo;
                AnswerStatisticUser asu = new AnswerStatisticUser(cu.User, resultGamesAggregates, null);
                return JsonConvert.SerializeObject(asu);

            }
            catch (Exception e)
            {
                Logger.LogError(e.Message + Environment.NewLine + e.StackTrace);
                return await JsonErrorAsync(SERVER_ERROR.SERVER_ERROR);
            }
        }

        /// <summary>
        /// Получить статистику пользователя в играх с соперником
        /// </summary>
        public async Task<string> GetCompetitorStatistic([FromBody] RequestUser ru, CancellationToken cancellationToken)
        {
            try
            {
                if (ru == null) return await JsonErrorAsync(SERVER_ERROR.BAD_DATA);
                if (ru.Id <= 0) return await JsonErrorAsync(SERVER_ERROR.BAD_DATA);
                CheckedUser cu = await CheckCurrentUser(ru.DeviceWorkToken, cancellationToken);
                if (cu.ErrorString != null) return cu.ErrorString;

                User competitorUser = await DBService.DataSourceUser.GetUser(ru.Id, cancellationToken);
                if (competitorUser == null) await JsonErrorAsync(SERVER_ERROR.USER_NOT_FOUND);

                GamesAggregates resultGamesAggregates =
                    await DBService.DataSourceGame.GetGamesAggregates(cu.User.Id, competitorUser.Id, cancellationToken);

                cu.User.DynamicUserInfo = cu.DynamicUserInfo;
                AnswerStatisticCompetitor asc = new AnswerStatisticCompetitor(cu.User, competitorUser, resultGamesAggregates, null);
                return JsonConvert.SerializeObject(asc);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message + Environment.NewLine + e.StackTrace);
                return await JsonErrorAsync(SERVER_ERROR.SERVER_ERROR);
            }
        }
    }
}
