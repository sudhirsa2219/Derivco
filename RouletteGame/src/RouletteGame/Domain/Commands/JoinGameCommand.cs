using MediatR;

namespace RouletteGame.Domain.Commands
{
    public record JoinGameCommand(string PlayerId) : IRequest;
}
