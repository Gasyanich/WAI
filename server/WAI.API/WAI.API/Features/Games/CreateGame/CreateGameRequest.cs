using System.ComponentModel.DataAnnotations;
using MediatR;

namespace WAI.API.Features.Games.CreateGame;

public class CreateGameRequest : IRequest
{
    [Required] public string GameName { get; set; }
}