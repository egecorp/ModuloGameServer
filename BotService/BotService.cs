using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ModuloGameServer.Contracts;
using ModuloGameServer.Models;
using ModuloGameServer.Request.Bot;
using Newtonsoft.Json;

namespace ModuloGameServer.BotService
{
    public class BotService: IModuloGameBotService
    {


        public async Task<bool> StartGame(Bot bot, Game game, CancellationToken cancellationToken)
        {
            RequestBotCommand requestBotCommand = new RequestBotCommand()
            {
                   Game = new RequestBotGame(game),
                   Command = BotCommand.NewGame,
                   BotToken = bot.ServerToken,
                   BotRequestToken =  bot.BotRequestToken
            };

            string request = JsonConvert.SerializeObject(requestBotCommand);

            string response = await SendRequest(bot.Url, request, cancellationToken);


            // TODO Заменить на корректную реализацию
            
            game.IsStart = true;
            
            return response != null;

        }

        public async Task<bool> PlayRound(Bot bot, Game game, CancellationToken cancellationToken)
        {
            RequestBotCommand requestBotCommand = new RequestBotCommand()
            {
                Game = new RequestBotGame(game),
                Command = BotCommand.PlayRound,
                BotToken = bot.ServerToken,
                BotRequestToken = bot.BotRequestToken
            };

            string request = JsonConvert.SerializeObject(requestBotCommand);

            string response = await SendRequest(bot.Url, request, cancellationToken);


            Random r = new Random();
            int d1 = 2 + r.Next(7);
            int d2 = d1;
            int d3 = d1;
            while( d2 == d1) d2 = 2 + r.Next(7);
            while ((d3 == d1) || (d3 == d2)) d3 = 2 + r.Next(7);

            game.PlayRound(false, game.User2MaxRoundNumber + 1, d1, d2, d3);


            return response != null;
        }

        public async Task<bool> Ping(Bot bot, Game game, CancellationToken cancellationToken)
        {
            RequestBotCommand requestBotCommand = new RequestBotCommand()
            {
                Game = new RequestBotGame(game),
                Command = BotCommand.Ping,
                BotToken = bot.ServerToken,
                BotRequestToken = bot.BotRequestToken
            };

            string request = JsonConvert.SerializeObject(requestBotCommand);

            string response = await SendRequest(bot.Url, request, cancellationToken);

            return response != null;

        }



        private async Task<string> SendRequest(string uri, string request, CancellationToken cancellationToken)
        {
            // TODO убрать
            if (uri != "abrakadabra") return await Task.FromResult<string>("");

            using var client = HttpClientFactory.Create();
            
            var content = new StringContent(request, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content, cancellationToken);

            string responseString = await response?.Content?.ReadAsStringAsync();
            
            response.EnsureSuccessStatusCode();

            return responseString;


        }


    }
}
