using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Seasons.ReadHost.Matches.Events
{
    public class MatchProgressed : ISubscribedDomainEvent
    {
        public Identity EntityId => MatchId;
        public GuidIdentity MatchId { get; }
        public PlayerProgression PlayerProgression { get; }

        public MatchProgressed(GuidIdentity matchId, PlayerProgression playerProgression)
        {
            MatchId = matchId;
            PlayerProgression = playerProgression;
        }
    }
}