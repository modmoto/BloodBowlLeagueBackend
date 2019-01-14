using System.Threading.Tasks;
using Application.Players.Events;
using Domain.Players;
using Microwave.Domain;
using Microwave.EventStores.Ports;
using Microwave.Queries;

namespace Application.Players
{
    public class OnPlayerBoughtCreatePlayer : IHandleAsync<PlayerBought>
    {
        private readonly IEventStore _eventStore;

        public OnPlayerBoughtCreatePlayer(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }
        public async Task HandleAsync(PlayerBought domainEvent)
        {
            var result = Player.Create((GuidIdentity) domainEvent.EntityId, domainEvent.PlayerTypeId);
            var storeResult = await _eventStore.AppendAsync(result.DomainEvents, 0);
            storeResult.Check();
        }
    }
}