using System.Collections.Generic;
using System.Linq;
using Domain.Matches.Matches;
using Domain.Matches.Matches.Events;
using Domain.Matches.Seasons.Events;
using Microwave.Domain;

namespace Domain.Matches.Seasons
{
    public class MatchPairingService
    {
        public IEnumerable<IDomainEvent> ComputePairings(GuidIdentity seasonId, IEnumerable<GuidIdentity> listTeam)
        {
            var teams = listTeam.ToList();
            var numberOfRounds = teams.Count - 1;
            var numberOfMatchesInARound = teams.Count / 2;

            var teamsTemp = new List<GuidIdentity>();

            teamsTemp.AddRange(teams.Skip(numberOfMatchesInARound).Take(numberOfMatchesInARound));
            teamsTemp.AddRange(teams.Skip(1).Take(numberOfMatchesInARound - 1).ToArray().Reverse());

            var numberOfTeams = teamsTemp.Count;

            var domainEvents = new List<IDomainEvent>();

            for (var roundNumber = 0; roundNumber < numberOfRounds; roundNumber++)
            {
                var matchups = new List<Matchup>();

                var teamIdx = roundNumber % numberOfTeams;

                var domainResult = Matchup.Create(teamsTemp[teamIdx], teams[0]);
                domainEvents.AddRange(domainResult.DomainEvents);
                var matchup = new Matchup();
                matchup.Apply((MatchCreated) domainResult.DomainEvents.Single());
                matchups.Add(matchup);

                for (var idx = 1; idx < numberOfMatchesInARound; idx++)
                {
                    var firstTeamIndex = (roundNumber + idx) % numberOfTeams;
                    var secondTeamIndex = (roundNumber + numberOfTeams - idx) % numberOfTeams;

                    var domainResultInner = Matchup.Create(teamsTemp[firstTeamIndex], teamsTemp[secondTeamIndex]);
                    domainEvents.AddRange(domainResultInner.DomainEvents);
                    var matchupInner = new Matchup();
                    matchupInner.Apply((MatchCreated) domainResultInner.DomainEvents.Single());
                    matchups.Add(matchupInner);
                }

                var round = GameDay.Create(seasonId, matchups);
                domainEvents.AddRange(round.DomainEvents);
            }

            return domainEvents;
        }
    }
}