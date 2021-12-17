using System.ComponentModel.DataAnnotations;

namespace WAI.API.DataAccess.Entities;

public class Game
{
    [Key] public long Id { get; set; }

    [Required] [MaxLength(256)] public string Name { get; set; }

    [Required] public GameStatus Status { get; set; }

    public ICollection<User> Users { get; set; }
    public ICollection<GameMember> GameMembers { get; set; }
}