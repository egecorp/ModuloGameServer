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

                Device device = await DBService.DataSourceDevice.GetDevice(rd.DeviceToken, cancellationToken);

                if (device == null) return await JsonErrorAsync("Access denied");

                if (device.IsDisabled == true) return await JsonErrorAsync("Device is disabled");

                AnswerDevice ad = new AnswerDevice(device);

                if (device.CurrentConfirmationId.HasValue)
                {

                    UserConfirmation userConfirmation =
                        await DBService.DataSourceUserConfirmation.GetUserConfirmation(
                            device.CurrentConfirmationId.Value, cancellationToken);

                    if ((userConfirmation != null) && !userConfirmation.IsCancel &&
                        ((userConfirmation.ExpiredTime ?? DateTime.MinValue) >= DateTime.Now))
                    {
                        User user = await DBService.DataSourceUser.GetUser(userConfirmation.UserId, cancellationToken);
                        ad.WaitConfirmation = true;
                        ad.UserMail = user.EMail;
                    }
                    else
                    {
                        if (userConfirmation != null)
                        {
                            userConfirmation.IsCancel = true;
                            await DBService.DataSourceUserConfirmation.ChangeUserConfirmation(userConfirmation, cancellationToken);
                        }

                        device.CurrentConfirmationId = null;
                        await DBService.DataSourceDevice.ChangeDevice(device, cancellationToken);
                    }
                    
                }
                
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
                    IsVerified = false
                };

                if (!CheckEmail(u.EMail)) return await JsonErrorAsync(SERVER_ERROR.SIGNUP_BAD_EMAIL, true);

                if ((u.NicName ?? "").Length < 2) return await JsonErrorAsync(SERVER_ERROR.SIGNUP_BAD_NICKNAME, true);


                User existsUser = await DBService.DataSourceUser.GetUserByEmail(u.EMail, cancellationToken);


                if (existsUser != null) return await JsonErrorAsync(SERVER_ERROR.SIGNUP_EMAIL_EXISTS, true);

                //FindUsersByNic


                await DBService.DataSourceUser.AddUser(u, cancellationToken);

                if (u.Id <= 0) return await JsonErrorAsync(SERVER_ERROR.SERVER_ERROR);
                
                u.NicName = u.NicName + Models.User.NICNAME_ID_DELIMITER + u.Id.ToString("000000");

                await DBService.DataSourceUser.ChangeUser(u, cancellationToken);

                existDevice.UserId = u.Id;

                await DBService.DataSourceDevice.ChangeDevice(existDevice, cancellationToken);

                UserConfirmation userConfirmation = CreateConfirmation(existDevice, u, ConfirmationType.Registration);

                await DBService.DataSourceUserConfirmation.AddUserConfirmation(userConfirmation, cancellationToken);

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
        public async Task<string> SignInExistUser([FromBody] RequestUser ru, CancellationToken cancellationToken)
        {
            await this.DBService.BeginTransaction(cancellationToken);

            try
            {
                if (ru == null) return await JsonErrorAsync(SERVER_ERROR.BAD_DATA);

                int? deviceId = Tokens.GetDeviceId(ru.DeviceWorkToken);

                if (!deviceId.HasValue) return await JsonErrorAsync(SERVER_ERROR.ACCESS_ERROR);

                Device device = await DBService.DataSourceDevice.GetDevice(deviceId.Value, cancellationToken);

                if (device == null) return await JsonErrorAsync(SERVER_ERROR.ACCESS_ERROR);

                if (device.IsDisabled == true) return await JsonErrorAsync(SERVER_ERROR.ACCESS_ERROR);

                if (device.UserId.HasValue) return await JsonErrorAsync(SERVER_ERROR.SIGNIN_ALREADY_BOUND, true);

                if (string.IsNullOrWhiteSpace(ru.EMail)) return await JsonErrorAsync(SERVER_ERROR.SIGNUP_BAD_EMAIL, true);

                User u = await DBService.DataSourceUser.GetUserByEmail(ru.EMail, cancellationToken);
                    
                if (u == null) return await JsonErrorAsync(SERVER_ERROR.SIGNIN_USER_NOT_FOUND, true);
                if (u.IsBlocked) return await JsonErrorAsync(SERVER_ERROR.SIGNIN_USER_BLOCKED, true);
                if (u.IsBot) return await JsonErrorAsync(SERVER_ERROR.SIGNIN_BADUSER, true);
                if (u.IsAnonim) return await JsonErrorAsync(SERVER_ERROR.SIGNIN_BADUSER, true);
                //if (!u.IsVerified) return await JsonErrorAsync(SERVER_ERROR.SIGNIN_BADUSER, true);


                UserConfirmation userConfirmation = CreateConfirmation(device, u, ConfirmationType.NewDevice);

                await DBService.DataSourceUserConfirmation.AddUserConfirmation(userConfirmation, cancellationToken);

                device.CurrentConfirmationId = userConfirmation.Id;
                await DBService.DataSourceDevice.ChangeDevice(device, cancellationToken);

                AnswerDevice ad = new AnswerDevice(device);


                ad.WaitConfirmation = true;
                ad.UserMail = u.EMail;


                await this.DBService.CoommitTransaction(cancellationToken);

                return JsonConvert.SerializeObject(ad);
                
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message + Environment.NewLine + e.StackTrace);
                await this.DBService.RollbackTransaction(cancellationToken);
                return await JsonErrorAsync(SERVER_ERROR.SERVER_ERROR);
            }

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
        public async Task <string> RepeateVerifyingMail([FromBody] RequestUser ru, CancellationToken cancellationToken)
        {
            await this.DBService.BeginTransaction(cancellationToken);

            try
            {
                if (ru == null) return await JsonErrorAsync(SERVER_ERROR.BAD_DATA);

                int? deviceId = Tokens.GetDeviceId(ru.DeviceWorkToken);

                if (!deviceId.HasValue) return await JsonErrorAsync(SERVER_ERROR.ACCESS_ERROR);

                Device device = await DBService.DataSourceDevice.GetDevice(deviceId.Value, cancellationToken);

                if (device == null) return await JsonErrorAsync(SERVER_ERROR.ACCESS_ERROR);

                if (device.IsDisabled == true) return await JsonErrorAsync(SERVER_ERROR.ACCESS_ERROR);

                if (device.UserId.HasValue) return await JsonErrorAsync(SERVER_ERROR.SIGNIN_ALREADY_BOUND, true);

                if (string.IsNullOrWhiteSpace(ru.EMail)) return await JsonErrorAsync(SERVER_ERROR.SIGNUP_BAD_EMAIL, true);

                User u = await DBService.DataSourceUser.GetUserByEmail(ru.EMail, cancellationToken);

                if (u == null) return await JsonErrorAsync(SERVER_ERROR.SIGNIN_USER_NOT_FOUND, true);
                if (u.IsBlocked) return await JsonErrorAsync(SERVER_ERROR.SIGNIN_USER_BLOCKED, true);
                if (u.IsBot) return await JsonErrorAsync(SERVER_ERROR.SIGNIN_BADUSER, true);
                if (u.IsAnonim) return await JsonErrorAsync(SERVER_ERROR.SIGNIN_BADUSER, true);
                //if (!u.IsVerified) return await JsonErrorAsync(SERVER_ERROR.SIGNIN_BADUSER, true);

                if (device.CurrentConfirmationId.HasValue)
                {
                    UserConfirmation oldUserConfirmation =
                        await DBService.DataSourceUserConfirmation.GetUserConfirmation(device.CurrentConfirmationId.Value,
                            cancellationToken);

                    if ((DateTime.Now - oldUserConfirmation.SendStamp.Value).TotalMinutes <
                        UserConfirmation.MIN_REQUEST_PERIOD_MINUTES)
                        return await JsonErrorAsync(SERVER_ERROR.SIGNIN_TOO_QUICK, true);
                    


                    oldUserConfirmation.IsCancel = true;
                    
                    await DBService.DataSourceUserConfirmation.ChangeUserConfirmation(oldUserConfirmation,
                        cancellationToken);
                }

                UserConfirmation userConfirmation = CreateConfirmation(device, u, ConfirmationType.NewDevice);

                await DBService.DataSourceUserConfirmation.AddUserConfirmation(userConfirmation, cancellationToken);

                device.CurrentConfirmationId = userConfirmation.Id;
                await DBService.DataSourceDevice.ChangeDevice(device, cancellationToken);

                AnswerDevice ad = new AnswerDevice(device);

                ad.WaitConfirmation = true;
                ad.UserMail = u.EMail;

                await this.DBService.CoommitTransaction(cancellationToken);

                return JsonConvert.SerializeObject(ad);

            }
            catch (Exception e)
            {
                Logger.LogError(e.Message + Environment.NewLine + e.StackTrace);
                await this.DBService.RollbackTransaction(cancellationToken);
                return await JsonErrorAsync(SERVER_ERROR.SERVER_ERROR);
            }

        }

        /// <summary>
        /// Отменить авторизацию и отвязать от устройства
        /// </summary>
        public async Task<string> CancelVerifying([FromBody] RequestUser ru, CancellationToken cancellationToken)
        {
            await this.DBService.BeginTransaction(cancellationToken);

            try
            {
                if (ru == null) return await JsonErrorAsync(SERVER_ERROR.BAD_DATA);

                int? deviceId = Tokens.GetDeviceId(ru.DeviceWorkToken);

                if (!deviceId.HasValue) return await JsonErrorAsync(SERVER_ERROR.ACCESS_ERROR);

                Device device = await DBService.DataSourceDevice.GetDevice(deviceId.Value, cancellationToken);

                if (device == null) return await JsonErrorAsync(SERVER_ERROR.ACCESS_ERROR);

                if (device.IsDisabled == true) return await JsonErrorAsync(SERVER_ERROR.ACCESS_ERROR);

                if (device.UserId.HasValue) return await JsonErrorAsync(SERVER_ERROR.SIGNIN_ALREADY_BOUND, true);

                if (device.CurrentConfirmationId.HasValue)
                {
                    UserConfirmation oldUserConfirmation =
                        await DBService.DataSourceUserConfirmation.GetUserConfirmation(device.CurrentConfirmationId.Value,
                            cancellationToken);

                    oldUserConfirmation.IsCancel = true;

                    await DBService.DataSourceUserConfirmation.ChangeUserConfirmation(oldUserConfirmation,
                        cancellationToken);
                }

                device.CurrentConfirmationId = null;
                await DBService.DataSourceDevice.ChangeDevice(device, cancellationToken);

                AnswerDevice ad = new AnswerDevice(device);

                ad.WaitConfirmation = false;
                ad.UserMail = null;

                await this.DBService.CoommitTransaction(cancellationToken);

                return JsonConvert.SerializeObject(ad);

            }
            catch (Exception e)
            {
                Logger.LogError(e.Message + Environment.NewLine + e.StackTrace);
                await this.DBService.RollbackTransaction(cancellationToken);
                return await JsonErrorAsync(SERVER_ERROR.SERVER_ERROR);
            }

        }

        /// <summary>
        /// Ввести код авторизации
        /// </summary>
        public async Task<string> EnterVerifyingCode([FromBody] RequestUser ru, CancellationToken cancellationToken)
        {
            await this.DBService.BeginTransaction(cancellationToken);

            try
            {
                if (ru == null) return await JsonErrorAsync(SERVER_ERROR.BAD_DATA);

                int? deviceId = Tokens.GetDeviceId(ru.DeviceWorkToken);

                if (!deviceId.HasValue) return await JsonErrorAsync(SERVER_ERROR.ACCESS_ERROR);

                Device device = await DBService.DataSourceDevice.GetDevice(deviceId.Value, cancellationToken);

                if (device == null) return await JsonErrorAsync(SERVER_ERROR.ACCESS_ERROR);

                if (device.IsDisabled == true) return await JsonErrorAsync(SERVER_ERROR.ACCESS_ERROR);

                // TODO добавить случай ввода кода для первого устройства
                if (device.UserId.HasValue) return await JsonErrorAsync(SERVER_ERROR.SIGNIN_ALREADY_BOUND, true);

                if (string.IsNullOrWhiteSpace(ru.ConfirmationCode)) return await JsonErrorAsync(SERVER_ERROR.SIGNIN_BADCODE, true);

                if (!device.CurrentConfirmationId.HasValue) return await JsonErrorAsync(SERVER_ERROR.SIGNIN_BADUSER, true);
                
                UserConfirmation userConfirmation =
                    await DBService.DataSourceUserConfirmation.GetUserConfirmation(device.CurrentConfirmationId.Value,
                        cancellationToken);

                if ((userConfirmation == null) || userConfirmation.IsCancel) return await JsonErrorAsync(SERVER_ERROR.SIGNIN_BADUSER, true);

                if (userConfirmation.IsConfirm) return await JsonErrorAsync(SERVER_ERROR.SIGNIN_ALREADY_BOUND, true);
                
                if ((userConfirmation.ExpiredTime ?? DateTime.MinValue) < DateTime.Now) return await JsonErrorAsync(
                    SERVER_ERROR.SIGNIN_EXPIRED, true);


                User u = await DBService.DataSourceUser.GetUser(userConfirmation.UserId, cancellationToken);

                if (u == null) return await JsonErrorAsync(SERVER_ERROR.SIGNIN_USER_NOT_FOUND, true);
                if (u.IsBlocked) return await JsonErrorAsync(SERVER_ERROR.SIGNIN_USER_BLOCKED, true);
                if (u.IsBot) return await JsonErrorAsync(SERVER_ERROR.SIGNIN_BADUSER, true);
                if (u.IsAnonim) return await JsonErrorAsync(SERVER_ERROR.SIGNIN_BADUSER, true);

                if (userConfirmation.Code != ru.ConfirmationCode) return await JsonErrorAsync(SERVER_ERROR.SIGNIN_BADCODE, true);

                userConfirmation.IsConfirm = true;
                await DBService.DataSourceUserConfirmation.ChangeUserConfirmation(userConfirmation,
                    cancellationToken);
            
                device.CurrentConfirmationId = null;
                device.UserId = u.Id;
                await DBService.DataSourceDevice.ChangeDevice(device, cancellationToken);

                AnswerDevice ad = new AnswerDevice(device);

                ad.WaitConfirmation = false;
                ad.UserMail = null;

                await this.DBService.CoommitTransaction(cancellationToken);

                return JsonConvert.SerializeObject(ad);

            }
            catch (Exception e)
            {
                Logger.LogError(e.Message + Environment.NewLine + e.StackTrace);
                await this.DBService.RollbackTransaction(cancellationToken);
                return await JsonErrorAsync(SERVER_ERROR.SERVER_ERROR);
            }
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
        public async Task<string> FindUsersByNic([FromBody] RequestUserFinder rd, CancellationToken cancellationToken)
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

                User currentUser = await DBService.DataSourceUser.GetUserInfo(existDevice.UserId.Value, cancellationToken);

                if (currentUser == null) return await JsonErrorAsync("User not found");

                if (currentUser.DynamicUserInfo == null)
                {
                    return await JsonErrorAsync("UserInfo not found");
                }

                List<User> userList = ((rd.NicName ?? "").Length > 1) ? (await DBService.DataSourceUser.FindUsersByNic(rd.NicName, cancellationToken)) : new List<User>();
                userList = (userList ?? new List<User>()).Where(x => !x.IsBot && (x.Id != currentUser.Id)).ToList();


                List<User> recentUsers = await DBService.DataSourceUser.GetCompetitors(currentUser.Id, cancellationToken);

                AnswerUserList result = new AnswerUserList(userList, recentUsers, currentUser);

                return JsonConvert.SerializeObject(result);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message + Environment.NewLine + e.StackTrace);
                return await JsonErrorAsync("Server Error");
            }

        }




        /// <summary>
        /// Найти бота по нику
        /// </summary>
        public async Task<string> FindBotsByNic([FromBody] RequestUserFinder rd, CancellationToken cancellationToken)
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

                User currentUser = await DBService.DataSourceUser.GetUserInfo(existDevice.UserId.Value, cancellationToken);

                if (currentUser == null) return await JsonErrorAsync("User not found");

                if (currentUser.DynamicUserInfo == null)
                {
                    return await JsonErrorAsync("UserInfo not found");
                }

                List<User> userList = await DBService.DataSourceUser.FindUsersByNic(rd.NicName, cancellationToken);
                userList = (userList ?? new List<User>()).Where(x => x.IsBot && (x.Id != currentUser.Id)).ToList();

                //TODO выбирать только ботов
                List<User> recentUsers = await DBService.DataSourceUser.GetCompetitors(currentUser.Id, cancellationToken);

                AnswerUserList result = new AnswerUserList(userList, recentUsers, currentUser);

                return JsonConvert.SerializeObject(result);
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


        private UserConfirmation CreateConfirmation(Device device, User u, ConfirmationType confirmationType)
        {
            return new UserConfirmation()
            {
                Code = "1111",
                LinkCode = (new Guid()).ToString(),
                ConfirmationMethod = ConfirmationMethod.Mail,
                ConfirmationType = confirmationType,
                DeviceId = device.Id,
                ExpiredTime = DateTime.Now.AddDays(1),
                SendStamp = DateTime.Now,
                UserId = u.Id
            };
        }

    }
}
