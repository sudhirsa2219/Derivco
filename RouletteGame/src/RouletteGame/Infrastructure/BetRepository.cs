using Dapper;
using RouletteGame.Models;

namespace RouletteGame.Infrastructure
{
    public class BetRepository
    {
        private readonly DapperContext _context;

        public BetRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Bet> AddBetAsync(Bet bet)
        {
            const string sql = @"
                INSERT INTO Bets (UserId, Amount, Type, Value, IsResolved, Payout)
                VALUES (@UserId, @Amount, @Type, @Value, @IsResolved, @Payout);
                SELECT last_insert_rowid();";

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.ExecuteScalarAsync<int>(sql, bet);
                bet.Id = id;
                return bet;
            }
        }

        public async Task<IEnumerable<Bet>> GetUnresolvedBetsAsync()
        {
            const string sql = "SELECT * FROM Bets WHERE IsResolved = 0";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Bet>(sql);
            }
        }
    }
}
