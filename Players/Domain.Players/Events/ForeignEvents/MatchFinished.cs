using System.Collections.Generic;
using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Domain.Players.Events.ForeignEvents
{
    public class MatchFinished : ISubscribedDomainEvent
    {
        public MatchFinished(IEnumerable<PlayerProgression> playerProgressions)
        {
            PlayerProgressions = playerProgressions;
        }
        public IEnumerable<PlayerProgression> PlayerProgressions { get; }
        public Identity EntityId { get; }
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