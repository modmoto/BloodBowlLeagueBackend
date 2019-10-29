using System;
using Microwave.Domain.EventSourcing;

namespace Domain.Teams.DomainEvents
{
    public class PlayerAddedToDraft : IDomainEvent
    {
        public PlayerAddedToDraft(
            Guid teamId,
            string playerTypeId,
            Guid playerId,
            GoldCoins newTeamChestBalance)
        {
            TeamId = teamId;
            NewTeamChestBalance = newTeamChestBalance;
            PlayerTypeId = playerTypeId;
            PlayerId = playerId;
        }

        public string EntityId => TeamId.ToString();
        public Guid TeamId { get; }
        public GoldCoins NewTeamChestBalance { get; }
        public string PlayerTypeId { get; }
        public Guid PlayerId { get; }
    }
}