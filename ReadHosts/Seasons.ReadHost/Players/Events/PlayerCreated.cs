using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Seasons.ReadHost.Players.Events
{
    public class PlayerCreated : ISubscribedDomainEvent
    {
        public PlayerCreated(
            GuidIdentity playerId,
            PlayerConfig playerConfig,
            GuidIdentity teamId,
            string name)
        {
            PlayerId = playerId;
            PlayerConfig = playerConfig;
            TeamId = teamId;
            Name = name;
        }

        public string Name { get; }
        public GuidIdentity PlayerId { get; }
        public PlayerConfig PlayerConfig { get; }
        public GuidIdentity TeamId { get; }
        public Identity EntityId => PlayerId;
    }

    public class PlayerConfig
    {
        public PlayerConfig(
            StringIdentity playerTypeId)
        {
            PlayerTypeId = playerTypeId;
        }

        public StringIdentity PlayerTypeId { get; }
    }
}