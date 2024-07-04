using Microsoft.Extensions.DependencyInjection;
using RouletteGame.Domain.Queries;
using RouletteGame.Services;
using FluentAssertions;
using RouletteGame.Domain.Commands;

namespace RouletteGame.Unit.Tests.Domain.Handlers
{
    public class JoinGameHandlerTests : IClassFixture<JoinGameFixture>
    {
        private readonly JoinGameFixture _fixture;

        public JoinGameHandlerTests(JoinGameFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetPlayerHandler_ShouldReturnEmptyWhenNoPlayerisRegistered()
        {
            var query = new JoinGameCommand(Guid.NewGuid().ToString());
            var response = await _fixture.Send(query);

            response.Should().BeFalse();
        }

        [Fact]
        public async Task GetPlayerHandler_ShouldReturnOnePlayerWhenPlayerisRegistered()
        {
            var service = _fixture.ServiceProvider.GetRequiredService<RouletteService>();
            var player = service.RegisterPlayer("John Doe");
            var query = new JoinGameCommand(player.Id);
            var response = await _fixture.Send(query);

            response.Should().BeTrue();
        }
    }
}
