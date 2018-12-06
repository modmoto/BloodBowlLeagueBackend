using System;
using Microwave.Domain;

namespace Domain.Teams.DomainEvents
{
    public class PlayerBought : IDomainEvent
    {
        public Guid EntityId { get; }
        public GoldCoins NewTeamChestBalance { get; }
        public Guid PlayerTypeId { get; }

        public PlayerBought(Guid entityId, Guid playerTypeId, GoldCoins newTeamChestBalance)
        {
            EntityId = entityId;
            NewTeamChestBalance = newTeamChestBalance;
            PlayerTypeId = playerTypeId;
        }
    }
}