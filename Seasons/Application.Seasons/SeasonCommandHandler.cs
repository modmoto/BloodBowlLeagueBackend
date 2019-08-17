using System.Linq;
using System.Threading.Tasks;
using Domain.Seasons;
using Domain.Seasons.TeamReadModels;
using Microwave.Domain.Identities;
using Microwave.EventStores;
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
            var domainEvents = domainResult.DomainEvents.ToList();
            var result = await _eventStore.AppendAsync(domainEvents, eventStoreResult.Version);
            result.Check();
        }

        public async Task AddTeamToSeason(AddTeamToSeasonCommand command)
        {
            var seasonResult = await _eventStore.LoadAsync<Season>(command.SeasonId);
            var season = seasonResult.Value;
            var team = (await _readModelRepository.LoadAsync<TeamReadModel>(command.TeamId)).Value;
            var domainResult = season.AddTeam(team);
            (await _eventStore.AppendAsync(domainResult.DomainEvents, seasonResult.Version)).Check();
        }

        public async Task<Identity> CreateSeason(CreateSeasonCommand command)
        {
            var domainResult = Season.Create(command.SeasonName);
            await _eventStore.AppendAsync(domainResult.DomainEvents, 0);
            return domainResult.DomainEvents.Single().EntityId;
        }

        public async Task<Season> GetSeason(GetSeasonCommand command)
        {
            var seasonResult = await _eventStore.LoadAsync<Season>(command.SeasonId);
            var season = seasonResult.Value;
            return season;
        }
    }

    public class CreateSeasonCommand
    {
        public string SeasonName { get; set; }
    }

    public class GetSeasonCommand
    {
        public GuidIdentity SeasonId { get; }

        public GetSeasonCommand(GuidIdentity seasonId)
        {
            SeasonId = seasonId;
        }
    }

    public class AddTeamToSeasonCommand
    {
        public GuidIdentity SeasonId { get; set; }
        public GuidIdentity TeamId { get; set; }
    }

    public class StartSeasonCommand
    {
        public StartSeasonCommand(GuidIdentity seasonId)
        {
            SeasonId = seasonId;
        }

        public GuidIdentity SeasonId { get; }
    }
}