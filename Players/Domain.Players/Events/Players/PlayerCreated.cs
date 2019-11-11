using System;
using Microwave.Domain.EventSourcing;

namespace Domain.Players.Events.Players
{
    public class PlayerCreated : IDomainEvent
    {
        public PlayerCreated(Guid playerId,
            Guid teamId,
            PlayerConfig playerConfig)
        {
            PlayerId = playerId;
            TeamId = teamId;
            PlayerConfig = playerConfig;
        }

        public string EntityId => PlayerId.ToString();
        public Guid TeamId { get; }
        public PlayerConfig PlayerConfig { get; }
        public Guid PlayerId { get; }
    }
}