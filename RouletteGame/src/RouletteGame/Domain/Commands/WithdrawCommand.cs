using MediatR;

namespace RouletteGame.Domain.Commands
{
    public record WithdrawCommand(string PlayerId) : IRequest;
}
