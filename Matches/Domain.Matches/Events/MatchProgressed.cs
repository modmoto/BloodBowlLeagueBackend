using System;
using Microwave.Domain.EventSourcing;

namespace Domain.Matches.Events
{
    public class MatchProgressed : IDomainEvent
    {
        public string EntityId => MatchId.ToString();
        public Guid MatchId { get; }
        public PlayerProgression PlayerProgression { get; }
        public GameResult GameResult { get; }

        public MatchProgressed(
            Guid matchId,
            PlayerProgression playerProgression,
            GameResult gameResult)
        {
            MatchId = matchId;
            PlayerProgression = playerProgression;
            GameResult = gameResult;
        }
    }
}