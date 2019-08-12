namespace Domain.Races.Races
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

        public override bool Equals(object obj)
        {
            var goldCoins = obj as GoldCoins;
            return goldCoins?.Value == Value;
        }

        public override int GetHashCode()
        {
            return Value;
        }
    }
}