using System;
using Microwave.Domain;

namespace Querries.Teams.DomainEvents
{
    public class PlayerBought : IDomainEvent
    {
        public Guid EntityId{ get; set; }
        public GoldCoins NewTeamChestBalance{ get; set; }
        public Guid PlayerTypeId{ get; set; }

        public PlayerBought(Guid entityId, Guid playerTypeId, GoldCoins newTeamChestBalance)
        {
            EntityId = entityId;
            NewTeamChestBalance = newTeamChestBalance;
            PlayerTypeId = playerTypeId;
        }
    }
}