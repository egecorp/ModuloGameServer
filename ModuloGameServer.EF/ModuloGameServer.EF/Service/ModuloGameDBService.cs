using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ModuloGameServer.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ModuloGameServer.Contracts
{
    public class ModuloGameDBService : IModuloGameDBService
    {

        private ModuloGameDBContext context;

        private IDataSourceUser myDataSourceUser = null;
        public IDataSourceUser DataSourceUser
        {
            get
            {
                return (myDataSourceUser == null) ? (myDataSourceUser = new DataSourceUser(context)) : myDataSourceUser;
            }
        }

        private IDataSourceDevice myDataSourceDevice = null;
        public IDataSourceDevice DataSourceDevice
        {
            get
            {
                return (myDataSourceDevice == null) ? (myDataSourceDevice = new DataSourceDevice(context)) : myDataSourceDevice;
            }
        }

        private IDataSourceGame myDataSourceGame = null;
        public IDataSourceGame DataSourceGame
        {
            get
            {
                return (myDataSourceGame == null) ? (myDataSourceGame = new DataSourceGame(context)) : myDataSourceGame;
            }
        }


        private IDataSourceBot myDataSourceBot = null;
        public IDataSourceBot DataSourceBot
        {
            get
            {
                return (myDataSourceBot == null) ? (myDataSourceBot = new DataSourceBot(context)) : myDataSourceBot;
            }
        }

        


        public ModuloGameDBService(ModuloGameDBContext dbContext)
        {
            context = dbContext;
        }


        public async Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken)
        {
            return await context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CoommitTransaction(CancellationToken cancellationToken)
        {
            await context.Database.CommitTransactionAsync(cancellationToken);
        }

        public async Task RollbackTransaction(CancellationToken cancellationToken)
        {
            await context.Database.RollbackTransactionAsync(cancellationToken);
        }

    }
}
