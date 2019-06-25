using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            var timeStamp = DateTime.Now;

            foreach (var entry in ChangeTracker.Entries()
                .Where(e => !e.Metadata.IsOwned() &&
                            e.State == EntityState.Added ||
                            e.State == EntityState.Modified))
            {
                entry.Property("LastModified").CurrentValue = timeStamp;

                if (entry.State == EntityState.Added)
                {
                    entry.Property("Created").CurrentValue = timeStamp;
                }
            }

            return base.SaveChanges();
        }

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
            modelBuilder.Entity<Samurai>().OwnsOne(s => s.BetterName)
                .Property(b => b.GivenName).HasColumnName("GivenName");modelBuilder.Entity<Samurai>().OwnsOne(s => s.BetterName)
                .Property(b => b.Surname).HasColumnName("Surname");
            //modelBuilder.Entity<Samurai>().Property<DateTime>("Created");
            //modelBuilder.Entity<Samurai>().Property<DateTime>("LastModified");
            foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(e => !e.IsOwned()))
            { 
                modelBuilder.Entity(entityType.Name).Property<DateTime>("Created");
                modelBuilder.Entity(entityType.Name).Property<DateTime>("LastModified");
            }
        }
    }
}
