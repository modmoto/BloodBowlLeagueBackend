using Microwave.Domain.Identities;

namespace Domain.Teams
{
    public class AllowedPlayer
    {
        public AllowedPlayer(StringIdentity playerTypeId, int maximumPlayers, GoldCoins cost)
        {
            PlayerTypeId = playerTypeId;
            MaximumPlayers = maximumPlayers;
            Cost = cost;
        }

        public StringIdentity PlayerTypeId { get; }
        public int MaximumPlayers { get; }
        public GoldCoins Cost { get; }
    }
}