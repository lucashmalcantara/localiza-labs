using System;
using System.Collections.Generic;

namespace LocalizaLabs.Domain.Entities
{
    public class NumberProcessingResult
    {
        public int Number { get; set; }
        public List<int> Dividers { get; private set; }
        public List<int> PrimeNumbers { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime? End { get; set; }

        public NumberProcessingResult(DateTime start)
        {
            Start = start;
            Dividers = new List<int>();
            PrimeNumbers = new List<int>();
        }

        public void AddDivider(int number)
        {
            if (number <= 0)
                throw new ArgumentOutOfRangeException(nameof(number), $"The number {number} is not a valid divider.");
        }

        public void AddPrime(int number)
        {
            if (number <= 1)
                throw new ArgumentOutOfRangeException(nameof(number), $"The number {number} is not a valid prime number.");
        }
    }
}
