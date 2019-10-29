using System;
using Microwave.Queries;

namespace Domain.Matches.ForeignEvents
{
    public class PlayerBought : ISubscribedDomainEvent
    {
        public PlayerBought(Guid teamId, Guid playerId)
        {
            TeamId = teamId;
            PlayerId = playerId;
        }

        public Guid TeamId { get; }
        public Guid PlayerId { get; }
        public string EntityId => TeamId;
    }
}