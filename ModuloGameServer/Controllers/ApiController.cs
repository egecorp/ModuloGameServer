using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ModuloGameServer.Contracts;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ModuloGameServer.Controllers
{
    [EnableCors("ApiPolicy")]
    public partial class ApiController : Controller
    {
        private IModuloGameDBService DBService;
        public ApiController(IModuloGameDBService _DBService)
        {
            DBService = _DBService;
        }

        public string DoIt()
        {
            return "Done it";
        }


        private static string JsonOk()
        {
            return @"{""Result"":""Ok""}";
        }
        private static string JsonError(string err)
        {
            return JsonConvert.SerializeObject(new { Error = err });
        }



        private async static Task<string> JsonOkAsync()
        {
            return await Task.FromResult<string>(JsonOk());
        }
        private async static Task<string> JsonErrorAsync(string err)
        {
            return await Task.FromResult<string>(JsonError(err));
        }

    }
}
