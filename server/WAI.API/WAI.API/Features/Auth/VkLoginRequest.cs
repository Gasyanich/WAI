using System.ComponentModel.DataAnnotations;
using MediatR;

namespace WAI.API.Features.Auth;

public class VkLoginRequest : IRequest<VkLoginResponse>
{
    [Required] public string Code { get; set; }
}