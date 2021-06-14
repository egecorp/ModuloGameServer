using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ModuloGameServer.Models
{
    /// <summary>
    /// Одна игра
    /// </summary>
    internal class GameMapping : IEntityTypeConfiguration<Game>
    {

        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Games");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(x => x.User1);

            builder.HasOne(x => x.User2);

            builder.Property(x => x.User1MaxRoundNumber).HasDefaultValue(0);

            builder.Property(x => x.User2MaxRoundNumber).HasDefaultValue(0);

            builder.Property(x => x.Status).HasDefaultValue(GAME_STATUS.GAME_CREATING);

        }

    }
}
