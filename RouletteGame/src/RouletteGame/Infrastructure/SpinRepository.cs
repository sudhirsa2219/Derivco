using Dapper;
using RouletteGame.Models;

namespace RouletteGame.Infrastructure
{
    public class SpinRepository
    {
        private readonly DapperContext _context;

        public SpinRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Spin> AddSpinAsync(Spin spin)
        {
            const string sql = @"
                INSERT INTO Spins (Timestamp, Result)
                VALUES (@Timestamp, @Result);
                SELECT last_insert_rowid();";

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.ExecuteScalarAsync<int>(sql, spin);
                spin.Id = id;
                return spin;
            }
        }

        public async Task<IEnumerable<Spin>> GetPreviousSpinsAsync()
        {
            const string sql = "SELECT * FROM Spins";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Spin>(sql);
            }
        }
    }
}
