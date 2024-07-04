namespace RouletteGame.Models
{
    public class Bet
    {
        public enum BetType { Number, Color, OddEven, HighLow }

        public int Id {  get; set; }
        public BetType Type { get; set; }
        public string BetValue { get; set; }
        public decimal Amount { get; set; }
    }
}
