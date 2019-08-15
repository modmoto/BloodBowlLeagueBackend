using System.Collections.Generic;
using Microwave.Domain.Identities;
using Teams.ReadHost.Races;

namespace Teams.ReadHost.Players
{
    public class PlayerConfig
    {
        public PlayerConfig(
            StringIdentity playerTypeId,
            IEnumerable<SkillType> skillsOnDefault,
            IEnumerable<SkillType> skillsOnDouble)
        {
            SkillsOnDefault = skillsOnDefault;
            SkillsOnDouble = skillsOnDouble;
            PlayerTypeId = playerTypeId;
        }

        public StringIdentity PlayerTypeId { get; }
        public IEnumerable<SkillType> SkillsOnDefault { get; }
        public IEnumerable<SkillType> SkillsOnDouble { get; }
    }
}