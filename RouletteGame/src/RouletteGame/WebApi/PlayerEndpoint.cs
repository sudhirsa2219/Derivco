using Carter;
using MediatR;
using RouletteGame.Domain.Commands;
using RouletteGame.Services;

namespace RouletteGame.WebApi
{
    public class PlayerEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/register", async (HttpContext http, IMediator mediator) =>
            {
                var name = http.Request.Query["name"].ToString();
                var player = await mediator.Send(new RegisterPlayerCommand(name));
                await http.Response.WriteAsJsonAsync(player);
            });

            app.MapGet("/player/{playerId}", (string playerId, RouletteService rouletteService) =>
            {
                var player = rouletteService.GetPlayer(playerId);
                return player != null ? Results.Ok(player) : Results.NotFound();
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
