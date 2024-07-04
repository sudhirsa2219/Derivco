using MediatR;
using RouletteGame.Domain.Commands;
using RouletteGame.Services;

namespace RouletteGame.Domain.Handlers
{
    public class PlaceBetsHandler : IRequestHandler<PlaceBetsCommand>
    {
        private readonly RouletteService _rouletteService;

        public PlaceBetsHandler(RouletteService rouletteService)
        {
            _rouletteService = rouletteService;
        }

        public Task Handle(PlaceBetsCommand request, CancellationToken cancellationToken)
        {
            _rouletteService.PlaceBets(request.PlayerId, request.Bets);
            return Task.FromResult(Unit.Value);
        }
    }
}
