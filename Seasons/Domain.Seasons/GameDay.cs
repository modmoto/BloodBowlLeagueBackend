using System;
using System.Collections.Generic;

namespace Domain.Seasons
{
    public class GameDay
    {
        public Guid Id { get; }
        public IEnumerable<Matchup> Matchups { get; }

        public GameDay(Guid id, IEnumerable<Matchup> matchups)
        {
            Id = id;
            Matchups = matchups;
        }

        public static GameDay Create(IEnumerable<Matchup> matchups)
        {
            return new GameDay(Guid.NewGuid(), matchups);
        }
    }
}