using MediatR;
using Microsoft.AspNetCore.Mvc;
using WAI.API.Features.Auth;

namespace WAI.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterVkUser(VkLoginRequest request)
    {
        var response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpGet]
    public IActionResult RedirectToVkLogin()
    {
        const string vkRedirectUrl = "https://oauth.vk.com/authorize?" +
                                     "client_id=8028309&" +
                                     "display=popup&" +
                                     "redirect_uri=https://localhost:7212/vkcallback&" +
                                     "scope=friends,offline,email,photos&" +
                                     "response_type=code&" +
                                     "v = 5.131";

        return Redirect(vkRedirectUrl);
    }
}