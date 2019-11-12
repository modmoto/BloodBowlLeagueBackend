using System.Linq;
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
        private readonly IReadModelRepository _readModelRepository;

        public OnPlayerBoughtCreatePlayer(
            IEventStore eventStore, 
            IReadModelRepository readModelRepository)
        {
            _eventStore = eventStore;
            _readModelRepository = readModelRepository;
        }

        public async Task HandleAsync(PlayerBought domainEvent)
        {
            var readModel = await _readModelRepository.LoadAllAsync<RaceReadModel>();
            var races = readModel.Value.ToList();
            var race = races.SingleOrDefault(r =>
                r.AllowedPlayers.Any(a => a.PlayerTypeId == domainEvent.PlayerTypeId));
            var playerRule = race.AllowedPlayers.Single(a => a.PlayerTypeId == domainEvent.PlayerTypeId);
            var result = Player.Create(
                domainEvent.PlayerId,
                domainEvent.TeamId,
                domainEvent.PlayerPositionNumber,
                playerRule);
            var storeResult = await _eventStore.AppendAsync(result.DomainEvents, 0);
            storeResult.Check();
        }
    }
}