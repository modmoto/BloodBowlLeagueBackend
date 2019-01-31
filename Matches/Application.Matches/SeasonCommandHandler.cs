using System.Threading.Tasks;
using Domain.Matches.Matches;
using Domain.Matches.Seasons;
using Microwave.Domain;
using Microwave.EventStores.Ports;
using Microwave.Queries;

namespace Application.Matches
{
    public class SeasonCommandHandler
    {
        private readonly IEventStore _eventStore;
        private readonly IReadModelRepository _readModelRepository;

        public SeasonCommandHandler(IEventStore eventStore, IReadModelRepository readModelRepository)
        {
            _eventStore = eventStore;
            _readModelRepository = readModelRepository;
        }

        public async Task StartSeason(StartSeasonCommand command)
        {
            var eventStoreResult = await _eventStore.LoadAsync<Season>(command.SeasonId);
            var season = eventStoreResult.Value;
            var domainResult = season.StartSeason();
            domainResult.EnsureSucces();
            var storeResult = await _eventStore.AppendAsync(domainResult.DomainEvents, eventStoreResult.Version);
            storeResult.Check();
        }

        public async Task AddTeamToSeason(AddTeamToSeasonCommand command)
        {
            var seasonResult = await _eventStore.LoadAsync<Season>(command.SeasonId);
            var season = seasonResult.Value;
            var team = (await _readModelRepository.Load<TeamReadModel>(command.TeamId)).Value;
            var domainResult = season.AddTeam(team.TeamId);
            (await _eventStore.AppendAsync(domainResult.DomainEvents, seasonResult.Version)).Check();
        }
    }

    public class AddTeamToSeasonCommand
    {
        public GuidIdentity SeasonId { get; set; }
        public Identity TeamId { get; set; }
    }

    public class StartSeasonCommand
    {
        public GuidIdentity SeasonId { get; set; }
    }
}