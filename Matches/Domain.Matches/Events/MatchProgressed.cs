using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;

namespace Domain.Matches.Events
{
    public class MatchProgressed : IDomainEvent
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