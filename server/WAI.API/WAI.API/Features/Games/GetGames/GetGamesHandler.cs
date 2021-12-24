using MediatR;
using Microsoft.EntityFrameworkCore;
using WAI.API.DataAccess;
using WAI.API.DataAccess.Entities;

namespace WAI.API.Features.Games.GetGames;

public class GetGamesHandler : IRequestHandler<GetGamesRequest, IEnumerable<GetGamesResponse>>
{
    private readonly DataContext _dataContext;

    public GetGamesHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<IEnumerable<GetGamesResponse>> Handle(
        GetGamesRequest request,
        CancellationToken cancellationToken)
    {
        var games = await _dataContext.Games.Where(g => g.Status == GameStatus.Created).ToListAsync(cancellationToken);

        return games.Select(g => new GetGamesResponse
        {
            Id = g.Id,
            Name = g.Name
        });
    }
}