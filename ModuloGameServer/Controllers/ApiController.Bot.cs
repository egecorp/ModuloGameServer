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
    }
}
