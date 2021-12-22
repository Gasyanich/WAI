using Microsoft.AspNetCore.SignalR;
using WAI.API.Services.Games;

namespace WAI.API.Hubs;

public class GamesHub : Hub
{
    private readonly IGamesService _gamesService;

    public GamesHub(IGamesService gamesService)
    {
        _gamesService = gamesService;
    }

    public override async Task OnConnectedAsync()
    {
        var games = await _gamesService.GetGamesInfoAsync();
        await Clients.Caller.SendAsync("GetGames", games);
    }
}