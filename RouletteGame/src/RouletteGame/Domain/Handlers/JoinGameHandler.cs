using MediatR;
using RouletteGame.Domain.Commands;
using RouletteGame.Services;

namespace RouletteGame.Domain.Handlers
{
    public class JoinGameHandler : IRequestHandler<JoinGameCommand>
    {
        private readonly RouletteService _rouletteService;

        public JoinGameHandler(RouletteService rouletteService)
        {
            _rouletteService = rouletteService;
        }

        public Task Handle(JoinGameCommand request, CancellationToken cancellationToken)
        {
            _rouletteService.JoinGame(request.PlayerId);
            return Task.FromResult(Unit.Value);
        }
    }
}
