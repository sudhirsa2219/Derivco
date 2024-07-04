using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RouletteGame.Domain.Queries;
using RouletteGame.Domain.QueryHandlers;
using RouletteGame.Models;
using RouletteGame.Services;

namespace RouletteGame.Unit.Tests.Domain.QueryHandlers
{
    public class GetAllPlayersFixture
    {
        public ServiceProvider ServiceProvider { get; private set; }

        public GetAllPlayersFixture()
        {
            var services = new ServiceCollection();
            ServiceProvider = services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetAllPlayersHandler).Assembly))
                .AddSingleton<RouletteService>()
                .BuildServiceProvider();
        }
        public async Task<List<Player>> Send(GetAllPlayersQuery query)
        {
            var mediator = ServiceProvider.GetRequiredService<IMediator>();
            var response = await mediator.Send(query);
            return response;
        }
    }
}
