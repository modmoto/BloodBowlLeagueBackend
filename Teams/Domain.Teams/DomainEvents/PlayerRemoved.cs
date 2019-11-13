using System;
using Microwave.Domain.EventSourcing;

namespace Domain.Teams.DomainEvents
{
    public class PlayerRemoved : IDomainEvent
    {
        public Guid TeamId { get; }

        public string EntityId => TeamId.ToString();
        public Guid PlayerId { get; }
        public GoldCoins NewTeamChestBalance { get; }

        public PlayerRemoved(Guid teamId, Guid playerId, GoldCoins newTeamChestBalance)
        {
            TeamId = teamId;
            PlayerId = playerId;
            NewTeamChestBalance = newTeamChestBalance;
        }
    }
}