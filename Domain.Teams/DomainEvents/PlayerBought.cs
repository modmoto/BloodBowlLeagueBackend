using System;
using Microwave.Domain;

namespace Domain.Teams.DomainEvents
{
    public class PlayerBought : IDomainEvent
    {
        public GoldCoins NewTeamChestBalance { get; }
        public Guid PlayerTypeId { get; }

        public PlayerBought(Guid entityId, Guid playerTypeId, GoldCoins newTeamChestBalance, Guid playerId)
        {
            EntityId = entityId;
            NewTeamChestBalance = newTeamChestBalance;
            PlayerId = playerId;
            PlayerTypeId = playerTypeId;
        }

        public Guid EntityId { get; }
        public Guid PlayerId { get; }
    }
}