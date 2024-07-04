using Microsoft.Data.Sqlite;
using Dapper;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace RouletteGame.Infrastructure
{
    public class DapperContext
    {
        //private readonly string _connectionString;
        protected readonly IConfiguration _configuration;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            return new SqliteConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task Init()
        {
            // create database tables if they don't exist
            using var connection = CreateConnection();
            await _PayoutDetails();

            await _Bet();

            async Task _PayoutDetails()
            {
                var sql = """
                CREATE TABLE IF NOT EXISTS 
                PayoutDetails (
                    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    PlayerId TEXT,
                    PlayerName TEXT,
                    BetAmount TEXT,
                    PayoutAmount TEXT,
                    IsWin TEXT
                );
            """;
                await connection.ExecuteAsync(sql);
            }

            async Task _Bet()
            {
                var sql = """
                CREATE TABLE IF NOT EXISTS 
                Bets (
                    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    BetNumber TEXT,
                    BetType TEXT,
                    BetValue TEXT,
                    BetAmount TEXT
                );
            """;
                await connection.ExecuteAsync(sql);
            }
        }
    }
}
