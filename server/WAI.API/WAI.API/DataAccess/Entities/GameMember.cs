using System.ComponentModel.DataAnnotations;

namespace WAI.API.DataAccess.Entities;

public class GameMember
{
    public long UserId { get; set; }
    public User User { get; set; }

    public long GameId { get; set; }
    public Game Game { get; set; }

    [MaxLength(256)]
    public string Word { get; set; }

    [Required]
    public GameMemberRole Role { get; set; }
}