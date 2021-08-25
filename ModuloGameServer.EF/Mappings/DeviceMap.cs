using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ModuloGameServer.Models
{
    /// <summary>
    /// Одно устройство пользователя
    /// Для него на самом устройстве генерируется токен устройства DeviceToken (логин)
    /// На сервере при регистрации генерируется токен сервера ServerToken (пароль)
    /// </summary>
    internal class DeviceMapping : IEntityTypeConfiguration<Device>
    {

        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.ToTable("Devices");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Caption).HasMaxLength(256);
            builder.Property(x => x.ServerToken).HasMaxLength(4096);

            builder.HasIndex(x => x.DeviceToken);

        }




    }
}
