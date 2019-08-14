using System.Collections.Generic;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;

namespace Domain.Matches.Events
{
    public class MatchFinished : IDomainEvent
    {
        public MatchFinished(
            GuidIdentity matchId,
            IEnumerable<PlayerProgression> playerProgressions,
            GameResult gameResult)
        {
            MatchId = matchId;
            PlayerProgressions = playerProgressions;
            GameResult = gameResult;
        }

        public GuidIdentity MatchId { get; }
        public Identity EntityId => MatchId;
        public IEnumerable<PlayerProgression> PlayerProgressions { get; }
        public GameResult GameResult { get; }
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