using Microsoft.EntityFrameworkCore;
using ModuloGameServer.Models;

namespace ModuloGameServer.Contracts
{
    public class ModuloGameDBSetupService : IModuloGameDBSetupService
    {

        private ModuloGameDBContext context;

        public ModuloGameDBSetupService(ModuloGameDBContext dbContext)
        {
            context = dbContext;
        }

        public void Setup()
        {
            context.Database.Migrate();
        }
    }
}
