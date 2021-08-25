using Microsoft.EntityFrameworkCore.Storage;
using ModuloGameServer.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ModuloGameServer.Contracts
{
    public interface IModuloGameBotService
    {

        Task<bool> StartGame(Bot bot, Game game, CancellationToken cancellationToken);

        Task<bool> PlayRound(Bot bot, Game game, CancellationToken cancellationToken);

        Task<bool> Ping(Bot bot, Game game, CancellationToken cancellationToken);

    }
}
