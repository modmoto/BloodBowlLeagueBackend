using System.Collections.Generic;
using Domain.Races.Skills;
using Microwave.Domain.Identities;

namespace Domain.Races.Races
{
    public class AllowedPlayer
    {
        private AllowedPlayer(
            StringIdentity playerTypeId,
            int maximumPlayers,
            GoldCoins cost,
            IEnumerable<Skill> startingSkills,
            IEnumerable<SkillType> skillsOnDefault,
            IEnumerable<SkillType> skillsOnDouble)
        {
            PlayerTypeId = playerTypeId;
            MaximumPlayers = maximumPlayers;
            Cost = cost;
            StartingSkills = startingSkills;
            SkillsOnDefault = skillsOnDefault;
            SkillsOnDouble = skillsOnDouble;
        }

        public StringIdentity PlayerTypeId { get; }
        public int MaximumPlayers { get; }
        public GoldCoins Cost { get; }
        public IEnumerable<Skill> StartingSkills { get; }
        public IEnumerable<SkillType> SkillsOnDefault { get; }
        public IEnumerable<SkillType> SkillsOnDouble { get; }

        public static AllowedPlayer DarkElveLineMan =>
            new AllowedPlayer(
                StringIdentity.Create("DE_LineMan"),
                16,
                new GoldCoins(70000),
                new List<Skill>(),
                new [] { SkillType.General },
                new [] { SkillType.General });

        public static AllowedPlayer DarkElveAssasine =>
            new AllowedPlayer(
                StringIdentity.Create("DE_Assassine"),
                2,
                new GoldCoins(90000),
                new [] { Skill.Create(Skill.Dodge) },
                new [] { SkillType.General },
                new [] { SkillType.General });

        public static AllowedPlayer DarkElveBlitzer =>
            new AllowedPlayer(
                StringIdentity.Create("DE_Blitzer"),
                4,
                new GoldCoins(100000),
                new [] { Skill.Create(Skill.Block) },
                new [] { SkillType.General },
                new [] { SkillType.General });

        public static AllowedPlayer DarkElveWitchElve =>
            new AllowedPlayer(
                StringIdentity.Create("DE_WitchElve"),
                2,
                new GoldCoins(110000),
                new [] { Skill.Create(Skill.Dodge) },
                new [] { SkillType.General },
                new [] { SkillType.General });
    }
}