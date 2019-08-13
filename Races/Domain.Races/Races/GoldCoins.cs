namespace Domain.Races.Races
{
    public class GoldCoins
    {
        public int Value { get; }

        public GoldCoins(int value)
        {
            Value = value;
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