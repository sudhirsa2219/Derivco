using Microsoft.Extensions.DependencyInjection;
using RouletteGame.Domain.Queries;
using RouletteGame.Services;
using FluentAssertions;
using RouletteGame.Domain.Commands;
using RouletteGame.Models;

namespace RouletteGame.Unit.Tests.Domain.Handlers
{
    public class PlaceBetsHandlerTests : IClassFixture<PlaceBetsFixture>
    {
        private readonly PlaceBetsFixture _fixture;

        public PlaceBetsHandlerTests(PlaceBetsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetPlayerHandler_ShouldReturnEmptyWhenNoPlayerisRegistered()
        {
            List<Bet> bets = new List<Bet>();
            var query = new PlaceBetsCommand(Guid.NewGuid().ToString(), bets);
            var response = await _fixture.Send(query);

            response.Should().BeFalse();
        }

        [Fact]
        public async Task GetPlayerHandler_ShouldReturnOnePlayerWhenPlayerisRegistered()
        {
            List<Bet> bets = new List<Bet>();
            
            var service = _fixture.ServiceProvider.GetRequiredService<RouletteService>();
            var player = service.RegisterPlayer("John Doe");

            bets.Add(new Bet() { Amount = 20, BetValue = "odd", Id=1 });

            var query = new PlaceBetsCommand(player.Id, bets);
            var response = await _fixture.Send(query);

            response.Should().BeTrue();
        }
    }
}
