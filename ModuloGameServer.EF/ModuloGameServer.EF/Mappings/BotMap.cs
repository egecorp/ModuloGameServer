using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ModuloGameServer.Models
{
    /// <summary>
    /// Один бот
    /// </summary>
    internal class BotMapping : IEntityTypeConfiguration<Bot>
    {

        public void Configure(EntityTypeBuilder<Bot> builder)
        {


            builder.ToTable("Bots");

            builder.HasKey(x => x.BotId);

            builder.Property(x => x.NicName).HasMaxLength(256);

            //builder.Property(x => x.OwnerUserId).IsRequired();

            builder.Property(x => x.UserId).IsRequired();
           
            //builder.HasOne(x => x.User);

            //builder.HasOne(x => x.OwnerUser);

            builder.HasIndex(x => x.NicName);

        }




    }
}
