using Microsoft.AspNetCore.SignalR;

namespace WAI.API.Hubs;

public class GameHub : Hub
{
    // private readonly GameService _gameService;
    //
    // public GameHub(GameService gameService)
    // {
    //     _gameService = gameService;
    // }

    // [Authorize]
    // public async Task ConnectToGame(long gameId)
    // {
    // var userIdStr = Context.User!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
    // var userId = long.Parse(userIdStr);
    //
    // await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
    //
    // var gameDto = await _gameService.ConnectToGameAsync(gameId, userId);
    //
    // Clients.Group(gameId.ToString()).SendAsync("")
}