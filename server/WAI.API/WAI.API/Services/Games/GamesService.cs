namespace WAI.API.Services.Games;

public class GamesService : IGamesService
{
    public IEnumerable<GameInfo> GetGamesInfo()
    {
        return new List<GameInfo>
        {
            new GameInfo
            {
                Id = 1,
                Creator = "Me",
                Name = "Test game name",
                MembersCount = 1
            },
            new GameInfo
            {
                Id = 2,
                Creator = "Not me",
                Name = "Game name test",
                MembersCount = 1
            }
        };
    }
}

public interface IGamesService
{
    IEnumerable<GameInfo> GetGamesInfo();
}