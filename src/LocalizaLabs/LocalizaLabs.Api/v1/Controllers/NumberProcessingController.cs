using LocalizaLabs.Api.v1.Models.NumberProcessing;
using LocalizaLabs.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LocalizaLabs.Api.v1.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class NumberProcessingController : ControllerBase
    {
        private INumberProcessingService _numberProcessingService;

        public NumberProcessingController(INumberProcessingService numberProcessingService)
        {
            _numberProcessingService = numberProcessingService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
        public async Task<IActionResult> PostAsync([FromBody] NumberProcessingRequestModel model)
        {
            try
            {
                return new ObjectResult(await _numberProcessingService.ProcessAsync(model.Number));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
