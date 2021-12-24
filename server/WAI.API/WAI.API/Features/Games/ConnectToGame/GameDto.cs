using WAI.API.DataAccess.Entities;

namespace WAI.API.Features.Games.ConnectToGame;

public class GameDto
{
    public IEnumerable<GameMemberDto> GameMembers { get; set; }

    public long GameId { get; set; }

    public GameStatus GameStatus { get; set; }
}