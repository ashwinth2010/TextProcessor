using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimCorp.TextFileProcessor.Application.Handler.Models;

namespace SimCorp.TextFileProcessor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextProcessorController : ControllerBase
    {
        private readonly ILogger<TextProcessorController> _logger;

        private readonly IMediator _mediator;

        public TextProcessorController(IMediator mediator, ILogger<TextProcessorController> logger)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetUniqueWords")]
        public async Task<UniqueWordsResponseDto> GetUniqueWords(string filePath, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(new GetUniqueWordHandlerRequest(filePath), cancellationToken);
        }

    }
}
