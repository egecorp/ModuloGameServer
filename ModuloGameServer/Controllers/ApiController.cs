using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModuloGameServer.Contracts;
using ModuloGameServer.Models;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ModuloGameServer.Controllers
{
    [EnableCors("ApiPolicy")]
    public partial class ApiController : Controller
    {
        private readonly ILogger Logger;

        private IModuloGameDBService DBService;

        protected IModuloGameBotService BotService;

        public ApiController(IModuloGameDBService _DBService, ILogger<ApiController> logger, IModuloGameBotService botService)
        {
            DBService = _DBService;
            Logger = logger;
            BotService = botService;
        }

        public string DoIt()
        {
            return "Done it";
        }


        private static string JsonOk()
        {
            return @"{""Result"":""Ok""}";
        }
        private static string JsonError(string err, bool isWorkflowError = false)
        {
            return JsonConvert.SerializeObject(new { Error = err, IsWorkflowError = isWorkflowError});
        }



        private async static Task<string> JsonOkAsync()
        {
            return await Task.FromResult<string>(JsonOk());
        }
        private async static Task<string> JsonErrorAsync(string err, bool isWorkflowError = false)
        {
            return await Task.FromResult<string>(JsonError(err, isWorkflowError));
        }
        private async static Task<string> JsonErrorAsync(SERVER_ERROR err, bool isWorkflowError = false)
        {
            return await Task.FromResult<string>(JsonError(err.ToString("g"), isWorkflowError));
        }

        /// <summary>
        /// Получить и проверить текущего пользователя по рабочему токену 
        /// </summary>
        /// <param name="WorkToken">Токен</param>
        /// <param name="cancellationToken">Cancellation токен</param>
        /// <returns>Объект пользователя или текст ошибки CheckedUser</returns>
        public async Task<CheckedUser> CheckCurrentUser(string WorkToken, CancellationToken cancellationToken)
        {
            CheckedUser result = new CheckedUser();
            int? deviceId = Tokens.GetDeviceId(WorkToken);

            if (!deviceId.HasValue)
            {
                 
                result.ErrorString = await JsonErrorAsync("Access denied");
                return result;
            }

            Device existDevice = await DBService.DataSourceDevice.GetDevice(deviceId.Value, cancellationToken);

            if (existDevice == null)
            {
                result.ErrorString = await JsonErrorAsync("Access denied");
                return result;
            }

            if (existDevice.IsDisabled == true)
            {
                result.ErrorString = await JsonErrorAsync("Device is disabled");
                return result;
            }
            if (!existDevice.UserId.HasValue)
            {
                result.ErrorString = await JsonErrorAsync("No user");
                return result;
            }

            result.User = await DBService.DataSourceUser.GetUserInfo(existDevice.UserId.Value, cancellationToken);

            if (result.User == null)
            {
                result.ErrorString = await JsonErrorAsync("User not found");
                return result;
            }

            result.DynamicUserInfo = result.User.DynamicUserInfo;

            return result;
        }
    }
}
