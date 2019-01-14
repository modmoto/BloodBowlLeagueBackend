using System.Collections.Generic;
using Application.Players.Skills;
using Microwave.Domain;

namespace Application.Players.Events
{
    internal class PlayerConfigCreated : IDomainEvent
    {
        public Identity EntityId { get; }
        public List<Skill> StartingSkills { get; }
        public List<SkillType> SkillsOnDefault { get; }
        public List<SkillType> SkillsOnDouble { get; }

        public PlayerConfigCreated(StringIdentity entityId, List<Skill> startingSkills, List<SkillType> skillsOnDefault, List<SkillType> skillsOnDouble)
        {
            EntityId = entityId;
            StartingSkills = startingSkills;
            SkillsOnDefault = skillsOnDefault;
            SkillsOnDouble = skillsOnDouble;
        }
    }
}