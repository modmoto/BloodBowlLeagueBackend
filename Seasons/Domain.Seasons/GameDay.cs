using System.Collections.Generic;
using Microwave.Domain;

namespace Domain.Seasons
{
    public class GameDay
    {
        public GuidIdentity Id { get; }
        public IEnumerable<Matchup> Matchups { get; }

        public GameDay(GuidIdentity id, IEnumerable<Matchup> matchups)
        {
            Id = id;
            Matchups = matchups;
        }
    }
}