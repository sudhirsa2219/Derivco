using FluentAssertions;
using RouletteGame.Models;
using RouletteGame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteGame.Unit.Tests.Services
{
    public class RouletteServiceTests : IClassFixture<RouletteServiceFixture>
    {
        private readonly RouletteServiceFixture _fixture;

        public RouletteServiceTests(RouletteServiceFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public void RegisterPlayer_ShouldAddPlayerToService()
        {
            var player = _fixture.Service.RegisterPlayer("John Doe");

            player.Should().NotBeNull();
            player.Name.Should().Be("John Doe");
            var retrievedPlayer = _fixture.Service.GetPlayer(player.Id);
            retrievedPlayer.Should().Be(player);
        }

        [Fact]
        public void JoinGame_ShouldSetPlayerHasJoinedToTrue()
        {
            var player = _fixture.Service.RegisterPlayer("John Doe");

            _fixture.Service.JoinGame(player.Id);

            var retrievedPlayer = _fixture.Service.GetPlayer(player.Id);
            retrievedPlayer.HasJoined.Should().BeTrue();
        }

        [Fact]
        public void WithdrawFromGame_ShouldSetPlayerHasJoinedToFalse()
        {
            var player = _fixture.Service.RegisterPlayer("John Doe");
            _fixture.Service.JoinGame(player.Id);
            _fixture.Service.WithdrawFromGame(player.Id);

            var retrievedPlayer = _fixture.Service.GetPlayer(player.Id);
            retrievedPlayer.HasJoined.Should().BeFalse();
        }

        [Fact]
        public void PlaceBets_ShouldAddBetsToPlayer()
        {
            var player = _fixture.Service.RegisterPlayer("John Doe");
            _fixture.Service.JoinGame(player.Id);
            var bets = new List<Bet>
            {
                new Bet { Type = Bet.BetType.Number, BetValue = "5", Amount = 10 },
                new Bet { Type = Bet.BetType.Color, BetValue = "red", Amount = 20 }
            };

            _fixture.Service.PlaceBets(player.Id, bets);
            var retrievedPlayer = _fixture.Service.GetPlayer(player.Id);
            retrievedPlayer.Bets.Should().HaveCount(2);
        }

        [Fact]
        public void PlaceBets_ShouldThrowExceptionWhenExceedingMaxBets()
        {
            var player = _fixture.Service.RegisterPlayer("John Doe");
            _fixture.Service.JoinGame(player.Id);
            var bets = Enumerable.Range(0, 51).Select(i => new Bet
            {
                Type = Bet.BetType.Number,
                BetValue = i.ToString(),
                Amount = 1
            }).ToList();

            Action act = () => _fixture.Service.PlaceBets(player.Id, bets);

            act.Should().Throw<InvalidOperationException>()
               .WithMessage("A player cannot place more than 50 bets per spin.");
        }

        [Fact]
        public void SpinWheel_ShouldReturnValidSpinResult()
        {
            var player = _fixture.Service.RegisterPlayer("John Doe");
            _fixture.Service.JoinGame(player.Id);

            var result = _fixture.Service.SpinWheel();

            result.Number.Should().BeInRange(0, 36);
            result.Color.Should().BeOneOf("green", "black", "red");
        }

        [Fact]
        public void GetSpinHistory_ShouldReturnHistory()
        {
            var player = _fixture.Service.RegisterPlayer("John Doe");
            _fixture.Service.JoinGame(player.Id);

            // Act
            var result1 = _fixture.Service.SpinWheel();
            var result2 = _fixture.Service.SpinWheel();
            var history = _fixture.Service.GetSpinHistory();

            history.Should().HaveCount(2);
            history.Should().Contain(result1.Number);
            history.Should().Contain(result2.Number);
        }
    }
}
