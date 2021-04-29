using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModuloGameServer.Contracts;
using ModuloGameServer.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ModuloGameServer.Controllers
{
    [EnableCors("ApiPolicy")]
    public partial class ApiController : Controller
    {
        private readonly ILogger Logger;

        private IModuloGameDBService DBService;
        public ApiController(IModuloGameDBService _DBService, ILogger<ApiController> logger)
        {
            DBService = _DBService;
            Logger = logger;
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


    }
}
