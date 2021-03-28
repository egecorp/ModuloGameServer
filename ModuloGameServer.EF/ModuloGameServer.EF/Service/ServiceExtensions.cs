using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModuloGameServer.Contracts;

namespace ModuloGameServer.Models
{
    /// <summary>
    /// Одно устройство пользователя
    /// Для него на самом устройстве генерируется токен устройства DeviceToken (логин)
    /// На сервере при регистрации генерируется токен сервера ServerToken (пароль)
    /// </summary>
    public static class ServiceExtension
    {
        public static void AddSqlServer(this IServiceCollection collection, string connectionString)
        {

            collection.AddDbContext<ModuloGameDBContext>(options =>
            {
                options.UseSqlServer(connectionString, config =>
                {
                    config.MigrationsAssembly(typeof(ModuloGameDBContext).Assembly.FullName);
                });
            });

            collection.AddScoped<IModuloGameDBService, ModuloGameDBService>();


            /*
            DbContextOptionsBuilder<ModuloGameDBContext> optionsBuilder = new DbContextOptionsBuilder<ModuloGameDBContext>();
            optionsBuilder.UseSqlServer(ConnectionString, config =>
            {
                config.MigrationsAssembly(typeof(ModuloGameDBContext).Assembly.FullName);
            });

            context = new ModuloGameDBContext(optionsBuilder.Options);
            */



        }




    }
}
