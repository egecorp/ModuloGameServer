using Microsoft.EntityFrameworkCore.Storage;
using ModuloGameServer.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ModuloGameServer.Contracts
{
    public interface IModuloGameMessageSender
    {

        string GenerateCode(CancellationToken cancellationToken);

        Task<bool> SendCodeAsync(User user, string code, CancellationToken cancellationToken);

    }
}
