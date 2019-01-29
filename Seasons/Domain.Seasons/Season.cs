using System.Collections.Generic;
using System.Linq;
using Domain.Seasons.Events;
using Microwave.Domain;

namespace Domain.Seasons
{
    public class Season : IApply<TeamAddedToSeason>, IApply<SeasonCreated>, IApply<SeasonStarted>
    {
        public GuidIdentity SeasonId { get; private set; }
        public IEnumerable<GuidIdentity> Teams { get; private set; } = new List<GuidIdentity>();
        public IEnumerable<Matchup> Matchups { get; private set; }
        public bool SeasonIsStarted { get; private set; }

        public static DomainResult Create()
        {
            return DomainResult.Ok(new SeasonCreated(GuidIdentity.Create()));
        }

        public DomainResult AddTeam(GuidIdentity teamId)
        {
            if (SeasonIsStarted) return DomainResult.Error(new SeasonAllreadyStarted());
            return DomainResult.Ok(new TeamAddedToSeason(SeasonId, teamId));
        }

        public DomainResult StartSeason()
        {
            if (TeamCountIsUneven()) return DomainResult.Error(new CanNotStartSeasonWithUnevenTeamCount(Teams.Count()));

            var teams = Teams.ToList();
            var gamesCount = teams.Count - 1;
            var gamesPerDay = teams.Count / 2;
            var matchupMatrix = new MatchupMatrix(teams.Count);
            var gameDays = new List<GameDay>();

            var matchupsOnADay = new List<Matchup>();
            while (gameDays.Count != gamesCount) { 
                for (var index = 0; index < matchupMatrix.Count; index++)
                {
                    for (var i = 0; i < matchupMatrix[index].Count; i++)
                    {
                        if (matchupMatrix[index][i] == MatchupState.IsFree)
                        {
                            matchupMatrix.MarkAsDone(index, i);
                            matchupsOnADay.Add(new Matchup(teams[index], teams[i]));

                            if (matchupsOnADay.Count == gamesPerDay)
                            {
                                gameDays.Add(new GameDay(matchupsOnADay));
                                matchupsOnADay = new List<Matchup>();
                                matchupMatrix.ResetTodayBlock();
                            }
                        }
                    }
                }
            }

            return DomainResult.Ok(new SeasonStarted(SeasonId, gameDays));
        }

        private bool TeamCountIsUneven()
        {
            return Teams.Count() % 2 != 0;
        }

        public void Apply(TeamAddedToSeason domainEvent)
        {
            Teams = Teams.Append(domainEvent.TeamId);
        }

        public void Apply(SeasonCreated domainEvent)
        {
            SeasonId = (GuidIdentity) domainEvent.EntityId;
        }

        public void Apply(SeasonStarted domainEvent)
        {
            SeasonIsStarted = true;
        }
    }


    public enum MatchupState
    {
        IsDone, IsFree, IsMatchUpForThisDay
    }

    public class Matchup
    {
        public Matchup(GuidIdentity homeTeam, GuidIdentity guestTeam)
        {
            HomeTeam = homeTeam;
            GuestTeam = guestTeam;
        }

        public GuidIdentity HomeTeam { get; }
        public GuidIdentity GuestTeam { get; }

        public override string ToString()
        {
            return $"{HomeTeam.Id} vs {GuestTeam.Id}";
        }
    }

    public class GameDay
    {
        public IEnumerable<Matchup> Matchups { get; }

        public GameDay(IEnumerable<Matchup> matchups)
        {
            Matchups = matchups;
        }
    }
}
