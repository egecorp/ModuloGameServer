using System;
using System.IO;

namespace ModuloGameServer
{
    public static class TempLog
    {
        private static string fileName;

        static TempLog()
        {
            fileName = "/var/log/mg.log";
        }


        public static void Log(string txt)
        {
            File.AppendAllText(fileName, txt + Environment.NewLine);
        }
        
    }
}
