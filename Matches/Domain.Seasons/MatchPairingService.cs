using System.Collections.Generic;
using System.Linq;
using Domain.Seasons.Events;
using Microwave.Domain;

namespace Domain.Seasons
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
                var matchups = new List<MatchupReadModel>();

                var teamIdx = roundNumber % numberOfTeams;

                var matchCreated = new MatchCreated(GuidIdentity.Create(), teamsTemp[teamIdx], teams[0]);
                domainEvents.Add(matchCreated);
                var matchup = new MatchupReadModel();
                matchup.Handle(matchCreated);
                matchups.Add(matchup);

                for (var index = 1; index < numberOfMatchesInARound; index++)
                {
                    var firstTeamIndex = (roundNumber + index) % numberOfTeams;
                    var secondTeamIndex = (roundNumber + numberOfTeams - index) % numberOfTeams;

                    var matchCreatedInner = new MatchCreated(GuidIdentity.Create(), teamsTemp[firstTeamIndex], teamsTemp[secondTeamIndex]);
                    domainEvents.Add(matchCreatedInner);
                    var matchupInner = new MatchupReadModel();
                    matchupInner.Handle(matchCreatedInner);
                    matchups.Add(matchupInner);
                }

                var round = GameDay.Create(seasonId, matchups);
                domainEvents.AddRange(round.DomainEvents);
            }

            return domainEvents;
        }
    }
}