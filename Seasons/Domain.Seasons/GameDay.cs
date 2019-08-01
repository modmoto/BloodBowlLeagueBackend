using System.Collections.Generic;
using Microwave.Domain.Identities;

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

        public static GameDay Create(IEnumerable<Matchup> matchups)
        {
            return new GameDay(GuidIdentity.Create(), matchups);
        }
    }
}