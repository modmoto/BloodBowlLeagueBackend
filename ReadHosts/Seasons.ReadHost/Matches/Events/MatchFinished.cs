using System.Collections.Generic;
using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Seasons.ReadHost.Matches.Events
{
    public class MatchFinished : ISubscribedDomainEvent
    {
        public GuidIdentity MatchId { get; set; }
        public Identity EntityId => MatchId;
        public IEnumerable<PlayerProgression> PlayerProgressions { get; set; }
        public GameResult GameResult { get; set; }
    }

    public class PlayerProgression
    {
        public GuidIdentity PlayerId { get; set; }
        public IEnumerable<ProgressionEvent> ProgressionEvents { get; set; }
    }

    public enum ProgressionEvent
    {
        NominatedMostValuablePlayer, PlayerPassed, PlayerMadeCasualty, PlayerMadeTouchdown
    }
}