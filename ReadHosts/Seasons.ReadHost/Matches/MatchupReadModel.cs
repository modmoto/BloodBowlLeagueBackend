﻿using System.Collections.Generic;
using System.Linq;
using Microwave.Domain.Identities;
using Microwave.Queries;
using Seasons.ReadHost.Matches.Events;

namespace Seasons.ReadHost.Matches
{
    public class MatchupReadModel : ReadModel<MatchCreated>,
        IHandle<MatchFinished>,
        IHandle<MatchStarted>,
        IHandle<MatchProgressed>
    {
        public GuidIdentity MatchId { get; private set; }
        public IEnumerable<GuidIdentity> HomeTeamPlayers { get; private set; }
        public IEnumerable<GuidIdentity> GuestTeamPlayers { get; private set; }
        public IEnumerable<PlayerProgression> PlayerProgressions { get; private set; } = new List<PlayerProgression>();
        public GuidIdentity TeamAsGuest { get; private set; }
        public GuidIdentity TeamAtHome { get; private set; }
        public bool IsFinished { get; private set; }
        public bool IsStarted { get; private set; }
        public GameResult GameResult { get; set; }

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
        }

        public void Handle(MatchProgressed domainEvent)
        {
            PlayerProgressions = PlayerProgressions.Append(domainEvent.PlayerProgression);
        }
    }
}