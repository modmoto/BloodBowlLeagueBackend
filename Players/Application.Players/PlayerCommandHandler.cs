using System;
using System.Threading.Tasks;
using Domain.Players;
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

        public async Task ChooseSkill(Guid playerId, LevelUpPlayerComand levelUpCommand)
        {
            var eventStoreResult = await _eventStore.LoadAsync<Player>(playerId);
            var player = eventStoreResult.Value;
            var skillResult = await _readModelRepository.LoadAsync<SkillReadModel>(levelUpCommand.SkillId);
            var result = player.ChooseSkill(skillResult.Value);

            (await _eventStore.AppendAsync(result.DomainEvents, eventStoreResult.Version)).Check();
        }

        public async Task RegisterLevelUpSkillPointRoll(Guid playerId, RegisterLevelUpSkillPointRollCommand levelUpCommand)
        {
            var eventStoreResult = await _eventStore.LoadAsync<Player>(playerId);
            var player = eventStoreResult.Value;
            var result = player.RegisterLevelUpSkillPointRoll(levelUpCommand.FreeSkillPoint);

            (await _eventStore.AppendAsync(result.DomainEvents, eventStoreResult.Version)).Check();
        }
    }

    public class LevelUpPlayerComand
    {
        public LevelUpPlayerComand(string skillId)
        {
            SkillId = skillId;
        }

        public string SkillId { get; }
    }

    public class RegisterLevelUpSkillPointRollCommand
    {
        public RegisterLevelUpSkillPointRollCommand(FreeSkillPoint freeSkillPoint)
        {
            FreeSkillPoint = freeSkillPoint;
        }

        public FreeSkillPoint FreeSkillPoint { get; }
    }
}