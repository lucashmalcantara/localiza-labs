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

            for (long i = number; i > 0; i--)
            {
                if (IsDivider(number, i))
                {
                    numberProcessingResult.AddDivider(i);

                    if (IsPrime(i))
                        numberProcessingResult.AddPrime(i);
                }
            }

            numberProcessingResult.End = DateTime.Now;

            SimulatedPersistence.Instance.NumberProcessingResults.Add(numberProcessingResult);

            SetAsReady(numberProcessingId);
        }

        private bool IsDivider(long number, long possibleDivider) =>
            number % possibleDivider == 0;

        private bool IsPrime(long number)
        {
            if (number <= 1)
                return false;

            long startNumber = number - 1;

            for (long i = startNumber; i > 1; i--)
            {
                if (IsDivider(number, i))
                    return false;
            }

            return true;
        }
    }
}
