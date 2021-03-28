using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ModuloGameServer.Models
{
    /// <summary>
    /// Один  пользователь
    /// </summary>
    internal class UserMapping : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EMail).HasMaxLength(1024);
            builder.Property(x => x.NicName).HasMaxLength(256);

            builder.Property(x => x.VerifyCode).HasMaxLength(6);
            ;
        }

    }
}
