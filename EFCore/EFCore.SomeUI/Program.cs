using EFCore.Data;
using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EFCore.SomeUI
{
    public class Program
    {
        private static readonly SamuraiContext Context = new SamuraiContext(new DbContextOptions<SamuraiContext>());

        public static void Main(string[] args)
        {
            //CreateSamurai();
            //RetrieveSamuraiCreatedInPastWeek();
            //CreateThenEditSamuraiWithQuote();
            GetAllSamurai();
            //CreateSamuraiWithBetterName();
        }

        private static void CreateSamuraiWithBetterName()
        {
            var samurai = new Samurai
            {
                Name = "Jack le Black",
                BetterName = new PersonFullName("Jack", "Black")
            };

            Context.Samurais.Add(samurai);
            Context.SaveChanges();
        }

        private static void GetAllSamurai()
        {
            var allSamurai = Context.Samurais.ToList();
        }

        private static void CreateThenEditSamuraiWithQuote()
        {
            var samurai = new Samurai {Name = "Ronin"};
            var quote = new Quote {Text = "Aren't I Marvelous?"};
            samurai.Quotes.Add(quote);
            Context.Samurais.Add(samurai);
            Context.SaveChanges();
            quote.Text += "See what I did there?";
            Context.SaveChanges();
        }

        private static void RetrieveSamuraiCreatedInPastWeek()
        {
            var oneWeekAgo = DateTime.Now.AddDays(-7);
            //var newSamurais = Context.Samurais
            //    .Where(s => EF.Property<DateTime>(s, "Created")
            //                >= oneWeekAgo)
            //    .ToList();

            var samuraiCreated = Context.Samurais
                .Where(s => EF.Property<DateTime>(s, "Created")
                            >= oneWeekAgo)
                .Select(s => new
                {
                    s.Id, s.Name,
                    Created = EF.Property<DateTime>(s, "Created")
                }).ToList();
        }

        private static void CreateSamurai()
        {
            var samurai = new Samurai {Name = "Ronin"};
            Context.Samurais.Add(samurai);

            // We've inserted the shadow properties update logic
            //into SaveChanges(); in SamuraiConteext
            //var timeStamp = DateTime.Now;
            //Context.Entry(samurai).Property("Created")
            //    .CurrentValue = timeStamp;
            //Context.Entry(samurai).Property("LastModified")
            //    .CurrentValue = timeStamp;

            Context.SaveChanges();
        }
    }
}
