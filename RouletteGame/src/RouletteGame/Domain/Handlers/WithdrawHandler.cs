using MediatR;
using RouletteGame.Domain.Commands;
using RouletteGame.Services;

namespace RouletteGame.Domain.Handlers
{
    public class WithdrawHandler : IRequestHandler<WithdrawCommand>
    {
        private readonly RouletteService _rouletteService;

        public WithdrawHandler(RouletteService rouletteService)
        {
            _rouletteService = rouletteService;
        }

        public Task Handle(WithdrawCommand request, CancellationToken cancellationToken)
        {
            _rouletteService.WithdrawFromGame(request.PlayerId);
            return Task.FromResult(Unit.Value);
        }
    }
}
