using System;
using System.Collections.Generic;
using Microwave.Queries;

namespace Domain.Players.Events.ForeignEvents
{
    public class MatchFinished : ISubscribedDomainEvent
    {
        public MatchFinished(Guid matchId, IEnumerable<PlayerProgression> playerProgressions)
        {
            MatchId = matchId;
            PlayerProgressions = playerProgressions;
        }

        public Guid MatchId { get; }
        public string EntityId => MatchId.ToString();
        public IEnumerable<PlayerProgression> PlayerProgressions { get; }
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