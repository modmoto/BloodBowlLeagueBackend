using System.Collections.Generic;
using Microwave.Domain;

namespace Domain.Players.Events.PlayerConfigs
{
    public class PlayerConfigCreated : IDomainEvent
    {
        public Identity EntityId => PlayerConfigId;
        public StringIdentity PlayerConfigId { get; }
        public IEnumerable<StringIdentity> StartingSkills { get; }
        public IEnumerable<SkillType> SkillsOnDefault { get; }
        public IEnumerable<SkillType> SkillsOnDouble { get; }

        public PlayerConfigCreated(
            StringIdentity playerConfigId,
            IEnumerable<StringIdentity> startingSkills,
            IEnumerable<SkillType> skillsOnDefault,
            IEnumerable<SkillType> skillsOnDouble)
        {
            PlayerConfigId = playerConfigId;
            StartingSkills = startingSkills;
            SkillsOnDefault = skillsOnDefault;
            SkillsOnDouble = skillsOnDouble;
        }
    }
}