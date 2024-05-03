using MediatR;
using System.ComponentModel.DataAnnotations;

namespace SimCorp.TextFileProcessor.Application.Handler.Models;

public record GetUniqueWordHandlerRequest(
    [property: Required] string FilePath)
    : IRequest<UniqueWordsResponseDto?>;

