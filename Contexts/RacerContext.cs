using DragRacerApi.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DragRacerApi.Contexts
{
    public class RacerContext : DbContext
    {
        public DbSet<Race> Races { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<User> Users { get; set; }

        public RacerContext(DbContextOptions<RacerContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionBuilder = new SqliteConnectionStringBuilder { DataSource = "./racer.db", Cache = SqliteCacheMode.Shared, Mode = SqliteOpenMode.ReadWriteCreate };
            var connection = new SqliteConnection(connectionBuilder.ToString());
            connection.Open();

            optionsBuilder.UseSqlite(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>().Navigation(e => e.Races).AutoInclude();
            modelBuilder.Entity<Race>().Navigation(e => e.User).AutoInclude();
        }
    }
}
