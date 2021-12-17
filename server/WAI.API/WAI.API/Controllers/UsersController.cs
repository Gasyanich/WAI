using MediatR;
using Microsoft.AspNetCore.Mvc;
using WAI.API.Features.Auth;

namespace WAI.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterVkUser(VkLoginRequest request)
    {
        await _mediator.Send(request);

        return Ok();
    }
}