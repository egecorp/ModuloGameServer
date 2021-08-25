using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ModuloGameServer.Models
{

    public class DataSourceUserConfirmation : IDataSourceUserConfirmation
    {
        private ModuloGameDBContext context;
        
        public DataSourceUserConfirmation(ModuloGameDBContext context)
        {
            this.context = context;
        }
        
        /// <summary>
        /// Добавляет подтверждение в базу
        /// </summary>
        public async Task AddUserConfirmation(UserConfirmation newUserConfirmation, CancellationToken cancellationToken)
        {
            await context.Set<UserConfirmation>().AddAsync(newUserConfirmation, cancellationToken);
            /*
            Device confirmationDevice =
                await context.Set<Device>().FirstAsync(x => x.Id == newUserConfirmation.DeviceId);

            confirmationDevice.CurrentConfirmationId = newUserConfirmation.Id;
            */
            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Изменяет подтверждение в базе
        /// </summary>
        public async Task ChangeUserConfirmation(UserConfirmation newUserConfirmation, CancellationToken cancellationToken)
        {
            context.Set<UserConfirmation>().Update(newUserConfirmation);
            await context.SaveChangesAsync(cancellationToken);
        }
        
        /// <summary>
        /// Ищет подтверждение по его Id
        /// </summary>
        public async Task<UserConfirmation> GetUserConfirmation(int id, CancellationToken cancellationToken)
        {
            return await context.Set<UserConfirmation>().FirstOrDefaultAsync(x => x.Id == id , cancellationToken);
        }

        /// <summary>
        /// Ищет подтверждение по его коду ссылки
        /// </summary>
        public async Task<UserConfirmation> GetUserConfirmationByLinkCode(string code, CancellationToken cancellationToken)
        {
            return await context.Set<UserConfirmation>().FirstOrDefaultAsync(x => x.LinkCode == code, cancellationToken);
        }
        
    }
}
