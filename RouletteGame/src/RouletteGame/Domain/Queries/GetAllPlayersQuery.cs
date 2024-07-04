using MediatR;
using RouletteGame.Models;

namespace RouletteGame.Domain.Queries
{
    public record GetAllPlayersQuery: IRequest<List<Player>>;
}
