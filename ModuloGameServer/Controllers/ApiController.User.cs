using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModuloGameServer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ModuloGameServer.Controllers
{
    public partial class ApiController : Controller
    {

        /// <summary>
        /// Зарегистрировать новое устройство и вернуть рабочий токен
        /// </summary>
        public async Task<string> RegisterDevice([FromBody] RequestDevice rd, CancellationToken cancellationToken)
        {
            try
            {
                if (rd == null) return await JsonErrorAsync("No request");


                if (await DBService.DataSourceDevice.CheckDeviceName(rd.DeviceToken, cancellationToken))
                {
                    return await JsonErrorAsync("Invalid data");
                }

                Device _newDevice = new Device()
                {
                    Caption = rd.Caption ?? "New Device",
                    DeviceToken = rd.DeviceToken,
                    ServerToken = Tokens.GenerateToken()
                };

                bool result = await DBService.DataSourceDevice.AddDevice(_newDevice, cancellationToken);

                Device newDevice = await DBService.DataSourceDevice.GetDevice(_newDevice.DeviceToken, cancellationToken);

                if (newDevice == null) return await JsonErrorAsync("Cannot get new device");

                AnswerDevice ad = new AnswerDevice(newDevice);
                string workToken = Tokens.AddDevice(ad);


                return JsonConvert.SerializeObject(ad);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return await JsonErrorAsync("Server Error");
            }
        }

        /// <summary>
        /// Получить временный токен для авторизации устройства
        /// </summary>
        public async Task<string> GetWorkToken([FromBody] RequestDevice rd, CancellationToken cancellationToken)
        {
            try
            {
                if (rd == null) return await JsonErrorAsync("No request");

                Device existDevice = await DBService.DataSourceDevice.GetDevice(rd.DeviceToken, cancellationToken);

                if (existDevice == null) return await JsonErrorAsync("Access denied");

                if (existDevice.IsDisabled == true) return await JsonErrorAsync("Device is disabled");

                AnswerDevice ad = new AnswerDevice(existDevice);

                string workToken = Tokens.AddDevice(ad);

                Thread.Sleep(2000);

                return JsonConvert.SerializeObject(ad);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return await JsonErrorAsync("Server Error");
            }


        }

        /// <summary>
        /// Получение информации о пользователе, связанном с устройством
        /// </summary>
        public async Task<string> GetUserInfo([FromBody] RequestDevice rd, CancellationToken cancellationToken)
        {
            try
            {
                if (rd == null) return await JsonErrorAsync("No request");

                int? deviceId = Tokens.GetDeviceId(rd.WorkToken);

                if (!deviceId.HasValue) return await JsonErrorAsync("Access denied");

                Device existDevice = await DBService.DataSourceDevice.GetDevice(deviceId.Value, cancellationToken);

                if (existDevice == null) return await JsonErrorAsync("Access denied");

                if (existDevice.IsDisabled == true) return await JsonErrorAsync("Device is disabled");

                if (!existDevice.UserId.HasValue) return await JsonErrorAsync("No user");

                User u = await DBService.DataSourceUser.GetUserInfo(existDevice.UserId.Value, cancellationToken);

                if (u == null) return await JsonErrorAsync("User not found");

                if (u.DynamicUserInfo == null)
                {
                    return await JsonErrorAsync("UserInfo not found");
                }

                AnswerUser au = new AnswerUser(u);

                return JsonConvert.SerializeObject(au);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message + Environment.NewLine + e.StackTrace);
                return await JsonErrorAsync("Server Error");
            }

        }

        /// <summary>
        /// Создать анонимного пользователя 
        /// </summary>
        public async Task<string> CreateAnonimUser([FromBody] RequestUser ru, CancellationToken cancellationToken)
        {
            await this.DBService.BeginTransaction(cancellationToken);

            try
            {
                if (ru == null) return await JsonErrorAsync("No request");

                int? deviceId = Tokens.GetDeviceId(ru.DeviceWorkToken);

                if (!deviceId.HasValue) return await JsonErrorAsync("Access denied");

                Device existDevice = await DBService.DataSourceDevice.GetDevice(deviceId.Value, cancellationToken);

                if (existDevice == null) return await JsonErrorAsync("Access denied");

                if (existDevice.IsDisabled == true) return await JsonErrorAsync("Device is disabled");

                if (existDevice.UserId.HasValue) return await JsonErrorAsync("Device already bind to user");


                User u = new User()
                {
                    IsAnonim = true
                };
                await DBService.DataSourceUser.AddUser(u, cancellationToken);

                if (u.Id <= 0) return await JsonErrorAsync("DataBase ERROR");

                u.NicName = "Anonim" + u.Id.ToString();

                await DBService.DataSourceUser.ChangeUser(u, cancellationToken);

                existDevice.UserId = u.Id;

                await DBService.DataSourceDevice.ChangeDevice(existDevice, cancellationToken);

                AnswerUser au = new AnswerUser(u);

                await this.DBService.CoommitTransaction(cancellationToken);

                return JsonConvert.SerializeObject(au);

            }
            catch (Exception e)
            {
                Logger.LogError(e.Message + Environment.NewLine + e.StackTrace);
                await this.DBService.RollbackTransaction(cancellationToken);
                return await JsonErrorAsync("Server Error");
            }
        }

        /// <summary>
        /// Создать пользователя с почтовым ящиком, зарегистрировать устройство
        /// и отправить код авторизации
        /// Возвращается постоянный токен устройства
        /// </summary>
        public async Task<string> CreateVerifiedUser([FromBody] RequestUser ru, CancellationToken cancellationToken)
        {
            await this.DBService.BeginTransaction(cancellationToken);

            try
            {
                if (ru == null) return await JsonErrorAsync(SERVER_ERROR.BAD_DATA);

                int? deviceId = Tokens.GetDeviceId(ru.DeviceWorkToken);

                if (!deviceId.HasValue) return await JsonErrorAsync(SERVER_ERROR.ACCESS_ERROR);

                Device existDevice = await DBService.DataSourceDevice.GetDevice(deviceId.Value, cancellationToken);

                if (existDevice == null) return await JsonErrorAsync(SERVER_ERROR.ACCESS_ERROR);

                if (existDevice.IsDisabled == true) return await JsonErrorAsync(SERVER_ERROR.ACCESS_ERROR);

                if (existDevice.UserId.HasValue) return await JsonErrorAsync(SERVER_ERROR.SIGNUP_ALREADY_BOUND);

                User u = new User()
                {
                    IsAnonim = false,
                    EMail = ru.EMail.ToLower(),
                    TNumber = ru.TNumber,
                    NicName = ru.NicName,
                    Birthday = ru.Birthday,
                };

                if (!CheckEmail(u.EMail)) return await JsonErrorAsync(SERVER_ERROR.SIGNUP_BAD_EMAIL, true);

                if ((u.NicName ?? "").Length < 2) return await JsonErrorAsync(SERVER_ERROR.SIGNUP_BAD_NICKNAME, true);


                User existsUser = await DBService.DataSourceUser.GetUserByEmail(u.EMail, cancellationToken);


                if (existsUser != null) return await JsonErrorAsync(SERVER_ERROR.SIGNUP_EMAIL_EXISTS, true);

                //FindUsersByNic


                await DBService.DataSourceUser.AddUser(u, cancellationToken);



                if (u.Id <= 0) return await JsonErrorAsync(SERVER_ERROR.SERVER_ERROR);

                u.NicName = "Anonim" + u.Id.ToString();

                await DBService.DataSourceUser.ChangeUser(u, cancellationToken);

                existDevice.UserId = u.Id;

                await DBService.DataSourceDevice.ChangeDevice(existDevice, cancellationToken);

                AnswerUser au = new AnswerUser(u);

                await this.DBService.CoommitTransaction(cancellationToken);

                return JsonConvert.SerializeObject(au);

            }
            catch (Exception e)
            {
                Logger.LogError(e.Message + Environment.NewLine + e.StackTrace);
                await this.DBService.RollbackTransaction(cancellationToken);
                return await JsonErrorAsync(SERVER_ERROR.SERVER_ERROR);
            }

        }


        /// <summary>
        /// Залогинить текущее устройство с почтовым ящиком пользователя
        /// и отправить код авторизации
        /// Возвращается постоянный токен устройства
        /// </summary>
        public string SignInExistUser([FromBody] RequestUser ru, CancellationToken cancellationToken)
        {
            return "Error";
        }

        /// <summary>
        /// Добавить почтовый ящик к простому пользователю
        /// и отправить код авторизации
        /// </summary>
        public string AnonimUserAddMail([FromBody] RequestUser ru, CancellationToken cancellationToken)
        {
            return "Error";
        }

        /// <summary>
        /// Повторить отправку кода авторизации
        /// </summary>
        public string RepeateVerifyingMail([FromBody] RequestUser ru, CancellationToken cancellationToken)
        {
            return "Error";
        }

        /// <summary>
        /// Ввести код авторизации
        /// </summary>
        public string EnterVerifyingCode([FromBody] RequestUser ru, CancellationToken cancellationToken)
        {
            return "Error";
        }

        /// <summary>
        /// Отвязать устройство от текущего пользователя, 
        /// в том числе ещё не подтверждённое
        /// </summary>
        public string SignOutVerifyingUser([FromBody] RequestDevice rd, CancellationToken cancellationToken)
        {
            return "Error";
        }



        /// <summary>
        /// Отвязать от связанного пользователя существующее устройство по его идентификатору
        /// </summary>
        public string RemoveDevice()
        {
            return "Error";
        }

        /// <summary>
        /// Изменить данные пользователя, не касающихся авторизации
        /// </summary>
        public string SetUserInfo()
        {
            return "Error";
        }



        /// <summary>
        /// Найти пользователя по нику
        /// </summary>
        public async Task<string> FindUsersByNick([FromBody] RequestUserFinder rd, CancellationToken cancellationToken)
        {
            try
            {
                if (rd == null) return await JsonErrorAsync("No request");

                int? deviceId = Tokens.GetDeviceId(rd.DeviceWorkToken);

                if (!deviceId.HasValue) return await JsonErrorAsync("Access denied");
                
                Device existDevice = await DBService.DataSourceDevice.GetDevice(deviceId.Value, cancellationToken);

                if (existDevice == null) return await JsonErrorAsync("Access denied");

                if (existDevice.IsDisabled == true) return await JsonErrorAsync("Device is disabled");

                if (!existDevice.UserId.HasValue) return await JsonErrorAsync("No user");

                User u = await DBService.DataSourceUser.GetUserInfo(existDevice.UserId.Value, cancellationToken);

                if (u == null) return await JsonErrorAsync("User not found");

                if (u.DynamicUserInfo == null)
                {
                    return await JsonErrorAsync("UserInfo not found");
                }

                List<User> userList = await DBService.DataSourceUser.FindUsersByNic(rd.NicName, cancellationToken);
                

                return JsonConvert.SerializeObject(userList);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message + Environment.NewLine + e.StackTrace);
                return await JsonErrorAsync("Server Error");
            }

        }


        private bool CheckEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            return match.Success;

        }


    }
}
