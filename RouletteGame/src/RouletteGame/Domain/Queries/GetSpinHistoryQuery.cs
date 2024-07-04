using MediatR;

namespace RouletteGame.Domain.Queries
{
    public record GetSpinHistoryQuery : IRequest<List<int>>;
}
