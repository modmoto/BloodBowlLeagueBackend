using System.Collections.Generic;
using System.Linq;
using Microwave.Domain;

namespace Domain.Seasons
{
    public class MatchPairingService
    {
        public IEnumerable<GameDay> ComputeFixtures(IEnumerable<GuidIdentity> listTeam)
        {
            var result = new List<GameDay>();

            var teams = listTeam.ToList();
            var numberOfRounds = teams.Count - 1;
            var numberOfMatchesInARound = teams.Count / 2;

            var teamsTemp = new List<GuidIdentity>();

            teamsTemp.AddRange(teams.Skip(numberOfMatchesInARound).Take(numberOfMatchesInARound));
            teamsTemp.AddRange(teams.Skip(1).Take(numberOfMatchesInARound - 1).ToArray().Reverse());

            var numberOfTeams = teamsTemp.Count;

            for (var roundNumber = 0; roundNumber < numberOfRounds; roundNumber++)
            {
                var teamIdx = roundNumber % numberOfTeams;

                var matchups = new List<Matchup>();
                matchups.Add(new Matchup(GuidIdentity.Create(), teamsTemp[teamIdx], teams[0]));

                for (var idx = 1; idx < numberOfMatchesInARound; idx++)
                {
                    var firstTeamIndex = (roundNumber + idx) % numberOfTeams;
                    var secondTeamIndex = (roundNumber + numberOfTeams - idx) % numberOfTeams;

                    matchups.Add(new Matchup(GuidIdentity.Create(), teamsTemp[firstTeamIndex], teamsTemp[secondTeamIndex]));
                }

                var round = new GameDay(GuidIdentity.Create(), matchups);
                result.Add(round);
            }

            return result;
        }
    }
}