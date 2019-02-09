using Microwave.Domain;

namespace Teams.ReadHost.Teams.TeamReadmodels.Events
{
    public class PlayerBought : IDomainEvent
    {
        public PlayerBought(GuidIdentity entityId, StringIdentity playerTypeId, GuidIdentity playerId, GoldCoins newTeamChestBalance)
        {
            EntityId = entityId;
            NewTeamChestBalance = newTeamChestBalance;
            PlayerTypeId = playerTypeId;
            PlayerId = playerId;
        }

        public Identity EntityId { get; }
        public GoldCoins NewTeamChestBalance { get; }
        public StringIdentity PlayerTypeId { get; }
        public GuidIdentity PlayerId { get; }
    }
}