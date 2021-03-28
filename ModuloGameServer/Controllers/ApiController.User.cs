using Microsoft.AspNetCore.Mvc;
using ModuloGameServer.Models;
using Newtonsoft.Json;
using System;
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

                User u = await DBService.DataSourceUser.GetUser(existDevice.UserId.Value, cancellationToken);

                if (u == null) return await JsonErrorAsync("User not found");

                AnswerUser au = new AnswerUser(u);

                return JsonConvert.SerializeObject(au);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return await JsonErrorAsync("Server Error");
            }

        }

        /// <summary>
        /// Создать анонимного пользователя 
        /// </summary>
        public async Task<string> CreateAnonimUser([FromBody] RequestUser ru, CancellationToken cancellationToken)
        {

            try
            {
                if (ru == null) return await JsonErrorAsync("No request");

                int? deviceId = Tokens.GetDeviceId(ru.WorkToken);

                if (!deviceId.HasValue) return await JsonErrorAsync("Access denied");

                Device existDevice = await DBService.DataSourceDevice.GetDevice(deviceId.Value, cancellationToken);

                if (existDevice == null) return await JsonErrorAsync("Access denied");

                if (existDevice.IsDisabled == true) return await JsonErrorAsync("Device is disabled");

                if (existDevice.UserId.HasValue) return await JsonErrorAsync("Device already bind to user");

                /*
                                User existsUser = DBService.DataSourceDevice.GetUserByEmail(ru.EMail.ToLower().Trim());

                                if (existsUser != null) return await JsonErrorAsync("Email already exists");
                */


                User u = new User()
                {
                    IsAnonim = true

                };
                DBService.DataSourceUser.AddUser(u, cancellationToken);

                if (u.Id <= 0) return await JsonErrorAsync("DataBase ERROR");


                u.NicName = "Anonim" + u.Id.ToString();

                DBService.DataSourceUser.ChangeUser(u, cancellationToken);

                AnswerUser au = new AnswerUser(u);

                return JsonConvert.SerializeObject(au);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return await JsonErrorAsync("Server Error");
            }
        }

        /// <summary>
        /// Создать пользователя с почтовым ящиком, зарегистрировать устройство
        /// и отправить код авторизации
        /// Возвращается постоянный токен устройства
        /// </summary>
        public string CreateVerifiedUser([FromBody] RequestUser ru, CancellationToken cancellationToken)
        {
            return "Error";
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


    }
}
