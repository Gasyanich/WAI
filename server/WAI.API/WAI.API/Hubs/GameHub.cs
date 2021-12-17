using Microsoft.AspNetCore.SignalR;

namespace WAI.API.Hubs;

public class GameHub : Hub
{
    private readonly ILogger<GameHub> _logger;

    public GameHub(ILogger<GameHub> logger)
    {
        _logger = logger;
    }

    public async Task CreateGame(string gameName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, gameName);
    }
}