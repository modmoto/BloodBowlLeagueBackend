using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Players;
using Domain.Players.Events.ForeignEvents;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;
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
            var resultList = new Dictionary<GuidIdentity, Tuple<List<IDomainEvent>, long>>();
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

                if (resultList.TryGetValue(player.PlayerId, out var playerTuple))
                {
                    var events = playerTuple.Item1;
                    events.AddRange(domainEvents);
                    resultList[player.PlayerId] = new Tuple<List<IDomainEvent>, long>(events, result.Version);
                }
                else
                {
                    resultList[player.PlayerId] = new Tuple<List<IDomainEvent>, long>(domainEvents.ToList(), result.Version);
                }
            }

            foreach (var playerResult in resultList)
            {
                var result = await _eventStore.AppendAsync(playerResult.Value.Item1, playerResult.Value.Item2);
                result.Check();
            }
        }
    }
}