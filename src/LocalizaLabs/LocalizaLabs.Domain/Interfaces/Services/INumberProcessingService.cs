using LocalizaLabs.Domain.Entities;
using System.Threading.Tasks;

namespace LocalizaLabs.Domain.Interfaces.Services
{
    public interface INumberProcessingService
    {
        Task<NumberProcessingResult> ProcessAsync(int number);
        NumberProcessingResult Process(int number);
    }
}
