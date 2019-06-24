using EFCore.Data;
using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCore.SomeUI
{
    public class Program
    {
        private static readonly SamuraiContext Context = new SamuraiContext(new DbContextOptions<SamuraiContext>());

        public static void Main(string[] args)
        {
            //InsertSamurai();
            //InsertMultipleSamurais();
            //InsertMultipleDifferentObjects();
            //SimpleSamuraiQuery();
            //MoreQueries();
            //RetrieveAndUpdateSamurai();
            //RetrieveAndUpdateMultipleSamurai();
            //QueryAndUpdateBattle_Disconnected();
            //DeleteUsingId(3);
            //InsertNewPkFkGraph();
            //InsertNewPkFkGraphMultipleChildren();
            //AddChildToExistingObjectWhileNotTracked();
            //AddChildToExistingObjectWhileNotTracked(1);
            //EagerLoadSamuraiWithQuotes();
            //ProjectSomeProperties();
            //FilteringWithRelatedData();
            //ModifyingRelatedDataWhenTracked();
            ModifyingRelatedDataWhenNotTracked();
        }

        private static void ModifyingRelatedDataWhenNotTracked()
        {
            var samurai = Context.Samurais.Include(s => s.Quotes)
                .FirstOrDefault();
            var quote = samurai.Quotes[0];
            quote.Text += "Did you hear that?";

            using (var newContext = new SamuraiContext(new DbContextOptions<SamuraiContext>()))
            {
                //newContext.Quotes.Update(quote);
                newContext.Entry(quote).State = EntityState.Modified;
                newContext.SaveChanges();
            }
        }

        private static void ModifyingRelatedDataWhenTracked()
        {
            var samurai = Context.Samurais.Include(s => s.Quotes)
                .FirstOrDefault();
            samurai.Quotes[0].Text += "Did you hear that?";
            Context.SaveChanges();
        }

        private static void FilteringWithRelatedData()
        {
            var samurais = Context.Samurais
                .Where(s => s.Quotes.Any(q => q.Text.Contains("happy")))
                .ToList();
        }

        private static void ProjectSomeProperties()
        {
            //load just some specific properties    
            //var someProperties = Context.Samurais.Select(s =>
            //    new {s.Id, s.Name, s.Quotes.Count}).ToList();
            //
            //var somePropertiesWithSomeQuotes = Context.Samurais
            //    .Select(s => new
            //    {
            //        s.Id, s.Name,
            //        HappyQuotes = s.Quotes.Where(q => q.Text.Contains("happy"))
            //    }).ToList();
            //
            //var samuraisWithHappyQuotes = Context.Samurais
            //    .Select(s => new
            //    {
            //        Samurai = s,
            //        Quotes = s.Quotes.Where(q => q.Text.Contains("happy"))
            //            .ToList()
            //    }).ToList();

            var samurai = Context.Samurais.ToList();
            var happyQuotes = Context.Quotes.Where(q =>
                q.Text.Contains("happy")).ToList();
        }

        private static void EagerLoadSamuraiWithQuotes()
        {
            var samuraiWithQuotes = Context.Samurais.Include(s => s.Quotes)
                .ToList();
        }

        private static void AddChildToExistingObjectWhileNotTracked(int samuraiId)
        {
            var quote = new Quote
            {
                SamuraiId = samuraiId,
                Text = "Now that I saved you, will you feed me dinner?"
            };

            using (var newContext = new SamuraiContext(new DbContextOptions<SamuraiContext>()))
            {
                newContext.Quotes.Add(quote);
                newContext.SaveChanges();
            }
        }

        private static void AddChildToExistingObjectWhileNotTracked()
        {
            var samurai = Context.Samurais.First();

            samurai.Quotes.Add(new Quote
            {
                Text = "Now that I saved you, will you feed me dinner?"
            });

            using (var newContext = new SamuraiContext(new DbContextOptions<SamuraiContext>()))
            {
                
            }
            
        }

        private static void InsertNewPkFkGraphMultipleChildren()
        {
            var samurai = new Samurai
            {
                Name = "Kyuzo",
                Quotes = new List<Quote>
                {
                    new Quote {Text = "Watch out for my sharp sword!"},
                    new Quote {Text = "I told you to watch out for the sharp sword. Oh well!"}
                }
            };
            Context.Samurais.Add(samurai);
            Context.SaveChanges();
        }

        private static void InsertNewPkFkGraph()
        {
            var samurai = new Samurai
            {
                Name = "Kambei Shimada",
                Quotes =  new List<Quote>
                {
                  new Quote {Text = "I've come to save you"}  
                }
            };
            Context.Samurais.Add(samurai);
            Context.SaveChanges();
        }

        private static void DeleteUsingId(int samuraiId)
        {
            var samurai = Context.Samurais.Find(samuraiId);
            //Context.Remove(samurai);
            //Context.SaveChanges();
            
            //alternate - stored procedure!
            Context.Database.ExecuteSqlCommand("exec DeleteById {0}",
                samurai.Id);
        }

        private static void QueryAndUpdateBattle_Disconnected()
        {
            var battle = Context.Battles.FirstOrDefault();
            battle.EndDate = new DateTime(1560,06,30);
            using (var newContextInstance = new SamuraiContext(new DbContextOptions<SamuraiContext>()))
            {
                newContextInstance.Battles.Update(battle);
                newContextInstance.SaveChanges();
            }
        }

        private static void RetrieveAndUpdateMultipleSamurai()
        {
            var samurai = Context.Samurais.ToList();
            samurai.ForEach(s => s.Name += "San");
            Context.SaveChanges();
        }

        private static void RetrieveAndUpdateSamurai()
        {
            var samurai = Context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            Context.SaveChanges();
        }

        private static void MoreQueries()
        {
            //var name = "Sampson";
            //var samurai = Context.Samurais
            //    .Find(4);
            //
            //Console.WriteLine(samurai.Id + " " + samurai.Name);

            var samurai = Context.Samurais.Where(s =>
                    EF.Functions.Like(s.Name,"%O%"))
                .ToList();

            foreach (var sam in samurai)
            {
                Console.WriteLine($"{sam.Id} {sam.Name}");
            }
        }

        private static void SimpleSamuraiQuery()
        {
            using (var context = new SamuraiContext(new DbContextOptions<SamuraiContext>()))
            {
                var samurais = context.Samurais.ToList();
            }
        }

        private static void InsertMultipleDifferentObjects()
        {
            var samurai = new Samurai {Name = "Oda Nobunaga"};
            var battle = new Battle
            {
                Name = "Battle of Nagashino",
                StartDate = new DateTime(1575, 06, 16),
                EndDate = new DateTime(1575, 06, 28)
            };
            using (var context = new SamuraiContext(new DbContextOptions<SamuraiContext>()))
            {
                context.AddRange(samurai, battle);
                context.SaveChanges();
            }
        }

        private static void InsertSamurai()
        {
            var samurai = new Samurai {Name = "Julie"};
            using (var context = new SamuraiContext(new DbContextOptions<SamuraiContext>()))
            {
                context.Samurais.Add(samurai);
                context.SaveChanges();
            }
        }

        private static void InsertMultipleSamurais()
        {
            var samurai = new Samurai { Name = "Julie" };
            var samuraiSammy = new Samurai {Name = "Sampson"};

            using (var context = new SamuraiContext(new DbContextOptions<SamuraiContext>()))
            {
                context.Samurais.AddRange(samurai, samuraiSammy);
                context.SaveChanges();
            }
        }
    }
}
