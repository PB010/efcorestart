﻿using System.Collections.Generic;

namespace EFCore.Domain
{
    public class Samurai
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PersonFullName BetterName { get; set; }  
        public List<Quote> Quotes { get; set; }
        public List<SamuraiBattle> SamuraiBattles { get; set; }
        public SecretIdentity SecretIdentity { get; set; }
         
        public Samurai()
        {
            Quotes = new List<Quote>();
        }
    }
}
