using EFCore.Data;
using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCore.SomeUI
{
    public class MappingsCommandsFirst
    {
        private static readonly SamuraiContext Context = new SamuraiContext(new DbContextOptions<SamuraiContext>());

        public static void New(string[] args)
        {
            //PrePopulateSamuraisAndBattles();
            //JoinBattleAndSamurai();
            //EnlistSamuraiIntoABattle();
            //EnlistSamuraiIntoABattleUnTracked();
            //GetSamuraiWithBattles();
            //RemoveJoinBetweenSamuraiAndBattleSimple();
            //RemoveBattleFromSamurai();
            //AddNewSamuraiWithSecretIdentity();
            EditASecretIdentity();
        }

        private static void EditASecretIdentity()
        {
            var samurai = Context.Samurais.Include(s => s.SecretIdentity)
                .FirstOrDefault(s => s.Id == 1);
            samurai.SecretIdentity.RealName = "T'Challa";
            Context.SaveChanges();
        }

        private static void AddNewSamuraiWithSecretIdentity()
        {
            var samurai = new Samurai { Name = "Jina Ujichika" };
            samurai.SecretIdentity = new SecretIdentity { RealName = "Julie" };
            Context.Samurais.Add(samurai);
            Context.SaveChanges();
        }

        private static void RemoveBattleFromSamurai()
        {
            //Goal: Remove join between Shichiroji(Id-3)
            //and Battle of Okehazama (Id-1)
            var samurai = Context.Samurais.Include(s =>
                    s.SamuraiBattles).ThenInclude(sb => sb.Battle)
                .SingleOrDefault(s => s.Id == 3);

            var sbToRemove = samurai.SamuraiBattles
                .SingleOrDefault(sb => sb.BattleId == 1);

            samurai.SamuraiBattles.Remove(sbToRemove); //Remove via List<T>
            //Context.Remove(sbToRemove); // remove using DbContext
            Context.ChangeTracker.DetectChanges();
            Context.SaveChanges();
        }

        private static void RemoveJoinBetweenSamuraiAndBattleSimple()
        {
            var join = new SamuraiBattle { BattleId = 1, SamuraiId = 8 };

            Context.Remove(join);
            Context.SaveChanges();
        }

        private static void GetSamuraiWithBattles()
        {
            var samuraiWithBattles = Context.Samurais
                .Include(s => s.SamuraiBattles)
                .ThenInclude(sb => sb.Battle)
                .FirstOrDefault(s => s.Id == 1);

            var battle = samuraiWithBattles.SamuraiBattles.First().Battle;
            var allTheBattles = new List<Battle>();
            foreach (var samuraiBattle in samuraiWithBattles.SamuraiBattles)
            {
                allTheBattles.Add(samuraiBattle.Battle);
            }
        }

        private static void EnlistSamuraiIntoABattleUnTracked()
        {
            Battle battle;

            using (var separateOperation = new SamuraiContext(new DbContextOptions<SamuraiContext>()))
            {
                battle = separateOperation.Battles.Find(1);
            }

            battle.SamuraiBattles.Add(new SamuraiBattle { SamuraiId = 2 });
            Context.Battles.Attach(battle);
            Context.ChangeTracker.DetectChanges();
            Context.SaveChanges();
        }

        private static void EnlistSamuraiIntoABattle()
        {
            var battle = Context.Battles.Find(1);
            battle.SamuraiBattles
                .Add(new SamuraiBattle { SamuraiId = 3 });
            Context.SaveChanges();
        }

        private static void JoinBattleAndSamurai()
        {
            //Kikuchiyo id is 1, Siege of Osaka id is 3
            var sbJoin = new SamuraiBattle { SamuraiId = 1, BattleId = 3 };
            Context.Add(sbJoin);
            Context.SaveChanges();
        }

        private static void PrePopulateSamuraisAndBattles()
        {
            var listOfSamurai = new List<Samurai>
            {
                new Samurai{Name = "Kikuchiyo"},
                new Samurai{Name = "Kambei Shimada"},
                new Samurai{Name = "Shichiroji"},
                new Samurai{Name = "Katsushiro Okamoto"},
                new Samurai{Name = "Heihachi Hayashida"},
                new Samurai{Name = "Kyuzo"},
                new Samurai{Name = "Gorobei Katayama"},
            };

            foreach (var samurai in listOfSamurai)
            {
                Context.Samurais.Add(samurai);
            }

            Context.Battles.AddRange(
                new Battle
                {
                    Name = "Battle of Okehazama",
                    StartDate = new DateTime(1560, 05, 01),
                    EndDate = new DateTime(1560, 06, 15)
                },
                new Battle
                {
                    Name = "Battle of Shiroyama",
                    StartDate = new DateTime(1877, 09, 24),
                    EndDate = new DateTime(1877, 09, 24)
                },
                new Battle
                {
                    Name = "Siege of Osaka",
                    StartDate = new DateTime(1614, 01, 01),
                    EndDate = new DateTime(1615, 12, 31)
                },
                new Battle
                {
                    Name = "Boshin War",
                    StartDate = new DateTime(1868, 01, 01),
                    EndDate = new DateTime(1869, 01, 01)
                });

            Context.SaveChanges();
        }
    }
}
