using System.Security.Claims;
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

        // добавляем текущее подключение юзера в группу игры
        await Groups.AddToGroupAsync(Context.ConnectionId, gameGroupName);

        var user = await _userManager.GetUserAsync(Context.User);
        var game = await _dataContext.Games
            .Include(g => g.GameMembers)
            .ThenInclude(gm => gm.User)
            .FirstAsync(g => g.Id == gameId);


        // проверяем, не состоит ли юзер в игре
        var isNewGameMember = game.GameMembers.All(gm => gm.UserId != user.Id);

        // если не состоит, то добавим его в бд и скажем остальным участинкам игры, что у нас есть новенький
        if (isNewGameMember)
        {
            game.GameMembers.Add(new GameMember
            {
                GameId = gameId,
                UserId = user.Id,
                Role = GameMemberRole.Player
            });

            await _dataContext.SaveChangesAsync();

            var newGameMemberDto = GetGameMemberDto(user);

            await Clients.GroupExcept(gameGroupName, Context.ConnectionId)
                .SendAsync("MemberConnected", newGameMemberDto);
        }

        // разделил на уровне клиента создателя игры и остальных участинков
        var creator = GetGameMemberDto(game.GameMembers.First(gm => gm.Role == GameMemberRole.Creator).User);
        var otherMembers = game.GameMembers
            .Where(gm => gm.Role == GameMemberRole.Player)
            .Select(gm => GetGameMemberDto(gm.User));

        var gameDto = new GameDto
        {
            Id = gameId,
            Name = game.Name,
            GameStatus = game.Status,
            Creator = creator,
            GameMembers = otherMembers,
        };

        // отправим новичку информацию о текущей игре
        await Clients.Caller.SendAsync("ConnectedToGame", gameDto);
    }

    [Authorize]
    public async Task DisconnectFromGame(long gameId)
    {
        var gameGroupName = gameId.ToString();

        var userId = long.Parse(Context.User!.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

        _dataContext.GameMembers.Remove(new GameMember {GameId = gameId, UserId = userId});
        await _dataContext.SaveChangesAsync();

        await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameGroupName);
        await Clients.Group(gameGroupName).SendAsync("MemberDisconnected", userId);
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