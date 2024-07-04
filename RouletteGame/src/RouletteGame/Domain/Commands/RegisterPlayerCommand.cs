using MediatR;
using RouletteGame.Models;
using System.Numerics;

namespace RouletteGame.Domain.Commands
{
    public record RegisterPlayerCommand(string Name) : IRequest<Player>;
}
