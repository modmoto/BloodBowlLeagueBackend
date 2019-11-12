using System;
using Microwave.Domain.EventSourcing;

namespace Domain.Players.Events.Players
{
    public class PlayerCreated : IDomainEvent
    {
        public PlayerCreated(Guid playerId,
            Guid teamId,
            int playerPositionNumber,
            PlayerConfig playerConfig)
        {
            PlayerId = playerId;
            TeamId = teamId;
            PlayerPositionNumber = playerPositionNumber;
            PlayerConfig = playerConfig;
        }

        public string EntityId => PlayerId.ToString();
        public Guid TeamId { get; }
        public int PlayerPositionNumber { get; }
        public PlayerConfig PlayerConfig { get; }
        public Guid PlayerId { get; }
    }
}