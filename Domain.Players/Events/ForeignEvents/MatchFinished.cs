using System.Collections.Generic;
using Microwave.Domain;

namespace Domain.Players.Events.ForeignEvents
{
    public class MatchFinished : IDomainEvent
    {
        public MatchFinished(Identity entityId, IEnumerable<PlayerProgression> playerProgressions)
        {
            EntityId = entityId;
            PlayerProgressions = playerProgressions;
        }

        public Identity EntityId { get; }
        public IEnumerable<PlayerProgression> PlayerProgressions { get; }
    }

    public class PlayerProgression
    {
        public PlayerProgression(GuidIdentity playerId, IEnumerable<ProgressionEvent> progressionEvents)
        {
            PlayerId = playerId;
            ProgressionEvents = progressionEvents;
        }

        public GuidIdentity PlayerId { get; }
        public IEnumerable<ProgressionEvent> ProgressionEvents { get; }
    }

    public enum ProgressionEvent
    {
        NominatedMostValuablePlayer, PlayerPassed, PlayerMadeCasualty, PlayerMadeTouchdown
    }
}