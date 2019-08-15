using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Players;
using Domain.Players.Events.ForeignEvents;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Validation;
using Microwave.EventStores;
using Microwave.Queries;

namespace Application.Players
{
    public class OnMatchFinishedUpdatePlayerProgress : IHandleAsync<MatchFinished>
    {
        private readonly IEventStore _eventStore;

        public OnMatchFinishedUpdatePlayerProgress(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task HandleAsync(MatchFinished domainEvent)
        {
            var resultList = new List<Tuple<EventStoreResult<Player>, IEnumerable<IDomainEvent>>>();
            foreach (var playerProgression in domainEvent.PlayerProgressions)
            {
                var domainResults = new List<DomainResult>();
                var result = await _eventStore.LoadAsync<Player>(playerProgression.PlayerId);

                var player = result.Value;

                switch (playerProgression.ProgressionEvent)
                {
                    case ProgressionEvent.PlayerPassed:
                        domainResults.Add(player.Pass());
                        break;
                    case ProgressionEvent.PlayerMadeCasualty:
                        domainResults.Add(player.Block());
                        break;
                    case ProgressionEvent.PlayerMadeTouchdown:
                        domainResults.Add(player.Move());
                        break;
                    case ProgressionEvent.NominatedMostValuablePlayer:
                        domainResults.Add(player.NominateForMostValuablePlayer());
                        break;
                }

                var domainEvents = domainResults.SelectMany(res => res.DomainEvents);

                resultList.Add(new Tuple<EventStoreResult<Player>, IEnumerable<IDomainEvent>>(result, domainEvents));
            }

            foreach (var tuple in resultList)
            {
                var eventStoreResult = tuple.Item1;
                var domainEvents = tuple.Item2;
                await _eventStore.AppendAsync(domainEvents, eventStoreResult.Version);
            }
        }
    }
}