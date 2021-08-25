using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModuloGameServer.Models;
using ModuloGameServer.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        /// Получить полные данные о конкретной партии
        /// </summary>
        public async Task<string> GetGameInfo([FromBody] RequestGame rg, CancellationToken cancellationToken)
        {
            try
            {
                if (rg == null) return await JsonErrorAsync("No request");
                if (!rg.Id.HasValue) return await JsonErrorAsync("Bad request");

                CheckedUser cu = await CheckCurrentUser(rg.DeviceWorkToken, cancellationToken);
                if (cu.ErrorString != null) return cu.ErrorString;

                Game game = await DBService.DataSourceGame.GetGame(rg.Id.Value, cancellationToken);
                if (game == null) return await JsonErrorAsync("Game not found");

                AnswerUserGame ag = new AnswerUserGame(game, cu.User.Id);
                return JsonConvert.SerializeObject(ag);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message + Environment.NewLine + e.StackTrace);
                return await JsonErrorAsync("Server Error");
            }
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
                    User1Id = cu.User.Id,
                    User1MaxRoundNumber = 0,
                    User2MaxRoundNumber = 0,
                    IsUser2Bot = competitor?.IsBot ?? false
                };

                

                if (competitor != null) newGame.User2Id = competitor.Id;

                await DBService.DataSourceGame.AddGame(newGame, cancellationToken);
                if (newGame.Id < 0)
                {
                    return await JsonErrorAsync("Game didn't be created");
                }



                Game game = await DBService.DataSourceGame.GetGame(newGame.Id, cancellationToken);
                if (game == null) return await JsonErrorAsync("Game not found");
 
                if (game.User2?.IsBot == true)
                {

                    //Task botPlaying = new Task(delegate()
                    //{
                    //    Thread.Sleep(1000);
                    Bot bot = await DBService.DataSourceBot.GetBot(game.User2Id.Value, cancellationToken);

                    if (await BotService.StartGame(bot, game, cancellationToken))
                    {
                        Logger.LogInformation($"Start game between bot {bot.BotId} and user {game.User1Id}");

                        game.UpdateStatus();

                        await DBService.DataSourceGame.ChangeGame(game, cancellationToken);
                    }
                    //});
                }



                AnswerUserGame ag = new AnswerUserGame(newGame, cu.User.Id);
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
        public async Task<string> AcceptGame([FromBody] RequestGame rg, CancellationToken cancellationToken)
        {
            try
            {
                if (rg == null) return await JsonErrorAsync("No request");
                if (!rg.Id.HasValue) return await JsonErrorAsync("Bad request");

                CheckedUser cu = await CheckCurrentUser(rg.DeviceWorkToken, cancellationToken);
                if (cu.ErrorString != null) return cu.ErrorString;

                Game game = await DBService.DataSourceGame.GetGame(rg.Id.Value, cancellationToken);
                if (game == null) return await JsonErrorAsync("Game not found");

                if (game.User2Id != cu.User.Id) return await JsonErrorAsync("Bad command"); 

                if (game.IsCancel) return await JsonErrorAsync("Game was canceled");
                if (game.IsDeclined) return await JsonErrorAsync("Game was already declined");
                if (game.IsStart) return await JsonErrorAsync("Game was already accepted");
                if (game.IsTimeout) return await JsonErrorAsync("Game was canceled by timeout");

                game.IsStart = true;
                await DBService.DataSourceGame.ChangeGame(game, cancellationToken);

                AnswerUserGame ag = new AnswerUserGame(game, cu.User.Id);
                return JsonConvert.SerializeObject(ag);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message + Environment.NewLine + e.StackTrace);
                return await JsonErrorAsync("Server Error");
            }
        }

        /// <summary>
        /// Отказаться от предложения партии
        /// </summary>
        public async Task<string> DeclineGame([FromBody] RequestGame rg, CancellationToken cancellationToken)
        {
            try
            {
                if (rg == null) return await JsonErrorAsync("No request");
                if (!rg.Id.HasValue) return await JsonErrorAsync("Bad request");

                CheckedUser cu = await CheckCurrentUser(rg.DeviceWorkToken, cancellationToken);
                if (cu.ErrorString != null) return cu.ErrorString;

                Game game = await DBService.DataSourceGame.GetGame(rg.Id.Value, cancellationToken);
                if (game == null) return await JsonErrorAsync("Game not found");

                if (game.User2Id != cu.User.Id) return await JsonErrorAsync("Bad command");

                if (game.IsCancel) return await JsonErrorAsync("Game was canceled");
                if (game.IsDeclined) return await JsonErrorAsync("Game was already declined");
                if (game.IsStart) return await JsonErrorAsync("Game was already accepted");
                if (game.IsTimeout) return await JsonErrorAsync("Game was canceled by timeout");

                game.IsDeclined = true;
                await DBService.DataSourceGame.ChangeGame(game, cancellationToken);

                AnswerUserGame ag = new AnswerUserGame(game, cu.User.Id);
                return JsonConvert.SerializeObject(ag);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message + Environment.NewLine + e.StackTrace);
                return await JsonErrorAsync("Server Error");
            }
        }


        /// <summary>
        /// Отозвать приглашение 
        /// </summary>
        public async Task<string> WithdrawGame([FromBody] RequestGame rg, CancellationToken cancellationToken)
        {
            try
            {
                if (rg == null) return await JsonErrorAsync("No request");
                if (!rg.Id.HasValue) return await JsonErrorAsync("Bad request");

                CheckedUser cu = await CheckCurrentUser(rg.DeviceWorkToken, cancellationToken);
                if (cu.ErrorString != null) return cu.ErrorString;

                Game game = await DBService.DataSourceGame.GetGame(rg.Id.Value, cancellationToken);
                if (game == null) return await JsonErrorAsync("Game not found");

                if (game.User1Id != cu.User.Id) return await JsonErrorAsync("Bad command");

                if (game.IsCancel) return await JsonErrorAsync("Game was canceled");
                if (game.IsDeclined) return await JsonErrorAsync("Game was already declined");
                if (game.IsStart) return await JsonErrorAsync("Game was already accepted");
                if (game.IsTimeout) return await JsonErrorAsync("Game was canceled by timeout");

                game.IsCancel = true;
                await DBService.DataSourceGame.ChangeGame(game, cancellationToken);

                AnswerUserGame ag = new AnswerUserGame(game, cu.User.Id);
                return JsonConvert.SerializeObject(ag);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message + Environment.NewLine + e.StackTrace);
                return await JsonErrorAsync("Server Error");
            }
        }


        /// <summary>
        /// Сделать один ход
        /// </summary>
        public async Task<string> PlayRound([FromBody] RequestGame rg, CancellationToken cancellationToken)
        {
            try
            {
                if (rg == null) return await JsonErrorAsync("No request");
                if (!rg.Id.HasValue) return await JsonErrorAsync("Bad request");

                CheckedUser cu = await CheckCurrentUser(rg.DeviceWorkToken, cancellationToken);
                if (cu.ErrorString != null) return cu.ErrorString;

                Game game = await DBService.DataSourceGame.GetGame(rg.Id.Value, cancellationToken);
                if (game == null) return await JsonErrorAsync("Game not found");

                bool isFirstGamer;

                if (game.User1Id == cu.User.Id)
                {
                    isFirstGamer = true;
                }
                else if (game.User2Id == cu.User.Id)
                {
                    isFirstGamer = false;
                }
                else
                {
                    return await JsonErrorAsync("Bad gamer");
                }

                if (!game.IsStart) return await JsonErrorAsync("Game wasn't accepted yet");

                if (game.IsCancel) return await JsonErrorAsync("Game was canceled");
                if (game.IsGiveUp) return await JsonErrorAsync("Game was gave up");
                if (game.IsDeclined) return await JsonErrorAsync("Game was already declined");
                if (game.IsTimeout) return await JsonErrorAsync("Game was canceled by timeout");
                if (game.IsFinish) return await JsonErrorAsync("Game was already finished");


                try
                {
                    game.PlayRound(isFirstGamer, rg.RoundNumber.Value, rg.Digit1.Value, rg.Digit2.Value, rg.Digit3.Value);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return await JsonErrorAsync("Game error during playing round");
                }


                await DBService.DataSourceGame.PlayRound(game, cancellationToken);
                
                game = await DBService.DataSourceGame.GetGame(rg.Id.Value, cancellationToken);

                if (game.User2?.IsBot == true)
                {

                    //Task botPlaying = new Task(delegate()
                    //{
                    //    Thread.Sleep(1000);
                        Bot bot = await DBService.DataSourceBot.GetBot(game.User2Id.Value, cancellationToken);

                        if (await BotService.PlayRound(bot, game, cancellationToken))
                        {
                            Logger.LogInformation($"Start game between bot {bot.BotId} and user {game.User1Id}");

                            game.UpdateStatus();

                            await DBService.DataSourceGame.ChangeGame(game, cancellationToken);
                        }
                    //});
                }


                AnswerUserGame ag = new AnswerUserGame(game, cu.User.Id);
                return JsonConvert.SerializeObject(ag);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message + Environment.NewLine + e.StackTrace);
                return await JsonErrorAsync("Server Error");
            }
        }

        /// <summary>
        /// Сдаться
        /// </summary>
        public async Task<string> PlayRoundGiveUp([FromBody] RequestGame rg, CancellationToken cancellationToken)
        {
            try
            {
                if (rg == null) return await JsonErrorAsync("No request");
                if (!rg.Id.HasValue) return await JsonErrorAsync("Bad request");

                CheckedUser cu = await CheckCurrentUser(rg.DeviceWorkToken, cancellationToken);
                if (cu.ErrorString != null) return cu.ErrorString;

                Game game = await DBService.DataSourceGame.GetGame(rg.Id.Value, cancellationToken);
                if (game == null) return await JsonErrorAsync("Game not found");

                bool isFirstGamer;

                if (game.User1Id == cu.User.Id)
                {
                    isFirstGamer = true;
                }
                else if (game.User2Id == cu.User.Id)
                {
                    isFirstGamer = false;
                }
                else
                {
                    return await JsonErrorAsync("Bad gamer");
                }

                if (!game.IsStart) return await JsonErrorAsync("Game wasn't accepted yet");

                if (game.IsCancel) return await JsonErrorAsync("Game was canceled");
                if (game.IsGiveUp) return await JsonErrorAsync("Game was gave up");
                if (game.IsDeclined) return await JsonErrorAsync("Game was already declined");
                if (game.IsTimeout) return await JsonErrorAsync("Game was canceled by timeout");
                if (game.IsFinish) return await JsonErrorAsync("Game was already finished");



                bool canPlayRound = (
                    (game.Status == GAME_STATUS.GAME_ROUND_1_NOUSER) ||
                    (game.Status == GAME_STATUS.GAME_ROUND_2_NOUSER) ||
                    (game.Status == GAME_STATUS.GAME_ROUND_3_NOUSER) ||
                    (game.Status == GAME_STATUS.GAME_ROUND_4_NOUSER) ||
                    (game.Status == GAME_STATUS.GAME_ROUND_5_NOUSER)
                );

                if (isFirstGamer && !canPlayRound)
                {
                    canPlayRound = (game.Status == GAME_STATUS.GAME_ROUND_1_USER2_DONE) ||
                                   (game.Status == GAME_STATUS.GAME_ROUND_2_USER2_DONE) ||
                                   (game.Status == GAME_STATUS.GAME_ROUND_3_USER2_DONE) ||
                                   (game.Status == GAME_STATUS.GAME_ROUND_4_USER2_DONE) ||
                                   (game.Status == GAME_STATUS.GAME_ROUND_5_USER2_DONE);

                }
                else if (!isFirstGamer && !canPlayRound)
                {
                    canPlayRound = (game.Status == GAME_STATUS.GAME_ROUND_1_USER1_DONE) ||
                                   (game.Status == GAME_STATUS.GAME_ROUND_2_USER1_DONE) ||
                                   (game.Status == GAME_STATUS.GAME_ROUND_3_USER1_DONE) ||
                                   (game.Status == GAME_STATUS.GAME_ROUND_4_USER1_DONE) ||
                                   (game.Status == GAME_STATUS.GAME_ROUND_5_USER1_DONE);
                }

                if (!canPlayRound) return await JsonErrorAsync("Cannot play round");


                if (isFirstGamer)
                {
                    game.IsUser1GiveUp = true;
                }
                else
                {
                    game.IsUser2GiveUp = true;
                }

                await DBService.DataSourceGame.PlayRound(game, cancellationToken);
                game = await DBService.DataSourceGame.GetGame(rg.Id.Value, cancellationToken);
                AnswerUserGame ag = new AnswerUserGame(game, cu.User.Id);
                return JsonConvert.SerializeObject(ag);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message + Environment.NewLine + e.StackTrace);
                return await JsonErrorAsync("Server Error");
            }
        }


    }
}
