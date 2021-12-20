using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WAI.API.DataAccess;
using WAI.API.DataAccess.Entities;

namespace WAI.API.Services.Games;

public class GamesService : IGamesService
{
    private readonly DataContext _dataContext;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _contextAccessor;

    public GamesService(DataContext dataContext, UserManager<User> userManager, IHttpContextAccessor contextAccessor)
    {
        _dataContext = dataContext;
        _userManager = userManager;
        _contextAccessor = contextAccessor;
    }

    public async Task<IEnumerable<GameInfo>> GetGamesInfoAsync()
    {
        var games = await _dataContext.Games.Where(g => g.Status == GameStatus.Created).ToListAsync();

        return games.Select(g => new GameInfo
        {
            Id = g.Id,
            Name = g.Name
        });
    }

    public async Task<GameInfo> CreateGameAsync(string gameName)
    {
        var creator = await _userManager.GetUserAsync(_contextAccessor!.HttpContext!.User);

        var game = new Game
        {
            Name = gameName,
            Status = GameStatus.Created,
            Users = new List<User>
            {
                creator
            }
        };

        _dataContext.Games.Add(game);

        await _dataContext.SaveChangesAsync();

        var creatorGameMember = await _dataContext.GameMembers
            .Where(gm => gm.GameId == game.Id && gm.UserId == creator.Id)
            .FirstAsync();

        creatorGameMember.Role = GameMemberRole.Creator;

        return new GameInfo
        {
            Id = game.Id,
            Name = gameName
        };
    }
}

public interface IGamesService
{
    Task<IEnumerable<GameInfo>> GetGamesInfoAsync();

    Task<GameInfo> CreateGameAsync(string gameName);
}