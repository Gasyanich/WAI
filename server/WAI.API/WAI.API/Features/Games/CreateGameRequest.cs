using System.ComponentModel.DataAnnotations;
using MediatR;

namespace WAI.API.Features.Games;

public class CreateGameRequest : IRequest
{
    [Required] public string GameName { get; set; }
}