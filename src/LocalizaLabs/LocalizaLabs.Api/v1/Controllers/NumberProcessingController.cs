using LocalizaLabs.Api.v1.Configurations.Mappers;
using LocalizaLabs.Api.v1.Models.NumberProcessing;
using LocalizaLabs.Domain.Entities;
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
                return new ObjectResult(
                    await Task.Run(() => _numberProcessingService.RequestProcess(model.Number)));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NumberProcessingResult))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            try
            {
                NumberProcessingResult result =
                    await Task.Run(() => _numberProcessingService.GetResult(id));


                return new ObjectResult(result.ToModel());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("CheckIsReady/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> CheckIsReadyAsync(Guid id)
        {
            try
            {
                return new ObjectResult(
                    await Task.Run(() => _numberProcessingService.CheckIsReady(id)));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
