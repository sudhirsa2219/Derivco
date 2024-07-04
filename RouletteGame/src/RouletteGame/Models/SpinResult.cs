namespace RouletteGame.Models
{
    public class SpinResult
    {
        public int Number { get; set; }
        public string Color { get; set; }
        public bool IsOdd { get; set; }
        public bool IsHigh { get; set; }
        public List<PayoutDetail> PayoutDetails { get; set; } = new List<PayoutDetail>();
    }
}
