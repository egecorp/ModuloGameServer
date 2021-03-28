using System;
using System.Collections.Generic;
using System.Linq;

namespace ModuloGameServer.Models
{
    /// <summary>
    /// Работа с токенами устройств
    /// </summary>
    public class Tokens
    {

        public string Token { set; get; }

        public int DeviceId { set; get; }

        public DateTime ExpiredTime { set; get; }


        private Tokens(Device d)
        {
            Token = Tokens.GenerateToken();

            DeviceId = d.Id;

            ExpiredTime = DateTime.Now.AddMinutes(EXPIRED_MINUTES);
        }


        /// <summary>
        /// Объект для блокировки
        /// </summary>
        private static object mLock = new object();

        /// <summary>
        /// Словарь токенов
        /// </summary>
        private static Dictionary<string, Tokens> myTokens = new Dictionary<string, Tokens>();

        /// <summary>
        /// Через сколько минут истекает действие токена
        /// </summary>
        private const int EXPIRED_MINUTES = 60;

        /// <summary>
        /// Добавить новое устройство и получить рабочий токен
        /// </summary>
        /// <param name="od"></param>
        /// <returns></returns>
        public static string AddDevice(AnswerDevice ad)
        {
            Tokens newtk;
            lock (mLock)
            {
                do
                {
                    newtk = new Tokens(ad);

                } while (myTokens.ContainsKey(newtk.Token));

                myTokens.Add(newtk.Token, newtk);

                ad.WorkToken = newtk.Token;
                ad.WorkTokenExpired = newtk.ExpiredTime;
            }
            return newtk.Token;
        }

        /// <summary>
        /// Проверить и удалить токены с истёкшим сроком действия
        /// </summary>
        public static void CheckExpired()
        {
            List<string> expiredTokens;
            lock (mLock)
            {
                expiredTokens = myTokens.Keys.Where(x => myTokens[x].ExpiredTime < DateTime.Now).ToList();
            }

            if ((expiredTokens != null) && (expiredTokens.Count > 0))
            {
                lock (mLock)
                {
                    foreach (string t in expiredTokens) myTokens.Remove(t);
                }
            }
        }

        /// <summary>
        /// Проверить токен и вернуть Id устройства
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static int? GetDeviceId(string token)
        {
            lock (mLock)
            {
                if (!myTokens.ContainsKey(token)) return null;
                Tokens t = myTokens[token];
                if (t.ExpiredTime < DateTime.Now) return null;
                return t.DeviceId;
            }
        }

        /// <summary>
        /// Генерация случайной строки для токена
        /// </summary>
        /// <returns></returns>
        public static string GenerateToken()
        {
            Random rnd = new Random();
            return "AT"
                + rnd.Next(100000000, 999999999).ToString()
                + "-"
                + rnd.Next(100000000, 999999999).ToString()
                + ":"
                + rnd.Next(100000000, 999999999).ToString()
                + "-"
                + rnd.Next(1000000, 9999999).ToString()
                + "."
                + rnd.Next(100000000, 999999999).ToString()
                + "-"
                + rnd.Next(100000000, 999999999).ToString()
                + ":"
                + rnd.Next(100000000, 999999999).ToString()
                + "-"
                + rnd.Next(1000000, 9999999).ToString()
                + "."
                + rnd.Next(100000000, 999999999).ToString()
                + "-"
                + rnd.Next(100000000, 999999999).ToString()
                + ":"
                + rnd.Next(100000000, 999999999).ToString()
                + "-"
                + rnd.Next(1000000, 9999999).ToString()
                + "."
                + rnd.Next(100000000, 999999999).ToString()
                + "-"
                + rnd.Next(100000000, 999999999).ToString()
                + ":"
                + rnd.Next(100000000, 999999999).ToString()
                + "-"
                + rnd.Next(1000000, 9999999).ToString()
                + "."
                + rnd.Next(100000000, 999999999).ToString()
                + "-"
                + rnd.Next(100000000, 999999999).ToString()
                + ":"
                + rnd.Next(100000000, 999999999).ToString()
                + "-"
                + rnd.Next(1000000, 9999999).ToString()
                + ".";
        }


    }
}
