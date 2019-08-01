using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Matches;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;
using Microwave.EventStores;
using Microwave.Queries;

namespace Application.Matches
{
    public class SeasonCreatedEventHandler : IHandleAsync<SeasonStarted>
    {
        private readonly IEventStore _eventStore;
        private readonly IReadModelRepository _readModelRepository;

        public SeasonCreatedEventHandler(IEventStore eventStore, IReadModelRepository readModelRepository)
        {
            _eventStore = eventStore;
            _readModelRepository = readModelRepository;
        }

        public async Task HandleAsync(SeasonStarted domainEvent)
        {
            var matchCreatedEvents = new List<IDomainEvent>();
            foreach (var gameDay in domainEvent.GameDays)
            {
                foreach (var matchup in gameDay.Matchups)
                {
                    var guestTeam = await _readModelRepository.Load<TeamReadModel>(matchup.TeamAsGuest);
                    var homeTeam = await _readModelRepository.Load<TeamReadModel>(matchup.TeamAsGuest);
                    var domainEvents = Matchup.Create(homeTeam.Value.TeamId, guestTeam.Value.TeamId).DomainEvents;
                    matchCreatedEvents.Add(domainEvents.Single());
                }
            }

            foreach (var createdEvent in matchCreatedEvents)
            {
                var result = await _eventStore.AppendAsync(createdEvent, 0);
                result.Check();
            }
        }
    }

    public class SeasonStarted : ISubscribedDomainEvent
    {
        public SeasonStarted(GuidIdentity seasonId, IEnumerable<GameDay> gameDays)
        {
            SeasonId = seasonId;
            GameDays = gameDays;
        }

        public GuidIdentity SeasonId { get; }
        public IEnumerable<GameDay> GameDays { get; }
        public Identity EntityId => SeasonId;
    }

    public class GameDay
    {
        public IEnumerable<MatchupReadModel> Matchups { get; set; }
    }

    public class MatchupReadModel
    {
        public GuidIdentity TeamAtHome { get; set; }
        public GuidIdentity TeamAsGuest { get; set; }
    }
}