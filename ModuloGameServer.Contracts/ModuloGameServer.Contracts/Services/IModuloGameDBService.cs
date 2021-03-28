using ModuloGameServer.Models;

namespace ModuloGameServer.Contracts
{
    public interface IModuloGameDBService
    {

        IDataSourceDevice DataSourceDevice { get; }

        IDataSourceUser DataSourceUser { get; }

        //Task<Device> GetDevice(int Id, CancellationToken cancellationToken);

        //Task<Device> GetDevice(Guid DeviceToken, CancellationToken cancellationToken);

        //Task<IEnumerable<Device>> GetDevices(CancellationToken cancellationToken);

        //  Task<User> GetUser(int Id, CancellationToken cancellationToken);

        //Task<Game> GetGame(int Id, CancellationToken cancellationToken);
    }
}
