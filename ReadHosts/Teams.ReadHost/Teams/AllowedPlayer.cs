using Microwave.Domain.Identities;

namespace Teams.ReadHost.Teams
{
    public class AllowedPlayer
    {
        public AllowedPlayer(StringIdentity playerTypeId, int maximumPlayers, GoldCoins cost)
        {
            PlayerTypeId = playerTypeId;
            MaximumPlayers = maximumPlayers;
            Cost = cost;
        }

        public Identity PlayerTypeId{ get; set; }
        public int MaximumPlayers{ get; set; }
        public GoldCoins Cost{ get; set; }
    }
}