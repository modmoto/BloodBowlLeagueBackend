﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Teams;
using Microwave.EventStores;

namespace Application.Teams
{
    public class TeamCommandHandler
    {
        private readonly IEventStore _eventStore;

        public TeamCommandHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Guid> CreateTeam(CreateTeamCommand createTeamCommand)
        {
            var readModelResult = await _eventStore.LoadAsync<RaceConfig>(createTeamCommand.RaceId);
            var race = readModelResult.Entity;
            var domainResult = Team.Create(race.Id, createTeamCommand.TeamName, createTeamCommand.TrainerName, race.AllowedPlayers);
            await _eventStore.AppendAsync(domainResult.DomainEvents, 0);
            return domainResult.DomainEvents.First().EntityId;
        }

        public async Task BuyPlayer(BuyPlayerCommand buyPlayerCommand)
        {
            var teamResult = await _eventStore.LoadAsync<Team>(buyPlayerCommand.TeamId);
            var team = teamResult.Entity;
            var buyPlayer = team.BuyPlayer(buyPlayerCommand.PlayerTypeId);
            await _eventStore.AppendAsync(buyPlayer.DomainEvents, buyPlayerCommand.TeamVersion);
        }
    }

    public class BuyPlayerCommand
    {
        public Guid TeamId { get; set; }
        public Guid PlayerTypeId { get; set; }
        public long TeamVersion { get; set; }
    }

    public class CreateTeamCommand
    {
        public string TrainerName { get; set; }
        public string TeamName { get; set; }
        public Guid RaceId { get; set; }
    }
}