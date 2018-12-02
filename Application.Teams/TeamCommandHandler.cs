using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Teams;
using Microwave.Application;
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

        public async Task<Guid> CreateTeam(CreateTeamComand createTeamComand)
        {
            var readModelResult = await _qeryRepository.Load<RaceConfig>(createTeamComand.RaceId);
            var race = readModelResult.Value;
            var domainResult = Team.Create(race.Id, createTeamComand.TeamName, createTeamComand.TrainerName);
            await _eventStore.AppendAsync(domainResult.DomainEvents, -1);
            return domainResult.DomainEvents.First().EntityId;
        }

        public async Task BuyPlayer(BuyPlayerComand buyPlayerComand)
        {
            var teamResult = await _eventStore.LoadAsync<Team>(buyPlayerComand.TeamId);
            var team = teamResult.Entity;
            var buyPlayer = team.BuyPlayer(buyPlayerComand.PlayerTypeId);
            await _eventStore.AppendAsync(buyPlayer.DomainEvents, buyPlayerComand.TeamVersion);
        }
    }

    public class BuyPlayerComand
    {
        public Guid TeamId { get; set; }
        public Guid PlayerTypeId { get; set; }
        public long TeamVersion { get; set; }
    }

    public class CreateTeamComand
    {
        public string TrainerName { get; set; }
        public string TeamName { get; set; }
        public Guid RaceId { get; set; }
    }
}