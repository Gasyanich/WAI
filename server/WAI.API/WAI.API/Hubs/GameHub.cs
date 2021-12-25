using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WAI.API.DataAccess;
using WAI.API.DataAccess.Entities;
using WAI.API.Hubs.DTO;

namespace WAI.API.Hubs;

public class GameHub : Hub
{
    private readonly DataContext _dataContext;
    private readonly UserManager<User> _userManager;

    public GameHub(DataContext dataContext, UserManager<User> userManager)
    {
        _dataContext = dataContext;
        _userManager = userManager;
    }

    [Authorize]
    public async Task ConnectToGame(long gameId)
    {
        var gameGroupName = gameId.ToString();

        await Groups.AddToGroupAsync(gameGroupName, Context.ConnectionId);

        var userTask = _userManager.GetUserAsync(Context.User);
        var gameTask = _dataContext.Games
            .Include(g => g.GameMembers)
            .ThenInclude(gm => gm.User)
            .FirstAsync(g => g.Id == gameId);

        var game = gameTask.Result;
        var user = userTask.Result;

        await Task.WhenAll(userTask, gameTask);

        var isNewGameMember = game.GameMembers.All(gm => gm.UserId != user.Id);

        if (isNewGameMember)
        {
            game.GameMembers.Add(new GameMember
            {
                GameId = gameId,
                UserId = user.Id,
                Role = GameMemberRole.Player
            });

            await _dataContext.SaveChangesAsync();
        }

        if (isNewGameMember)
        {
            var newGameMemberDto = GetGameMemberDto(user);
            await Clients.GroupExcept(gameGroupName, Context.ConnectionId)
                .SendAsync("MemberConnected", newGameMemberDto);
        }

        var gameDto = new GameDto
        {
            Id = gameId,
            GameMembers = game.GameMembers.Select(gm => GetGameMemberDto(gm.User)),
            Name = game.Name,
            GameStatus = game.Status
        };

        await Clients.Caller.SendAsync("ConnectedToGame", gameDto);
    }

    private static GameMemberDto GetGameMemberDto(User user)
    {
        return new GameMemberDto
        {
            AvatarUrl = user.AvatarUrl!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserId = user.Id
        };
    }
}