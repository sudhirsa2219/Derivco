using MediatR;
using RouletteGame.Models;

namespace RouletteGame.Domain.Queries
{
    public record GetPlayerQuery(string playerId) : IRequest<Player>;
}
