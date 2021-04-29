using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ModuloGameServer.Models
{
    /// <summary>
    /// Один ход одного из участников
    /// </summary>
    internal class GameRoundMapping : IEntityTypeConfiguration<GameRound>
    {

        public void Configure(EntityTypeBuilder<GameRound> builder)
        {
            builder.ToTable("GameRound");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(x => x.User);

            builder.HasOne(x => x.Game).WithMany(g => g.Rounds).HasForeignKey(x => x.GameId).HasPrincipalKey(x => x.Id);


        }

    }
}
