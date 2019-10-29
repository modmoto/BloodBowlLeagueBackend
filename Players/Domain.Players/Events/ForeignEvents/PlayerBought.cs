using System;

namespace Domain.Players.Events.ForeignEvents
{
    public class PlayerBought : ISubscribedDomainEvent
    {
        public PlayerBought(
            Guid teamId,
            string playerTypeId,
            Guid playerId)
        {
            TeamId = teamId;
            PlayerTypeId = playerTypeId;
            PlayerId = playerId;
        }

        public string PlayerTypeId { get; }
        public Guid PlayerId { get; }
        public Guid TeamId { get; }
        public string EntityId => TeamId;
    }
}