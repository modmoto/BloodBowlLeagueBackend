using System.Collections.Generic;
using Microwave.Domain;

namespace Application.Players.Events
{
    public class PlayerConfigCreated : IDomainEvent
    {
        public Identity EntityId { get; }
        public IEnumerable<StringIdentity> StartingSkills { get; }
        public IEnumerable<SkillType> SkillsOnDefault { get; }
        public IEnumerable<SkillType> SkillsOnDouble { get; }

        public PlayerConfigCreated(
            StringIdentity entityId,
            IEnumerable<StringIdentity> startingSkills,
            IEnumerable<SkillType> skillsOnDefault,
            IEnumerable<SkillType> skillsOnDouble)
        {
            EntityId = entityId;
            StartingSkills = startingSkills;
            SkillsOnDefault = skillsOnDefault;
            SkillsOnDouble = skillsOnDouble;
        }
    }
}