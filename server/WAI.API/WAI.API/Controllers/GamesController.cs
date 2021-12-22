using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WAI.API.Features.Games;

namespace WAI.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GamesController : Controller
{
    private readonly IMediator _mediator;

    public GamesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateGame([FromBody] CreateGameRequest request)
    {
        await _mediator.Send(request);

        return Ok();
    }
}