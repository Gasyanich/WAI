using MediatR;
using WAI.API.Services.Games;

namespace WAI.API.Features.Games;

public class CreatedGameEvent : INotification
{
    public CreatedGameEvent(GameInfo gameInfo)
    {
        GameInfo = gameInfo;
    }

    public GameInfo GameInfo { get; set; }
}