using MediatR;
using RouletteGame.Models;

namespace RouletteGame.Domain.Commands
{
    public record PlaceBetsCommand(string PlayerId, List<Bet> Bets) : IRequest;
    //public class PlaceBetCommand : IRequest<PlaceBetResponse>
    //{
    //    public int UserId { get; set; }
    //    public decimal Amount { get; set; }
    //    public string Type { get; set; }
    //    public string Value { get; set; }
    //}
}
