using System.Collections.Generic;
using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Domain.Players.Events.ForeignEvents
{
    public class MatchFinished : ISubscribedDomainEvent
    {
        public MatchFinished(GuidIdentity matchId, IEnumerable<PlayerProgression> playerProgressions)
        {
            MatchId = matchId;
            PlayerProgressions = playerProgressions;
        }

        public GuidIdentity MatchId { get; }
        public Identity EntityId => MatchId;
        public IEnumerable<PlayerProgression> PlayerProgressions { get; }
    }

    public class PlayerProgression
    {
        public PlayerProgression(GuidIdentity playerId, ProgressionEvent progressionEvent)
        {
            PlayerId = playerId;
            ProgressionEvent = progressionEvent;
        }

        public GuidIdentity PlayerId { get; }
        public ProgressionEvent ProgressionEvent { get; }
    }

    public enum ProgressionEvent
    {
        NominatedMostValuablePlayer, PlayerPassed, PlayerMadeCasualty, PlayerMadeTouchdown
    }
}