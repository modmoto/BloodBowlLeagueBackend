using System.Collections.Generic;
using System.Linq;
using Microwave.Domain.Identities;

namespace Domain.Seasons
{
    public class MatchPairingService
    {
        public IEnumerable<GameDay> ComputePairings(IEnumerable<GuidIdentity> listTeam)
        {
            var teams = listTeam.ToList();
            var numberOfRounds = teams.Count - 1;
            var numberOfMatchesInARound = teams.Count / 2;

            var teamsTemp = new List<GuidIdentity>();

            teamsTemp.AddRange(teams.Skip(numberOfMatchesInARound).Take(numberOfMatchesInARound));
            teamsTemp.AddRange(teams.Skip(1).Take(numberOfMatchesInARound - 1).Reverse());

            var numberOfTeams = teamsTemp.Count;

            var gameDays = new List<GameDay>();

            for (var roundNumber = 0; roundNumber < numberOfRounds; roundNumber++)
            {
                var matchups = new List<Matchup>();

                var teamIdx = roundNumber % numberOfTeams;

                var matchup = new Matchup(teamsTemp[teamIdx], teams[0]);
                matchups.Add(matchup);

                for (var index = 1; index < numberOfMatchesInARound; index++)
                {
                    var firstTeamIndex = (roundNumber + index) % numberOfTeams;
                    var secondTeamIndex = (roundNumber + numberOfTeams - index) % numberOfTeams;

                    var matchupInner = new Matchup(teamsTemp[firstTeamIndex], teamsTemp[secondTeamIndex]);
                    matchups.Add(matchupInner);
                }

                var round = GameDay.Create(matchups);
                gameDays.Add(round);
            }

            return gameDays;
        }
    }
}