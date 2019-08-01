using System.Collections.Generic;
using Domain.Seasons.Events;
using Microwave.Domain.Identities;
using Microwave.Domain.Validation;

namespace Domain.Seasons
{
    public class GameDay
    {
        public GuidIdentity Id { get; }
        public IEnumerable<Matchup> Matchups { get; }

        public GameDay(IEnumerable<Matchup> matchups)
        {
            Id = GuidIdentity.Create();
            Matchups = matchups;
        }
    }
}