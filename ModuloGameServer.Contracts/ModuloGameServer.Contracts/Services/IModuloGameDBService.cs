using Microsoft.EntityFrameworkCore.Storage;
using ModuloGameServer.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ModuloGameServer.Contracts
{
    public interface IModuloGameDBService
    {

        IDataSourceDevice DataSourceDevice { get; }

        IDataSourceUser DataSourceUser { get; }


        Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken);

        Task CoommitTransaction(CancellationToken cancellationToken);

        Task RollbackTransaction(CancellationToken cancellationToken);
        

    }
}
