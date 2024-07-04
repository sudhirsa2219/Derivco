using Carter;
using MediatR;
using RouletteGame.Domain.Commands;
using RouletteGame.Models;

namespace RouletteGame.WebApi
{
    public class BetEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/bets/{playerId}", async (List<Bet> bets, string playerId, IMediator mediator) =>
            {
                await mediator.Send(new PlaceBetsCommand(playerId, bets));
                return Results.Ok();
            });
        }
    }
}
