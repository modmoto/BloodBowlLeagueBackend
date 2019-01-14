using Microwave.Domain;

namespace Application.Players.Events
{
    public class PlayerBought : IDomainEvent
    {
        public Identity EntityId{ get; set; }
        public StringIdentity PlayerTypeId{ get; set; }

        public PlayerBought(GuidIdentity entityId, StringIdentity playerTypeId)
        {
            EntityId = entityId;
            PlayerTypeId = playerTypeId;
        }
    }
}