using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;
using System.Threading.Tasks;

namespace ModuloGameServer.Models
{

    public class ModuloGameDBContext : DbContext
    {
        public ModuloGameDBContext(DbContextOptions<ModuloGameDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }



        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetConnectString());
        }*/

        /*
        public DbSet<Device> BDSetDevice { set; get; }

        public DbSet<Game> BDSetGame { set; get; }

        public DbSet<GameRound> BDSetGameRound { set; get; }

        public DbSet<User> BDSetUser { set; get; }

        public DbSet<UserResult> BDSetUserResult { set; get; }


        private static bool IsFirstConnectionSolved = false;

        private static object mLock = new object();

        public static ModuloGameDBContext Get()
        {
            lock (mLock)
            {
                if (!IsFirstConnectionSolved)
                {
                    IsFirstConnectionSolved = true;
                    InitDB();
                }
            }
            return new ModuloGameDBContext();
        }

        private static string myConnectString = null;

        private static string GetConnectString()
        {
            return @"Server=(localdb)\MSSQLLocalDB;Database=RestGameDB;Trusted_Connection=True;";
        }

        */




    }
}
