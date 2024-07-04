using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RouletteGame.Domain.Queries;
using RouletteGame.Services;
using RouletteGame.Unit.Tests.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteGame.Unit.Tests.Domain.QueryHandlers
{
    public class GetAllPlayersHandlerTests : IClassFixture<GetAllPlayersFixture>
    {
        private readonly GetAllPlayersFixture _fixture;

        public GetAllPlayersHandlerTests(GetAllPlayersFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetAllPlayersHandler_ShouldReturnEmptyWhenNoPlayerisPresent()
        {
            var query = new GetAllPlayersQuery();
            var response = await _fixture.Send(query);

            response.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllPlayersHandler_ShouldReturnOnePlayerWhenPlayerisRegistered()
        {
            var service = _fixture.ServiceProvider.GetRequiredService<RouletteService>();
            var player = service.RegisterPlayer("John Doe");
            var query = new GetAllPlayersQuery();
            var response = await _fixture.Send(query);

            response.Count.Should().Be(1);
        }
    }
}
