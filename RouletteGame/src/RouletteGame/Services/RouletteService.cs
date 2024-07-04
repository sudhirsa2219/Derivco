using RouletteGame.Models;
using System.Collections.Concurrent;

namespace RouletteGame.Services
{
    public class RouletteService
    {
        private const int MaxBetsPerPlayer = 50;
        private const double BetTimeLimitInSeconds = 30.0;
        private ConcurrentDictionary<string, Player> players = new ConcurrentDictionary<string, Player>();
        private List<int> spinHistory = new List<int>();
        private Random rand = new Random();
        private System.Timers.Timer betTimer;
        private bool spinInProgress = false;

        public RouletteService()
        {
            betTimer = new System.Timers.Timer(BetTimeLimitInSeconds * 1000);
            betTimer.Elapsed += (sender, e) => ExecuteSpin();
        }

        public Player RegisterPlayer(string name)
        {
            var player = new Player { Id = Guid.NewGuid().ToString(), Name = name };
            players[player.Id] = player;
            return player;
        }

        public Player GetPlayer(string playerId)
        {
            players.TryGetValue(playerId, out var player);
            return player;
        }

        public void JoinGame(string playerId)
        {
            if (players.TryGetValue(playerId, out var player))
            {
                player.HasJoined = true;
                if (!spinInProgress)
                {
                    StartBetting();
                }
            }
        }

        public void WithdrawFromGame(string playerId)
        {
            if (players.TryGetValue(playerId, out var player))
            {
                player.HasJoined = false;
            }
        }

        public void PlaceBets(string playerId, List<Bet> bets)
        {
            if (players.TryGetValue(playerId, out var player))
            {
                if (spinInProgress)
                {
                    throw new InvalidOperationException("Bets cannot be placed during a spin.");
                }
                if (player.Bets.Count + bets.Count > MaxBetsPerPlayer)
                {
                    throw new InvalidOperationException($"A player cannot place more than {MaxBetsPerPlayer} bets per spin.");
                }
                player.Bets.AddRange(bets);
            }
        }

        private void StartBetting()
        {
            spinInProgress = false;
            betTimer.Start();
        }

        public SpinResult SpinWheel()
        {
            int number = rand.Next(0, 37);
            string color = number == 0 ? "green" : number % 2 == 0 ? "black" : "red";
            bool isOdd = number % 2 != 0;
            bool isHigh = number > 18;

            var spinResult = new SpinResult { Number = number, Color = color, IsOdd = isOdd, IsHigh = isHigh };
            spinHistory.Add(number);

            CalculatePayouts(spinResult);

            return spinResult;
        }

        private void ExecuteSpin()
        {
            if (spinInProgress) return;

            spinInProgress = true;
            betTimer.Stop();

            int number = rand.Next(0, 37);
            string color = number == 0 ? "green" : number % 2 == 0 ? "black" : "red";
            bool isOdd = number % 2 != 0;
            bool isHigh = number > 18;

            var spinResult = new SpinResult { Number = number, Color = color, IsOdd = isOdd, IsHigh = isHigh };
            spinHistory.Add(number);

            CalculatePayouts(spinResult);
            ResetPlayerBets();

            spinInProgress = false;
        }

        private void CalculatePayouts(SpinResult spinResult)
        {
            foreach (var player in players.Values.Where(p => p.HasJoined))
            {
                decimal totalPayout = 0;
                foreach (var bet in player.Bets)
                {
                    bool win = false;
                    decimal payout = 0;

                    switch (bet.Type)
                    {
                        case Bet.BetType.Number:
                            if (bet.BetValue == spinResult.Number.ToString())
                            {
                                win = true;
                                payout = bet.Amount * 35;
                            }
                            break;
                        case Bet.BetType.Color:
                            if (bet.BetValue == spinResult.Color)
                            {
                                win = true;
                                payout = bet.Amount * 2;
                            }
                            break;
                        case Bet.BetType.OddEven:
                            if (bet.BetValue == "odd" && spinResult.IsOdd || bet.BetValue == "even" && !spinResult.IsOdd)
                            {
                                win = true;
                                payout = bet.Amount * 2;
                            }
                            break;
                        case Bet.BetType.HighLow:
                            if (bet.BetValue == "high" && spinResult.IsHigh || bet.BetValue == "low" && !spinResult.IsHigh)
                            {
                                win = true;
                                payout = bet.Amount * 2;
                            }
                            break;
                    }

                    if (win)
                    {
                        player.Balance += payout;
                        totalPayout += payout;
                    }
                    else
                    {
                        player.Balance -= bet.Amount;
                    }

                    spinResult.PayoutDetails.Add(new PayoutDetail
                    {
                        PlayerId = player.Id,
                        PlayerName = player.Name,
                        BetAmount = bet.Amount,
                        PayoutAmount = win ? payout : 0,
                        IsWin = win
                    });
                }

                spinResult.PayoutDetails.Add(new PayoutDetail
                {
                    PlayerId = player.Id,
                    PlayerName = player.Name,
                    BetAmount = player.Bets.Sum(b => b.Amount),
                    PayoutAmount = totalPayout,
                    IsWin = totalPayout > 0
                });
            }
        }

        private void ResetPlayerBets()
        {
            foreach (var player in players.Values)
            {
                player.Bets.Clear();
            }
        }

        public List<int> GetSpinHistory()
        {
            return spinHistory;
        }
    }
}
