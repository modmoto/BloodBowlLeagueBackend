using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Players.Events;
using Domain.Players;
using Microwave.Domain;
using Microwave.EventStores.Ports;
using Microwave.Queries;

namespace Application.Players
{
    public class OnePlayerBoughtCreatePlayer : IHandleAsync<PlayerBought>
    {
        private readonly IEventStore _eventStore;

        public OnePlayerBoughtCreatePlayer(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task HandleAsync(PlayerBought domainEvent)
        {
            var loadAsync = await _eventStore.LoadAsync<PlayerConfig>(domainEvent.PlayerTypeId);
            var result = Player.Create((GuidIdentity) domainEvent.EntityId, domainEvent.PlayerTypeId);
            var storeResult = await _eventStore.AppendAsync(result.DomainEvents, 0);
            storeResult.Check();
        }
    }

    public class PlayerConfig : Entity
    {
        public void Apply(PlayerConfigCreated configCreated)
        {
            CurrentSkills = configCreated.StartingSkills;
            SkillsOnDefault = configCreated.SkillsOnDefault;
            SkillsOnDouble = configCreated.SkillsOnDouble;

        }

        public IEnumerable<StringIdentity> CurrentSkills { get; set; }

        public IEnumerable<SkillType> SkillsOnDefault { get; set; }

        public IEnumerable<SkillType> SkillsOnDouble { get; set; }
    }
}