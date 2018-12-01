using System;
using Microwave.Domain;

namespace Domain.Teams.DomainEvents
{
    public class PlayerBought : IDomainEvent
    {
        public int PlayerCost { get; }
        public Guid PlayerTypeId { get; }

        public PlayerBought(Guid entityId, Guid playerTypeId, int playerCost)
        {
            EntityId = entityId;
            PlayerCost = playerCost;
            PlayerTypeId = playerTypeId;
        }

        public Guid EntityId { get; }
    }
}