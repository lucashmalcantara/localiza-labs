using System;
using System.Collections.Generic;

namespace LocalizaLabs.Domain.Entities
{
    public class NumberProcessingResult
    {
        public Guid Id { get; set; }
        public long Number { get; set; }
        public List<long> Divisors { get; private set; }
        public List<long> PrimeNumbers { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime? End { get; set; }

        public NumberProcessingResult(Guid id, long number, DateTime start)
        {
            Id = id;
            Number = number;
            Start = start;
            Divisors = new List<long>();
            PrimeNumbers = new List<long>();
        }

        public void AddDivisor(long number)
        {
            if (number <= 0)
                throw new ArgumentOutOfRangeException(nameof(number), $"The number {number} is not a valid Divisor.");

            Divisors.Add(number);
        }

        public void AddDivisorRange(List<long> numbers) =>
            numbers.ForEach(n => AddDivisor(n));

        public void AddPrime(long number)
        {
            if (number <= 1)
                throw new ArgumentOutOfRangeException(nameof(number), $"The number {number} is not a valid prime number.");

            PrimeNumbers.Add(number);
        }

        public void AddPrimeRange(List<long> numbers) =>
            numbers.ForEach(n => AddPrime(n));
    }
}
