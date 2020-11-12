using LocalizaLabs.Domain.Entities;
using LocalizaLabs.Domain.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace LocalizaLabs.Domain.Services.Services
{
    public class NumberProcessingService : INumberProcessingService
    {
        public async Task<NumberProcessingResult> ProcessAsync(int number)
        {
            return await Task.Run(() => Process(number));
        }

        public NumberProcessingResult Process(int number)
        {
            var startOfProcessing = DateTime.Now;
            var numberProcessingResult = new NumberProcessingResult(startOfProcessing);

            for (int i = number; i > 0; i--)
            {
                if (IsDivider(number, i))
                {
                    numberProcessingResult.AddDivider(i);

                    if (IsPrime(number))
                        numberProcessingResult.AddPrime(i);
                }
            }

            numberProcessingResult.End = DateTime.Now;

            return numberProcessingResult;
        }

        private bool IsDivider(int number, int possibleDivider) =>
            number % possibleDivider == 0;

        private bool IsPrime(int number)
        {
            if (number <= 1)
                return false;

            int startNumber = number - 1;

            for (int i = startNumber; i > 1; i--)
            {
                if (IsDivider(number, i))
                    return false;
            }

            return true;
        }
    }
}
