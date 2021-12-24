using MediatR;

namespace WAI.API.Features.Games.GetGames;

public class GetGamesRequest : IRequest<IEnumerable<GetGamesResponse>>
{
}