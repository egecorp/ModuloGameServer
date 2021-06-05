using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ModuloGameServer.Models
{
    /// <summary>
    /// Одно подтверждение пользователя
    /// </summary>
    internal class UserConfirmationMapping : IEntityTypeConfiguration<UserConfirmation>
    {

        public void Configure(EntityTypeBuilder<UserConfirmation> builder)
        {

            builder.ToTable("UserConfirmations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).HasMaxLength(16);

            builder.Property(x => x.UserId).IsRequired();

            builder.Property(x => x.DeviceId).IsRequired();

        }

    }
}
