using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RouletteGame.Domain.Commands;
using RouletteGame.Domain.Handlers;
using RouletteGame.Domain.Queries;
using RouletteGame.Domain.QueryHandlers;
using RouletteGame.Models;
using RouletteGame.Services;

namespace RouletteGame.Unit.Tests.Domain.Handlers
{
    public class JoinGameFixture
    {
        public ServiceProvider ServiceProvider { get; private set; }

        public JoinGameFixture()
        {
            var services = new ServiceCollection();
            ServiceProvider = services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(JoinGameHandler).Assembly))
                .AddSingleton<RouletteService>()
                .BuildServiceProvider();
        }
        public async Task<bool> Send(JoinGameCommand query)
        {
            var mediator = ServiceProvider.GetRequiredService<IMediator>();
            var response = await mediator.Send(query);
            return response;
        }
    }
}
