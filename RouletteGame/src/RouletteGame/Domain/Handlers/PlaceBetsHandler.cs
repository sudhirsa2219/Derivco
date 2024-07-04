using MediatR;
using RouletteGame.Domain.Commands;
using RouletteGame.Services;

namespace RouletteGame.Domain.Handlers
{
    public class PlaceBetsHandler : IRequestHandler<PlaceBetsCommand,bool>
    {
        private readonly RouletteService _rouletteService;

        public PlaceBetsHandler(RouletteService rouletteService)
        {
            _rouletteService = rouletteService;
        }

        public Task<bool> Handle(PlaceBetsCommand request, CancellationToken cancellationToken)
        {
            var res = _rouletteService.PlaceBets(request.PlayerId, request.Bets);
            return Task.FromResult(res);
        }
    }
}
