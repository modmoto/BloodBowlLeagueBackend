namespace Domain.Teams
{
    public class GoldCoins
    {
        public int Value { get; }

        public GoldCoins(int value)
        {
            Value = value;
        }

        public GoldCoins Minus(GoldCoins otherCost)
        {
            return new GoldCoins(Value - otherCost.Value);
        }

        public bool MoreThan(GoldCoins otherValue)
        {
            return Value > otherValue.Value;
        }
    }
}