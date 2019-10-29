namespace Teams.ReadHost.Teams
{
    public class AllowedPlayer
    {
        public AllowedPlayer(string playerTypeId, int maximumPlayers, GoldCoins cost)
        {
            PlayerTypeId = playerTypeId;
            MaximumPlayers = maximumPlayers;
            Cost = cost;
        }

        public string PlayerTypeId{ get; set; }
        public int MaximumPlayers{ get; set; }
        public GoldCoins Cost{ get; set; }
    }
}