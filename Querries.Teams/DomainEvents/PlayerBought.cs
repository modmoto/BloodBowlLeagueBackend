using Microwave.Domain;

namespace Querries.Teams.DomainEvents
{
    public class PlayerBought : IDomainEvent
    {
        public Identity EntityId{ get; set; }
        public GoldCoins NewTeamChestBalance{ get; set; }
        public Identity PlayerTypeId{ get; set; }

        public PlayerBought(GuidIdentity entityId, StringIdentity playerTypeId, GoldCoins newTeamChestBalance)
        {
            EntityId = entityId;
            NewTeamChestBalance = newTeamChestBalance;
            PlayerTypeId = playerTypeId;
        }
    }
}