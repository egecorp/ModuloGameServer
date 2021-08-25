using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ModuloGameServer.Models
{
    /// <summary>
    /// Одно устройство пользователя
    /// Для него на самом устройстве генерируется токен устройства DeviceToken (логин)
    /// На сервере при регистрации генерируется токен сервера ServerToken (пароль)
    /// </summary>
    internal class DynamicUserInfoMapping : IEntityTypeConfiguration<DynamicUserInfo>
    {

        public void Configure(EntityTypeBuilder<DynamicUserInfo> builder)
        {
            builder.ToTable("DynamicUserInfo");

            builder.HasKey(x => x.UserId);

            builder.Ignore(x => x.ActiveGameList);

            builder.Ignore(x => x.RecentGameList);

        }

    }
}
