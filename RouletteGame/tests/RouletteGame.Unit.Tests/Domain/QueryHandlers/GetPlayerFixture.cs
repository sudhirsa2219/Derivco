using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RouletteGame.Domain.Queries;
using RouletteGame.Domain.QueryHandlers;
using RouletteGame.Models;
using RouletteGame.Services;

namespace RouletteGame.Unit.Tests.Domain.QueryHandlers
{
    public class JoinGameFixture
    {
        public ServiceProvider ServiceProvider { get; private set; }

        public JoinGameFixture()
        {
            var services = new ServiceCollection();
            ServiceProvider = services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetPlayerHandler).Assembly))
                .AddSingleton<RouletteService>()
                .BuildServiceProvider();
        }
        public async Task<Player> Send(GetPlayerQuery query)
        {
            var mediator = ServiceProvider.GetRequiredService<IMediator>();
            var response = await mediator.Send(query);
            return response;
        }
    }
}
