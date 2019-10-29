using System;
using Microwave.Queries;

namespace Seasons.ReadHost.Players.Events
{
    public class PlayerCreated : ISubscribedDomainEvent
    {
        public PlayerCreated(
            Guid playerId,
            PlayerConfig playerConfig,
            Guid teamId,
            string name)
        {
            PlayerId = playerId;
            PlayerConfig = playerConfig;
            TeamId = teamId;
            Name = name;
        }

        public string Name { get; }
        public Guid PlayerId { get; }
        public PlayerConfig PlayerConfig { get; }
        public Guid TeamId { get; }
        public string EntityId => PlayerId.ToString();
    }

    public class PlayerConfig
    {
        public PlayerConfig(
            string playerTypeId)
        {
            PlayerTypeId = playerTypeId;
        }

        public string PlayerTypeId { get; }
    }
}