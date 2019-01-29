using System.Collections;
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

            var matchupMatrix = new MatchupMatrix(Teams.Count());
            foreach (var outerMatchup in matchupMatrix)
            {
                foreach (var matchUp in outerMatchup)
                {
                    
                }
            }


            var matchups = Teams.SelectMany((fst, i) => Teams.Skip(i + 1).Select(snd => new Matchup(fst, snd))).ToList();

            var gameDays = new List<GameDay>();
            var gameDaysAmmount = Teams.Count() - 1;
            var matchesPerDayAmmount = Teams.Count() - 1;
            for (var i = 0; i < gameDaysAmmount; i++)
            {
                var matchupsOnThisDay = new List<Matchup>();
                var matchupCount = 0;
                var matchupsRemoved = new List<Matchup>();
                foreach (var matchup in matchups)
                {
                    if (PlayerIsAllreadyPlayingOnThisDay(matchup, matchupsOnThisDay)) continue;
                    matchupCount++;
                    matchupsOnThisDay.Add(matchup);
                    matchupsRemoved.Add(matchup);
                    if (matchupCount == matchesPerDayAmmount) break;
                }

                foreach (var matchup in matchupsRemoved)
                {
                    matchups.Remove(matchup);
                }

                var gameDay = new GameDay(matchupsOnThisDay);
                gameDays.Add(gameDay);
            }
            
            return DomainResult.Ok(new SeasonStarted(SeasonId, gameDays));
        }

        private bool PlayerIsAllreadyPlayingOnThisDay(Matchup matchup, IEnumerable<Matchup> matchupsOnDay)
        {
            return matchupsOnDay.Any(m =>    m.HomeTeam == matchup.HomeTeam
                                          || m.HomeTeam == matchup.GuestTeam
                                          || m.GuestTeam == matchup.HomeTeam
                                          || m.GuestTeam == matchup.GuestTeam);
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
