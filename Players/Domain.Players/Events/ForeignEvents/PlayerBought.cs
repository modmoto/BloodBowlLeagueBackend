using System;
using Microwave.Queries;

namespace Domain.Players.Events.ForeignEvents
{
    public class PlayerBought : ISubscribedDomainEvent
    {
        public PlayerBought(
            Guid teamId,
            string playerTypeId,
            int playerPositionNumber,
            Guid playerId)
        {
            TeamId = teamId;
            PlayerTypeId = playerTypeId;
            PlayerPositionNumber = playerPositionNumber;
            PlayerId = playerId;
        }

        public string PlayerTypeId { get; }
        public int PlayerPositionNumber { get; }
        public Guid PlayerId { get; }
        public Guid TeamId { get; }
        public string EntityId => TeamId.ToString();
    }
}