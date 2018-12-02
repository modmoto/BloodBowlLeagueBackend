using System;
using Microwave.Domain;

namespace Domain.Teams.DomainEvents
{
    public class PlayerBought : IDomainEvent
    {
        public int PlayerCost { get; }
        public Guid PlayerTypeId { get; }

        public PlayerBought(Guid entityId, Guid playerTypeId, int playerCost, Guid playerId)
        {
            EntityId = entityId;
            PlayerCost = playerCost;
            PlayerId = playerId;
            PlayerTypeId = playerTypeId;
        }

        public Guid EntityId { get; }
        public Guid PlayerId { get; }
    }
}