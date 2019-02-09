using Microwave.Domain;

namespace Teams.ReadHost.Teams.TeamReadmodels
{
    public class AllowedPlayer
    {
        public AllowedPlayer(StringIdentity playerTypeId, int maximumPlayers, GoldCoins cost, string playerDescription)
        {
            PlayerTypeId = playerTypeId;
            MaximumPlayers = maximumPlayers;
            Cost = cost;
            PlayerDescription = playerDescription;
        }

        public Identity PlayerTypeId{ get; set; }
        public int MaximumPlayers{ get; set; }
        public GoldCoins Cost{ get; set; }
        public string PlayerDescription{ get; set; }
    }
}