using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Teams.ReadHost.Players.Events
{
    public class PlayerCreated : ISubscribedDomainEvent
    {
        public PlayerCreated(
            GuidIdentity playerId,
            StringIdentity playerTypeId,
            GuidIdentity teamId)
        {
            PlayerId = playerId;
            PlayerTypeId = playerTypeId;
            TeamId = teamId;
        }

        public GuidIdentity PlayerId { get; }
        public StringIdentity PlayerTypeId { get; }
        public GuidIdentity TeamId { get; }
        public Identity EntityId => PlayerId;
    }
}