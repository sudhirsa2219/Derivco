using MediatR;
using RouletteGame.Domain.Queries;
using RouletteGame.Services;

namespace RouletteGame.Domain.QueryHandlers
{
    public class GetSpinHistoryHandler : IRequestHandler<GetSpinHistoryQuery, List<int>>
    {
        private readonly RouletteService _rouletteService;

        public GetSpinHistoryHandler(RouletteService rouletteService)
        {
            _rouletteService = rouletteService;
        }

        public Task<List<int>> Handle(GetSpinHistoryQuery request, CancellationToken cancellationToken)
        {
            var history = _rouletteService.GetSpinHistory();
            return Task.FromResult(history);
        }
    }
}
