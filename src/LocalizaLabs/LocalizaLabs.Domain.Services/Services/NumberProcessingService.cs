using LocalizaLabs.Domain.Entities;
using LocalizaLabs.Domain.Interfaces.Services;
using LocalizaLabs.Infrastructure.Persistence.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizaLabs.Domain.Services.Services
{
    public class NumberProcessingService : INumberProcessingService
    {
        public NumberProcessingService()
        {
        }

        public Guid RequestProcess(long number)
        {
            var numberProcessingId = Guid.NewGuid();
            ProcessAsync(numberProcessingId, number);
            Enqueue(numberProcessingId);
            return numberProcessingId;
        }

        public NumberProcessingResult GetResult(Guid numberProcessingId) =>
            SimulatedPersistence.Instance.NumberProcessingResults
                .Where(r => r.Id == numberProcessingId)
                .FirstOrDefault();

        public bool CheckIsReady(Guid numberProcessingId) =>
            SimulatedPersistence.Instance.ProcessingRequestQueue
            .Any(p => p.NumberProcessingId == numberProcessingId && p.Ready);

        private Task ProcessAsync(Guid numberProcessingId, long number) =>
             Task.Run(() => Process(numberProcessingId, number));

        private void Enqueue(Guid numberProcessingId) =>
            SimulatedPersistence.Instance.ProcessingRequestQueue
                .Enqueue(new ProcessingRequest(numberProcessingId));

        private void SetAsReady(Guid numberProcessingId)
        {
            var processing = SimulatedPersistence.Instance.ProcessingRequestQueue
                                .Where(p => p.NumberProcessingId == numberProcessingId)
                                .FirstOrDefault();

            if (processing == null)
                return;

            processing.Ready = true;
        }

        private void Process(Guid numberProcessingId, long number)
        {
            var startOfProcessing = DateTime.Now;

            var numberProcessingResult = new NumberProcessingResult(numberProcessingId, number, startOfProcessing);

            var cacheResult = GetFromCache(number);

            if (cacheResult != null)
            {
                MergeFromCacheResult(numberProcessingResult, cacheResult);
                SimulatedPersistence.Instance.NumberProcessingResults.Add(numberProcessingResult);
                SetAsReady(numberProcessingId);
                return;
            }

            numberProcessingResult.AddDivisor(number);

            if (IsPrime(number))
                numberProcessingResult.AddPrime(number);

            long secondPossibleDivisor = number / 2;

            for (long i = secondPossibleDivisor; i > 0; i--)
            {
                if (IsDivisor(number, i))
                {
                    numberProcessingResult.AddDivisor(i);

                    if (IsPrime(i))
                        numberProcessingResult.AddPrime(i);
                }
            }

            numberProcessingResult.End = DateTime.Now;

            SimulatedPersistence.Instance.NumberProcessingResults.Add(numberProcessingResult);

            SetAsReady(numberProcessingId);
        }

        private void MergeFromCacheResult(NumberProcessingResult numberProcessingResult, NumberProcessingResult cacheResult)
        {
            numberProcessingResult.AddDivisorRange(cacheResult.Divisors);
            numberProcessingResult.AddPrimeRange(cacheResult.PrimeNumbers);
            numberProcessingResult.End = DateTime.Now;
        }

        private NumberProcessingResult GetFromCache(long number) =>
            SimulatedPersistence.Instance.NumberProcessingResults
                .Where(npr => npr.Number == number)
                .FirstOrDefault();

        private bool IsDivisor(long number, long possibleDivisor) =>
            number % possibleDivisor == 0;

        private bool IsPrime(long number)
        {
            if (number <= 1)
                return false;

            long startNumber = number - 1;

            for (long i = startNumber; i > 1; i--)
            {
                if (IsDivisor(number, i))
                    return false;
            }

            return true;
        }
    }
}
