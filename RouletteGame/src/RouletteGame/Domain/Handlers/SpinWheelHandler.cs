using MediatR;
using RouletteGame.Domain.Commands;
using RouletteGame.Models;
using RouletteGame.Services;

namespace RouletteGame.Domain.Handlers
{
    public class SpinWheelHandler : IRequestHandler<SpinWheelCommand, SpinResult>
    {
        private readonly RouletteService _rouletteService;

        public SpinWheelHandler(RouletteService rouletteService)
        {
            _rouletteService = rouletteService;
        }

        public Task<SpinResult> Handle(SpinWheelCommand request, CancellationToken cancellationToken)
        {
            var result = _rouletteService.SpinWheel();
            return Task.FromResult(result);
        }
    }
}
