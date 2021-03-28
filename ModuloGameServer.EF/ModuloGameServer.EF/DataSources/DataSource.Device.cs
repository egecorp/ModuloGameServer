using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ModuloGameServer.Models
{

    public class DataSourceDevice : IDataSourceDevice
    {
        private ModuloGameDBContext context;

        public DataSourceDevice(ModuloGameDBContext context)
        {
            this.context = context;
        }


        public async Task<Device> GetDevice(int Id, CancellationToken cancellationToken)
        {
            return await context.Set<Device>().FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        public async Task<Device> GetDevice(Guid DeviceToken, CancellationToken cancellationToken)
        {
            return await context.Set<Device>().FirstOrDefaultAsync(x => x.DeviceToken == DeviceToken, cancellationToken);
        }


        public async Task<IEnumerable<Device>> GetDevices(Expression<Func<Device, bool>> whereFunction, CancellationToken cancellationToken)
        {
            return await context.Set<Device>().Where(whereFunction).ToListAsync(cancellationToken);
        }


        /// <summary>
        /// Функция проверяет, существут ли устройство с таким же токеном устройства
        /// </summary>
        /// <returns>Возвращает true, если всё в порядке</returns>
        public async Task<bool> CheckDeviceName(Guid DeviceToken, CancellationToken cancellationToken)
        {
            Device d = await GetDevice(DeviceToken, cancellationToken);
            return await Task.FromResult<bool>(d != null);
        }


        /// <summary>
        /// Добавляет устройство в базу
        /// </summary>
        public async Task<bool> AddDevice(Device newDevice, CancellationToken cancellationToken)
        {
            await context.Set<Device>().AddAsync(newDevice, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult<bool>(true);
        }



    }
}
