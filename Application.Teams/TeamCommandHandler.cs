using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Teams;
using Microwave.Application.Ports;
using Microwave.Queries;
using Querries.Teams;

namespace Application.Teams
{
    public class TeamCommandHandler
    {
        private readonly IEventStore _eventStore;
        private readonly IQeryRepository _qeryRepository;

        public TeamCommandHandler(IEventStore eventStore, IQeryRepository qeryRepository)
        {
            _eventStore = eventStore;
            _qeryRepository = qeryRepository;
        }

        public async Task<Guid> CreateTeam(CreateTeamCommand createTeamCommand)
        {
            var readModelResult = await _qeryRepository.Load<RaceConfig>(createTeamCommand.RaceId);
            var race = readModelResult.Value;
            var domainResult = Team.Create(race.Id, createTeamCommand.TeamName, createTeamCommand.TrainerName, race.ReadModel.AllowedPlayers);
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