using System;
using System.Collections.Generic;
using SecretSanta.Domain.Services.Interfaces;

namespace SecretSanta.Domain.Tests.Services
{
    public class TestableRandom : IRandom
    {
        public List<int> RandomList { get; set; }
        private int _nextNumber = 0;
        public int Next()
        {
            return RandomList[_nextNumber++];
        }

        public int Next(int min, int max)
        {
            return RandomList[_nextNumber++];
        }
    }
}