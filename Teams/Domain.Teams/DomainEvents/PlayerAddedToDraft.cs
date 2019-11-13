using System;
using Microwave.Domain.EventSourcing;

namespace Domain.Teams.DomainEvents
{
    public class PlayerAddedToDraft : IDomainEvent
    {
        public PlayerAddedToDraft(
            Guid teamId,
            string playerTypeId,
            int playerPositionNumber,
            Guid playerId,
            GoldCoins newTeamChestBalance)
        {
            TeamId = teamId;
            NewTeamChestBalance = newTeamChestBalance;
            PlayerTypeId = playerTypeId;
            PlayerPositionNumber = playerPositionNumber;
            PlayerId = playerId;
        }

        public string EntityId => TeamId.ToString();
        public Guid TeamId { get; }
        public GoldCoins NewTeamChestBalance { get; }
        public string PlayerTypeId { get; }
        public int PlayerPositionNumber { get; }
        public Guid PlayerId { get; }
    }
}