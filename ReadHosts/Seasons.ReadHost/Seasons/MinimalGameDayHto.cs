using System;
using System.Collections.Generic;
using Seasons.ReadHost.Matches;

namespace Seasons.ReadHost.Seasons
{
    public class MinimalGameDayHto
    {

        public MinimalGameDayHto(Guid id, List<MinimalMatchHto> matchups)
        {
            Id = id;
            Matchups = matchups;
        }

        public Guid Id { get; }
        public List<MinimalMatchHto> Matchups { get; }
    }
}