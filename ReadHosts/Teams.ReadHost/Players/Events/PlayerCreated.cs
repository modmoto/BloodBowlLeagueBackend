using System;
using Microwave.Queries;

namespace Teams.ReadHost.Players.Events
{
    public class PlayerCreated : ISubscribedDomainEvent
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
        public string PlayerTypeId => PlayerConfig.PlayerTypeId;
    }
}