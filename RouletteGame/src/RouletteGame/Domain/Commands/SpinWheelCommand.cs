using MediatR;
using RouletteGame.Models;

namespace RouletteGame.Domain.Commands
{
    public record SpinWheelCommand : IRequest<SpinResult>;
}
