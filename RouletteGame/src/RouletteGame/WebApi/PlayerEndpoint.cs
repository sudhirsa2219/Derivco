using Carter;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using RouletteGame.Domain.Commands;
using RouletteGame.Domain.Queries;
using RouletteGame.Services;
using System.Xml.Linq;

namespace RouletteGame.WebApi
{
    public class PlayerEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/register", async (string name, IMediator mediator) =>
            {
                //var name = http.Request.Query["name"].ToString();
                var player = await mediator.Send(new RegisterPlayerCommand(name));
                return Results.Ok(player);
            });

            app.MapGet("/player/{playerId}", async (string playerId, IMediator mediator) =>
            {
                var player = await mediator.Send(new GetPlayerQuery(playerId));
                return player != null ? Results.Ok(player) : Results.NotFound();
            });

            app.MapGet("/allplayers/", async (IMediator mediator) =>
            {
                var players = await mediator.Send(new GetAllPlayersQuery());
                return players != null ? Results.Ok(players) : Results.NotFound();
            });

            app.MapPost("/join/{playerId}", async (string playerId, IMediator mediator) =>
            {
                await mediator.Send(new JoinGameCommand(playerId));
                return Results.Ok();
            });

            app.MapPost("/withdraw/{playerId}", async (string playerId, IMediator mediator) =>
            {
                await mediator.Send(new WithdrawCommand(playerId));
                return Results.Ok();
            });
        }
    }
}
