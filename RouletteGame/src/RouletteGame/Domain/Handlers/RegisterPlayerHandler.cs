using MediatR;
using RouletteGame.Domain.Commands;
using RouletteGame.Models;
using RouletteGame.Services;

namespace RouletteGame.Domain.Handlers
{
    public class RegisterPlayerHandler : IRequestHandler<RegisterPlayerCommand, Player>
    {
        private readonly RouletteService _rouletteService;

        public RegisterPlayerHandler(RouletteService rouletteService)
        {
            _rouletteService = rouletteService;
        }

        public Task<Player> Handle(RegisterPlayerCommand request, CancellationToken cancellationToken)
        {
            var player = _rouletteService.RegisterPlayer(request.Name);
            return Task.FromResult(player);
        }
    }
}
