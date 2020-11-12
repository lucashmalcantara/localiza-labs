using LocalizaLabs.Domain.Entities;
using System;

namespace LocalizaLabs.Domain.Interfaces.Services
{
    public interface INumberProcessingService
    {
        Guid RequestProcess(long number);
        bool CheckIsReady(Guid numberProcessingId);
        NumberProcessingResult GetResult(Guid numberProcessingId);
    }
}
