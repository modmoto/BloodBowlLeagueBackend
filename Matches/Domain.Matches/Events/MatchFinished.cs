using System;
using System.Collections.Generic;
using Microwave.Domain.EventSourcing;

namespace Domain.Matches.Events
{
    public class MatchFinished : IDomainEvent
    {
        public MatchFinished(
            Guid matchId,
            IEnumerable<PlayerProgression> playerProgressions,
            GameResult gameResult)
        {
            MatchId = matchId;
            PlayerProgressions = playerProgressions;
            GameResult = gameResult;
        }

        public Guid MatchId { get; }
        public string EntityId => MatchId.ToString();
        public IEnumerable<PlayerProgression> PlayerProgressions { get; }
        public GameResult GameResult { get; }
    }

    public class PlayerProgression
    {
        public PlayerProgression(Guid playerId, ProgressionEvent progressionEvent)
        {
            PlayerId = playerId;
            ProgressionEvent = progressionEvent;
        }

        public Guid PlayerId { get; }
        public ProgressionEvent ProgressionEvent { get; }
    }

    public enum ProgressionEvent
    {
        NominatedMostValuablePlayer, PlayerPassed, PlayerMadeCasualty, PlayerMadeTouchdown
    }
}