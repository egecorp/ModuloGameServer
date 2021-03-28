using Microsoft.EntityFrameworkCore;
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

        public ModuloGameDBService(ModuloGameDBContext dbContext)
        {
            context = dbContext;
        }

        public async Task<Game> GetGame(int Id, CancellationToken cancellationToken)
        {
            return await context.Set<Game>().FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }
    }
}
