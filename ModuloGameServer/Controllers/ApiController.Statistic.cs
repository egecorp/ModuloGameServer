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

                int userPosition = await DBService.DataSourceUser.GetWorldPosition(cu.User.Id, cancellationToken);

                cu.User.DynamicUserInfo = cu.DynamicUserInfo;
                AnswerStatisticUser asu = new AnswerStatisticUser(cu.User, resultGamesAggregates, userPosition, null);
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

                (List<Game> gamesList, int gamesCount) =
                    await DBService.DataSourceGame.GetGamesList(cu.User.Id, competitorUser.Id, 0, cancellationToken);

                int myPosition = await DBService.DataSourceUser.GetWorldPosition(cu.User.Id, cancellationToken);

                int competitorPosition = await DBService.DataSourceUser.GetWorldPosition(competitorUser.Id, cancellationToken);

                IEnumerable<AnswerStatisticGame> answerGamesList =
                    gamesList.Select(x => new AnswerStatisticGame(x, cu.User.Id));

                cu.User.DynamicUserInfo = cu.DynamicUserInfo;
                AnswerStatisticCompetitor asc = new AnswerStatisticCompetitor(cu.User, competitorUser,
                    resultGamesAggregates, myPosition, competitorPosition, answerGamesList, gamesCount);
                return JsonConvert.SerializeObject(asc);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message + Environment.NewLine + e.StackTrace);
                return await JsonErrorAsync(SERVER_ERROR.SERVER_ERROR);
            }
        }





        /// <summary>
        /// Получить топ-TOP_USERS_COUNT
        /// </summary>
        public async Task<string> GetTop100([FromBody] RequestUser ru, CancellationToken cancellationToken)
        {
            try
            {
                if (ru == null) return await JsonErrorAsync(SERVER_ERROR.BAD_DATA);
                CheckedUser cu = await CheckCurrentUser(ru.DeviceWorkToken, cancellationToken);
                if (cu.ErrorString != null) return cu.ErrorString;
                
                // TODO проверить, нужно ли
                cu.User.DynamicUserInfo = cu.DynamicUserInfo;

                List<User> topUsers = await DBService.DataSourceUser.GetTop(AnswerStatisticTop.TOP_USERS_COUNT, cancellationToken);


                int myPosition = await DBService.DataSourceUser.GetWorldPosition(cu.User.Id, cancellationToken) + 1;

                if (myPosition <= AnswerStatisticTop.TOP_USERS_COUNT)
                {
                    List<User> reOrderUsers = topUsers
                        .Where(x => x.DynamicUserInfo.CommonRating > cu.DynamicUserInfo.CommonRating)
                        .OrderByDescending(x => x.DynamicUserInfo.CommonRating)
                        .ThenByDescending(x => x.Id)
                        .ToList();

                    reOrderUsers.Add(cu.User);

                    reOrderUsers.AddRange(topUsers
                        .Where(x => (x.DynamicUserInfo.CommonRating <= cu.DynamicUserInfo.CommonRating)
                                    && (x.Id != cu.User.Id))
                        .OrderByDescending(x => x.DynamicUserInfo.CommonRating)
                        .ThenByDescending(x => x.Id)
                        .ToList());
                    topUsers = reOrderUsers;
                }


                AnswerStatisticTop top = new AnswerStatisticTop(myPosition, topUsers);
                return JsonConvert.SerializeObject(top);

            }
            catch (Exception e)
            {
                Logger.LogError(e.Message + Environment.NewLine + e.StackTrace);
                return await JsonErrorAsync(SERVER_ERROR.SERVER_ERROR);
            }
        }
    }
}
