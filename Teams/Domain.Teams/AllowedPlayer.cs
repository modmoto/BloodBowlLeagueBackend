namespace Domain.Teams
{
    public class AllowedPlayer
    {
        public AllowedPlayer(string playerTypeId, int maximumPlayers, GoldCoins cost)
        {
            PlayerTypeId = playerTypeId;
            MaximumPlayers = maximumPlayers;
            Cost = cost;
        }

        public string PlayerTypeId { get; }
        public int MaximumPlayers { get; }
        public GoldCoins Cost { get; }
    }
}