using MediatR;
using RouletteGame.Domain.Commands;
using RouletteGame.Services;

namespace RouletteGame.Domain.Handlers
{
    public class JoinGameHandler : IRequestHandler<JoinGameCommand,bool>
    {
        private readonly RouletteService _rouletteService;

        public JoinGameHandler(RouletteService rouletteService)
        {
            _rouletteService = rouletteService;
        }

        public Task<bool> Handle(JoinGameCommand request, CancellationToken cancellationToken)
        {
            var res = _rouletteService.JoinGame(request.PlayerId);
            return Task.FromResult(res);
        }
    }
}
