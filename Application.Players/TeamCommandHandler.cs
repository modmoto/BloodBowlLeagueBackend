using System.Threading.Tasks;
using Domain.Players;
using Microwave.Domain;
using Microwave.EventStores.Ports;

namespace Application.Players
{
    public class PlayerCommandHandler
    {
        private readonly IEventStore _eventStore;

        public PlayerCommandHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task LevelUp(GuidIdentity playerId, LevelUpPlayerComand createTeamCommand)
        {
            var result = await _eventStore.LoadAsync<Player>(playerId);
            var valueEntity = result.Value.Entity;
        }
    }

    public class LevelUpPlayerComand
    {
    }
}