using System.Collections.Generic;

namespace Domain.Players
{
    public class AllowedPlayer
    {
        private AllowedPlayer(
            string playerTypeId,
            int maximumPlayers,
            IEnumerable<SkillReadModel> startingSkills,
            PlayerStats playerStats,
            IEnumerable<SkillType> skillsOnDefault,
            IEnumerable<SkillType> skillsOnDouble)
        {
            PlayerTypeId = playerTypeId;
            MaximumPlayers = maximumPlayers;
            StartingSkills = startingSkills;
            PlayerStats = playerStats;
            SkillsOnDefault = skillsOnDefault;
            SkillsOnDouble = skillsOnDouble;
        }

        public string PlayerTypeId { get; }
        public int MaximumPlayers { get; }
        public IEnumerable<SkillReadModel> StartingSkills { get; }
        public PlayerStats PlayerStats { get; }
        public IEnumerable<SkillType> SkillsOnDefault { get; }
        public IEnumerable<SkillType> SkillsOnDouble { get; }
    }
}