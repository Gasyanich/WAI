using MediatR;
using WAI.API.Services.Games;

namespace WAI.API.Features.Games;

public class CreateGameHandler : IRequestHandler<CreateGameRequest>
{
    private readonly IGamesService _gamesService;
    private readonly IMediator _mediator;

    public CreateGameHandler(IGamesService gamesService, IMediator mediator)
    {
        _gamesService = gamesService;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(CreateGameRequest request, CancellationToken cancellationToken)
    {
        var gameInfo = await _gamesService.CreateGameAsync(request.GameName);

        await _mediator.Publish(new CreatedGameEvent(gameInfo), cancellationToken);

        return Unit.Value;
    }
}