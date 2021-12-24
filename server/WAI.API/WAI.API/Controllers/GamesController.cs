using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WAI.API.Features.Games.CreateGame;
using WAI.API.Features.Games.GetGames;

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

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody] CreateGameRequest request)
    {
        await _mediator.Send(request);

        return Ok();
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetGames()
    {
        var response = await _mediator.Send(new GetGamesRequest());

        return Ok(response);
    }
}