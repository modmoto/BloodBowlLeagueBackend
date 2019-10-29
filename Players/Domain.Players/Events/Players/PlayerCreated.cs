using System;
using Microwave.Domain;
using Microwave.Domain.EventSourcing;

namespace Domain.Players.Events.Players
{
    public class PlayerCreated : IDomainEvent
    {
        public PlayerCreated(
            Guid playerId,
            Guid teamId,
            PlayerConfig playerConfig,
            string name)
        {
            PlayerId = playerId;
            TeamId = teamId;
            PlayerConfig = playerConfig;
            Name = name;
        }

        public string Name { get; }
        public string EntityId => PlayerId.ToString();
        public Guid TeamId { get; }
        public PlayerConfig PlayerConfig { get; }
        public Guid PlayerId { get; }
    }
}