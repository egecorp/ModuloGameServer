using System;


namespace ModuloGameServer.Models
{
    /// <summary>
    /// Команды, отправляемые боту сервером
    /// </summary>
    public enum BotCommand
    {
        Ping,
        NewGame,
        PlayRound,
        FinishGame
    };
}

