using System.Threading.Tasks;
using Domain.Players;
using Microwave.Domain.Identities;
using Microwave.EventStores;

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
            var player = (await _eventStore.LoadAsync<Player>(playerId)).Value;
            var result = player.ChooseSkill(Skill.Create(levelUpCommand.SkillId));

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