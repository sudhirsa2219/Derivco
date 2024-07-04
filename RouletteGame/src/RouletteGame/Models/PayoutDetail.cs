namespace RouletteGame.Models
{
    public class PayoutDetail
    {
        public string PlayerId { get; set; }
        public string PlayerName { get; set; }
        public decimal BetAmount { get; set; }
        public decimal PayoutAmount { get; set; }
        public bool IsWin { get; set; }
    }
}
