using System;

namespace Querries.Teams.DomainEvents
{
    public class AllowedPlayer
    {
        public AllowedPlayer(Guid playerTypeId, int maximumPlayers, GoldCoins cost, string playerDescription)
        {
            PlayerTypeId = playerTypeId;
            MaximumPlayers = maximumPlayers;
            Cost = cost;
            PlayerDescription = playerDescription;
        }

        public Guid PlayerTypeId{ get; set; }
        public int MaximumPlayers{ get; set; }
        public GoldCoins Cost{ get; set; }
        public string PlayerDescription{ get; set; }
    }
}