using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Teams.ReadHost.Players.Events
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

        public GuidIdentity PlayerId { get; }
        public PlayerConfig PlayerConfig { get; }
        public StringIdentity PlayerTypeId => PlayerConfig.PlayerTypeId;
        public GuidIdentity TeamId { get; }
        public Identity EntityId => PlayerId;
        public string Name { get;  }
    }

    public class PlayerConfig
    {
        public StringIdentity PlayerTypeId { get; set; }
    }
}