using System.Collections.Generic;
using SecretSanta.Domain.Services.Interfaces;

namespace SecretSanta.Domain.Tests.Services
{
    public class TestableRandom : IRandom
    {
        public List<int> RandomList = new List<int>();
        private static int NextNumber { get; set; }
        public int Next()
        {
            return RandomList[NextNumber++];
        }

        public int Next(int min, int max)
        {
            return RandomList[NextNumber++];
        }
    }
}