using System;
using System.Collections.Generic;
using System.Linq;
using Microwave.Domain.Results;
using Microwave.Queries;
using Seasons.ReadHost.Matches.Events;
using Seasons.ReadHost.Teams;

namespace Seasons.ReadHost.Matches
{
    public class MatchupReadModel : ReadModel<MatchCreated>,
        IHandle<MatchFinished>,
        IHandle<MatchCreated>,
        IHandle<MatchStarted>,
        IHandle<MatchProgressed>
    {
        public Guid MatchId { get; private set; }
        public IEnumerable<Guid> HomeTeamPlayers { get; private set; }
        public IEnumerable<Guid> GuestTeamPlayers { get; private set; }
        public IEnumerable<PlayerProgression> PlayerProgressions { get; private set; } = new List<PlayerProgression>();
        public Guid TeamAsGuest { get; private set; }
        public Guid TeamAtHome { get; private set; }
        public bool IsFinished { get; private set; }
        public bool IsStarted { get; private set; }
        public GameResult GameResult { get; private set; }
        public string HomeTeamName { get; private set; }
        public string GuestTeamName { get; private set; }

        public void Handle(MatchFinished domainEvent)
        {
            IsFinished = true;
            GameResult = domainEvent.GameResult;
        }

        public void Handle(MatchStarted domainEvent)
        {
            IsStarted = true;
            HomeTeamPlayers = domainEvent.HomeTeam;
            GuestTeamPlayers = domainEvent.GuestTeam;
        }

        public void Handle(MatchCreated domainEvent)
        {
            MatchId = domainEvent.MatchId;
            TeamAtHome = domainEvent.TeamAtHome;
            TeamAsGuest = domainEvent.TeamAsGuest;
            GameResult = new GameResult
            {
                HomeTeam = new PointsOfTeam(),
                GuestTeam = new PointsOfTeam()
            };
        }

        public void Handle(MatchProgressed domainEvent)
        {
            GameResult = domainEvent.GameResult;
            PlayerProgressions = PlayerProgressions.Append(domainEvent.PlayerProgression);
        }

        public void SetFullTeams(TeamReadModel guestTeam, TeamReadModel homeTeam)
        {
            GuestTeamName = guestTeam.TeamName;
            HomeTeamName = homeTeam.TeamName;
        }
    }
}