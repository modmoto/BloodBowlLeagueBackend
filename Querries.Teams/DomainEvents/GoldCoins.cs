namespace Querries.Teams.DomainEvents
{
    public class GoldCoins
    {
        public int Value { get; set; }

        public GoldCoins(int value)
        {
            Value = value;
        }
    }
}