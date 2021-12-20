using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    
    
}