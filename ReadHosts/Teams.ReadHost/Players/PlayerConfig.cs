using System.Collections.Generic;
using Teams.ReadHost.Races;

namespace Teams.ReadHost.Players
{
    public class PlayerConfig
    {
        public PlayerConfig(
            string playerTypeId,
            IEnumerable<SkillReadModel> startingSkills,
            IEnumerable<SkillType> skillsOnDefault,
            IEnumerable<SkillType> skillsOnDouble)
        {
            StartingSkills = startingSkills;
            SkillsOnDefault = skillsOnDefault;
            SkillsOnDouble = skillsOnDouble;
            PlayerTypeId = playerTypeId;
        }

        public string PlayerTypeId { get; }
        public IEnumerable<SkillReadModel> StartingSkills { get; }
        public IEnumerable<SkillType> SkillsOnDefault { get; }
        public IEnumerable<SkillType> SkillsOnDouble { get; }
    }
}