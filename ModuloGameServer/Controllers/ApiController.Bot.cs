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
        //TODO, может, перенести в другое место, типа BotService
        public const int BOT_MIN_NAME_LENGTH = 5;

        /// <summary>
        /// Зарегистрировать бота и добавить пользователя для него
        /// </summary>
        public async Task<string> RegisterBot([FromBody] RequestBot rb, CancellationToken cancellationToken)
        {
            try
            {
                if (rb == null) return await JsonErrorAsync("No request");
                
                // TODO вставить авторизацию
                /*
                if (await DBService.DataSourceDevice.CheckDeviceName(rd.DeviceToken, cancellationToken))
                {
                    return await JsonErrorAsync("Invalid data");
                }
                */

                if ((rb.NicName ?? "").Length < BOT_MIN_NAME_LENGTH) return await JsonErrorAsync("Too short NicName");

                Bot _newBot = new Bot()
                {
                    NicName = rb.NicName,
                    BotRequestToken = rb.BotRequestToken,
                    OwnerUserId = rb.OwnerUserId,
                    ServerToken = Tokens.GenerateToken()
                };

                bool result = await DBService.DataSourceBot.AddBot(_newBot, cancellationToken);

                Bot newBot = await DBService.DataSourceBot.GetBot(_newBot.BotId, cancellationToken);

                if (newBot == null) return await JsonErrorAsync("Cannot get new bot");

                AnswerBot ab = new AnswerBot(newBot);

                return JsonConvert.SerializeObject(ab);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return await JsonErrorAsync("Server Error");
            }
        }

        
        public async Task<string> CheckBots(CancellationToken cancellationToken)
        {
            // TODO вставить авторизацию

            List<Game> activeBotsGames = await DBService.DataSourceGame.GetBotsGames(cancellationToken);
            //IsUser2Bot

            List<Game> needAcceptGameList =
                activeBotsGames.Where(x => x.Status == GAME_STATUS.GAME_WAIT_USER2).ToList();

            List<Game> playGameList =
                activeBotsGames.Where(x => 
                    (x.Status == GAME_STATUS.GAME_ROUND_1_USER1_DONE) ||
                    (x.Status == GAME_STATUS.GAME_ROUND_2_USER1_DONE) ||
                    (x.Status == GAME_STATUS.GAME_ROUND_3_USER1_DONE) ||
                    (x.Status == GAME_STATUS.GAME_ROUND_4_USER1_DONE) ||
                    (x.Status == GAME_STATUS.GAME_ROUND_5_USER1_DONE) 
                ).ToList();



            foreach (Game game in needAcceptGameList)
            {
                Bot bot = await DBService.DataSourceBot.GetBot(game.User2Id.Value, cancellationToken);

                if (await BotService.StartGame(bot, game, cancellationToken))
                {
                    Logger.LogInformation($"Start game between bot {bot.BotId} and user {game.User1Id}");

                    game.UpdateStatus();

                    await DBService.DataSourceGame.ChangeGame(game, cancellationToken);
                }
            }


            foreach (Game game in playGameList)
            {
                Bot bot = await DBService.DataSourceBot.GetBot(game.User2Id.Value, cancellationToken);

                if (await BotService.PlayRound(bot, game, cancellationToken))
                {
                    Logger.LogInformation($"Play game between bot {bot.BotId} and user {game.User1Id}");

                    game.UpdateStatus();

                    await DBService.DataSourceGame.ChangeGame(game, cancellationToken);
                }
            }



            return await JsonErrorAsync("Not implemented");
        }
    }
}
