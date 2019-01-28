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

        public async Task LevelUp(GuidIdentity playerId, LevelUpPlayerComand levelUpCommand)
        {
            var skill = (await _eventStore.LoadAsync<Skill>(levelUpCommand.SkillId)).Entity;
            var player = (await _eventStore.LoadAsync<Player>(playerId)).Entity;
            var result = player.ChooseSkill(skill);

            (await _eventStore.AppendAsync(result.DomainEvents, (await _eventStore.LoadAsync<Player>(playerId)).Version)).Check();
        }
    }

    public class LevelUpPlayerComand
    {
        public LevelUpPlayerComand(StringIdentity skillId)
        {
            SkillId = skillId;
        }

        public StringIdentity SkillId { get; }
    }
}