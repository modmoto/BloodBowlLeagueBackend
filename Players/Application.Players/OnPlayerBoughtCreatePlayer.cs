using System.Threading.Tasks;
using Domain.Players;
using Domain.Players.Events.ForeignEvents;
using Microwave.EventStores;
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
            var eventResult = await _eventStore.LoadAsync<PlayerConfig>(domainEvent.PlayerTypeId);
            var playerConfig = eventResult.Value;
            var result = Player.Create(
                domainEvent.PlayerId,
                domainEvent.TeamId,
                domainEvent.PlayerTypeId,
                playerConfig);
            var storeResult = await _eventStore.AppendAsync(result.DomainEvents, 0);
            storeResult.Check();
        }
    }
}