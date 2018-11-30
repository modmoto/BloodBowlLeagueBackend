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
            var eventStoreResult = await _qeryRepository.Load<RaceConfig>(createTeamComand.RaceId);
            var race = eventStoreResult.Value;
            var domainResult = Team.Create(race.Id, createTeamComand.TeamName, createTeamComand.TrainerName);
            await _eventStore.AppendAsync(domainResult.DomainEvents, -1);
            return domainResult.DomainEvents.First().EntityId;
        }

        public async Task BuyPlayer(BuyPlayerComand buyPlayerComand)
        {
            var teamResult = await _eventStore.LoadAsync<Team>(buyPlayerComand.TeamId);
            var team = teamResult.Value;
            var buyPlayer = team.BuyPlayer(buyPlayerComand.PlayerTypeId);
            await _eventStore.AppendAsync(buyPlayer.DomainEvents, teamResult.Version);
        }
    }

    public class BuyPlayerComand
    {
        public Guid TeamId { get; set; }
        public Guid PlayerTypeId { get; set; }
    }

    public class CreateTeamComand
    {
        public CreateTeamComand(string trainerName, string teamName, Guid raceId)
        {
            TrainerName = trainerName;
            TeamName = teamName;
            RaceId = raceId;
        }

        public string TrainerName { get; }
        public string TeamName { get;  }
        public Guid RaceId { get; }
    }
}