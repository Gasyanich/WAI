using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WAI.API.DataAccess;
using WAI.API.DataAccess.Entities;

namespace WAI.API.Features.Games.CreateGame;

public class CreateGameHandler : IRequestHandler<CreateGameRequest>
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly DataContext _dataContext;
    private readonly UserManager<User> _userManager;

    public CreateGameHandler(DataContext dataContext, UserManager<User> userManager,
        IHttpContextAccessor contextAccessor)
    {
        _dataContext = dataContext;
        _userManager = userManager;
        _contextAccessor = contextAccessor;
    }

    public async Task<Unit> Handle(CreateGameRequest request, CancellationToken cancellationToken)
    {
        var creator = await _userManager.GetUserAsync(_contextAccessor!.HttpContext!.User);

        var game = new Game
        {
            Name = request.GameName,
            Status = GameStatus.Created,
            Users = new List<User>
            {
                creator
            }
        };

        _dataContext.Games.Add(game);

        await _dataContext.SaveChangesAsync(cancellationToken);

        var creatorGameMember = await _dataContext.GameMembers
            .Where(gm => gm.GameId == game.Id && gm.UserId == creator.Id)
            .FirstAsync(cancellationToken);

        creatorGameMember.Role = GameMemberRole.Creator;

        await _dataContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}