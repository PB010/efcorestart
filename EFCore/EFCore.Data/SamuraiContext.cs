using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace EFCore.Data
{
    public class SamuraiContext : DbContext
    {
        public SamuraiContext(DbContextOptions<SamuraiContext> options)
        :base(options)
        {
            
        }

        public static readonly LoggerFactory MyConsoleLoggerFactory
        = new LoggerFactory(new []
        {
#pragma warning disable 618
            new ConsoleLoggerProvider((category, level)
#pragma warning restore 618
                => category == DbLoggerCategory.Database.Command.Name
                   && level == LogLevel.Information, true)});

        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server = (localdb)\\MSSQLLocalDb; Database = SamuraiAppData; Trusted_Connection = True;")
                .UseLoggerFactory(MyConsoleLoggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiBattle>()
                .HasKey(s => new {s.SamuraiId, s.BattleId});
        }
    }
}
