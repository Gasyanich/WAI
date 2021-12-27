using WAI.API.DataAccess.Entities;

namespace WAI.API.Hubs.DTO;

public class GameDto
{
    public long Id { get; set; }

    public string Name { get; set; }

    public GameMemberDto Creator { get; set; }

    public IEnumerable<GameMemberDto> GameMembers { get; set; }

    public GameStatus GameStatus { get; set; }
}