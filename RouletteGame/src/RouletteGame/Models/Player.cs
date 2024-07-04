namespace RouletteGame.Models
{
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; } = 1000;
        public List<Bet> Bets { get; set; } = new List<Bet>();
        public bool HasJoined { get; set; } = false;
    }
}
