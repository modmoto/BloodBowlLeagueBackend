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
                    var homeTeam = await _readModelRepository.Load<TeamReadModel>(matchup.TeamAtHome);
                    var domainEvents = Matchup.Create(
                            matchup.MatchId,
                            homeTeam.Value,
                            guestTeam.Value)
                        .DomainEvents;
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
}