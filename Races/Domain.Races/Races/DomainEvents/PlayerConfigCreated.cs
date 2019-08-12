using System.Collections.Generic;
using Domain.Races.Skills;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;

namespace Domain.Races.Races.DomainEvents
{
    public class PlayerConfigCreated : IDomainEvent
    {
        public Identity EntityId => PlayerConfigId;
        public StringIdentity PlayerConfigId { get; }
        public IEnumerable<Skill> StartingSkills { get; }
        public IEnumerable<SkillType> SkillsOnDefault { get; }
        public IEnumerable<SkillType> SkillsOnDouble { get; }

        public PlayerConfigCreated(
            StringIdentity playerConfigId,
            IEnumerable<Skill> startingSkills,
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