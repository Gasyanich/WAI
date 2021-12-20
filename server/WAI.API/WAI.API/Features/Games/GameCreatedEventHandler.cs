using MediatR;
using Microsoft.AspNetCore.SignalR;
using WAI.API.Hubs;
using WAI.API.Services.Games;

namespace WAI.API.Features.Games;

public class GameCreatedEventHandler : INotificationHandler<CreatedGameEvent>
{
    private readonly IHubContext<GamesHub> _gamesHubContext;
    private readonly IGamesService _gamesService;

    public GameCreatedEventHandler(IHubContext<GamesHub> gamesHubContext, IGamesService gamesService)
    {
        _gamesHubContext = gamesHubContext;
        _gamesService = gamesService;
    }

    public async Task Handle(CreatedGameEvent notification, CancellationToken cancellationToken)
    {
        var gamesInfo = await _gamesService.GetGamesInfoAsync();
        
        await _gamesHubContext.Clients.All.SendAsync("GetGames", gamesInfo, cancellationToken);
    }
}