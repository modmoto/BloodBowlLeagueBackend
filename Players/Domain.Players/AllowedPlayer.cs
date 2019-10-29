using System.Collections.Generic;

namespace Domain.Players
{
    public class AllowedPlayer
    {
        public AllowedPlayer(
            string playerTypeId,
            IEnumerable<SkillReadModel> startingSkills,
            IEnumerable<SkillType> skillsOnDefault,
            IEnumerable<SkillType> skillsOnDouble)
        {
            PlayerTypeId = playerTypeId;
            StartingSkills = startingSkills;
            SkillsOnDefault = skillsOnDefault;
            SkillsOnDouble = skillsOnDouble;
        }

        public string PlayerTypeId { get; }
        public IEnumerable<SkillReadModel> StartingSkills { get; }
        public IEnumerable<SkillType> SkillsOnDefault { get; }
        public IEnumerable<SkillType> SkillsOnDouble { get; }
    }
}