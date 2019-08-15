using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Teams.ReadHost.Players.Events
{
    public class PlayerCreated : ISubscribedDomainEvent
    {
        public PlayerCreated(
            GuidIdentity playerId,
            StringIdentity playerTypeId,
            GuidIdentity teamId,
            string name)
        {
            PlayerId = playerId;
            PlayerTypeId = playerTypeId;
            TeamId = teamId;
            Name = name;
        }

        public GuidIdentity PlayerId { get; }
        public StringIdentity PlayerTypeId { get; }
        public GuidIdentity TeamId { get; }
        public Identity EntityId => PlayerId;
        public string Name { get;  }
    }
}