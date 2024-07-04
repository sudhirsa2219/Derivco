using MediatR;
using RouletteGame.Domain.Queries;
using RouletteGame.Models;
using RouletteGame.Services;

namespace RouletteGame.Domain.QueryHandlers
{
    public class GetAllPlayersHandler : IRequestHandler<GetAllPlayersQuery, List<Player>>
    {
        private readonly RouletteService _rouletteService;
        public GetAllPlayersHandler(RouletteService rouletteService)
        {
            _rouletteService = rouletteService;
        }
        public Task<List<Player>> Handle(GetAllPlayersQuery request, CancellationToken cancellationToken)
        {
            var result = _rouletteService.GetAllPlayers();
            return Task.FromResult(result);
        }
    }
}
