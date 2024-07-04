using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RouletteGame.Domain.Commands;
using RouletteGame.Domain.Handlers;
using RouletteGame.Services;

namespace RouletteGame.Unit.Tests.Domain.Handlers
{
    public class PlaceBetsFixture
    {
        public ServiceProvider ServiceProvider { get; private set; }

        public PlaceBetsFixture()
        {
            var services = new ServiceCollection();
            ServiceProvider = services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(PlaceBetsHandler).Assembly))
                .AddSingleton<RouletteService>()
                .BuildServiceProvider();
        }
        public async Task<bool> Send(PlaceBetsCommand query)
        {
            var mediator = ServiceProvider.GetRequiredService<IMediator>();
            var response = await mediator.Send(query);
            return response;
        }
    }
}
