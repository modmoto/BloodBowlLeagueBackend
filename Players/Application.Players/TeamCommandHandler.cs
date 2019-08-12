using System.Threading.Tasks;
using Domain.Players;
using Microwave.Domain.Identities;
using Microwave.EventStores;
using Microwave.Queries;

namespace Application.Players
{
    public class PlayerCommandHandler
    {
        private readonly IEventStore _eventStore;
        private readonly IReadModelRepository _readModelRepository;

        public PlayerCommandHandler(IEventStore eventStore, IReadModelRepository readModelRepository)
        {
            _eventStore = eventStore;
            _readModelRepository = readModelRepository;
        }

        public async Task LevelUp(GuidIdentity playerId, LevelUpPlayerComand levelUpCommand)
        {
            var player = (await _eventStore.LoadAsync<Player>(playerId)).Value;
            var skillResult = await _readModelRepository.LoadAsync<SkillReadModel>(levelUpCommand.SkillId);
            var result = player.ChooseSkill(skillResult.Value);

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