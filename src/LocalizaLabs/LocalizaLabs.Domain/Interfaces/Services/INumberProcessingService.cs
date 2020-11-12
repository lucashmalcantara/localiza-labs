using System;

namespace LocalizaLabs.Domain.Interfaces.Services
{
    public interface INumberProcessingService
    {
        Guid RequestProcess(long number);
        //Task<NumberProcessingResult> ProcessAsync(long number);
        //NumberProcessingResult Process(long number);
    }
}
