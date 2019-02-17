using System;
using SecretSanta.Domain.Services.Interfaces;

namespace SecretSanta.Domain.Services
{
    public class BetterRandom : IRandom
    {
        private static readonly Random RandomInstance = new Random();
        
        public int Next() 
        { 
            lock (RandomInstance) return RandomInstance.Next(); 
        }

        public int Next(int min, int max)
        {
            lock (RandomInstance) return RandomInstance.Next(min, max);
        }
    }
}