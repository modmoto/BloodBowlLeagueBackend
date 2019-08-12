using System.Collections.Generic;
using Microwave.Domain.Identities;

namespace Domain.Players
{
    public class AllowedPlayer
    {
        public AllowedPlayer(
            StringIdentity playerTypeId,
            IEnumerable<Skill> startingSkills,
            IEnumerable<SkillType> skillsOnDefault,
            IEnumerable<SkillType> skillsOnDouble)
        {
            PlayerTypeId = playerTypeId;
            StartingSkills = startingSkills;
            SkillsOnDefault = skillsOnDefault;
            SkillsOnDouble = skillsOnDouble;
        }

        public StringIdentity PlayerTypeId { get; }
        public IEnumerable<Skill> StartingSkills { get; }
        public IEnumerable<SkillType> SkillsOnDefault { get; }
        public IEnumerable<SkillType> SkillsOnDouble { get; }
    }
}