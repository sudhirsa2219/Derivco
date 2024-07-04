using MediatR;
using RouletteGame.Models;

namespace RouletteGame.Domain.Commands
{
    public record PlaceBetsCommand(string PlayerId, List<Bet> Bets) : IRequest<bool>;
}
