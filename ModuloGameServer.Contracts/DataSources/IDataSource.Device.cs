using System;
using System.Threading;
using System.Threading.Tasks;

namespace ModuloGameServer.Models
{

    public interface IDataSourceDevice
    {

        /// <summary>
        /// Функция проверяет, существут ли устройство с таким же токеном устройства
        /// </summary>
        /// <returns>Возвращает true, если всё в порядке</returns>
        Task<bool> CheckDeviceName(Guid DeviceToken, CancellationToken cancellationToken);


        /// <summary>
        /// Добавляет устройство в базу
        /// </summary>
        Task<bool> AddDevice(Device newDevice, CancellationToken cancellationToken);


        /// <summary>
        /// Ищет устройство по его токену
        /// </summary>
        Task<Device> GetDevice(Guid DeviceToken, CancellationToken cancellationToken);

        /// <summary>
        /// Ищет устройство по его Id
        /// </summary>
        Task<Device> GetDevice(int Id, CancellationToken cancellationToken);

        /// <summary>
        /// Изменяет устройство в базе
        /// </summary>
        Task ChangeDevice(Device existsDevice, CancellationToken cancellationToken);

    }
}
