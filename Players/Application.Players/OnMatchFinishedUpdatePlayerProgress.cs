using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Players;
using Domain.Players.Events.ForeignEvents;
using Microwave.Domain;
using Microwave.EventStores.Ports;
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

                var resultStore = await _eventStore.AppendAsync(domainEvents, result.Version);
                resultStore.Check();
            }
        }
    }
}