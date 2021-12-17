using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WAI.API.DataAccess.Entities;

public class User : IdentityUser<long>
{
    [Required] [MaxLength(200)] public string FirstName { get; set; }

    [Required] [MaxLength(200)] public string LastName { get; set; }

    [MaxLength(200)] public string? AvatarUrl { get; set; }

    [Required] public string VkAccessToken { get; set; }

    public ICollection<Game> Games { get; set; }
    public ICollection<GameMember> GameMembers { get; set; }
}