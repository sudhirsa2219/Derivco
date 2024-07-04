using Carter;
using MediatR;
using RouletteGame.Domain.Commands;
using RouletteGame.Domain.Queries;

namespace RouletteGame.WebApi
{
    public class SpinEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/spin", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new SpinWheelCommand());
                return Results.Ok(result);
            });

            app.MapGet("/history", async (IMediator mediator) =>
            {
                var history = await mediator.Send(new GetSpinHistoryQuery());
                return Results.Ok(history);
            });
        }
    }
}
