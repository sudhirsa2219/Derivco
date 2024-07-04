using MediatR;
using RouletteGame.Domain.Queries;
using RouletteGame.Models;
using RouletteGame.Services;
using System.Numerics;

namespace RouletteGame.Domain.QueryHandlers
{
    public class GetPlayerHandler : IRequestHandler<GetPlayerQuery, Player>
    {
        private readonly RouletteService _rouletteService;
        public GetPlayerHandler(RouletteService rouletteService) 
        {
            _rouletteService = rouletteService;
        }
        public Task<Player> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
        {
            var result = _rouletteService.GetPlayer(request.playerId);
            return Task.FromResult(result);
        }
    }
}
