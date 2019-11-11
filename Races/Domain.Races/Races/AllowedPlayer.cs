using System.Collections.Generic;
using Domain.Races.Skills;

namespace Domain.Races.Races
{
    public class AllowedPlayer
    {
        private AllowedPlayer(
            string playerTypeId,
            int maximumPlayers,
            GoldCoins cost,
            PlayerStats playerStats,
            IEnumerable<Skill> startingSkills,
            IEnumerable<SkillType> skillsOnDefault,
            IEnumerable<SkillType> skillsOnDouble)
        {
            PlayerTypeId = playerTypeId;
            MaximumPlayers = maximumPlayers;
            Cost = cost;
            PlayerStats = playerStats;
            StartingSkills = startingSkills;
            SkillsOnDefault = skillsOnDefault;
            SkillsOnDouble = skillsOnDouble;
        }

        public string PlayerTypeId { get; }
        public int MaximumPlayers { get; }
        public GoldCoins Cost { get; }
        public PlayerStats PlayerStats { get; }
        public IEnumerable<Skill> StartingSkills { get; }
        public IEnumerable<SkillType> SkillsOnDefault { get; }
        public IEnumerable<SkillType> SkillsOnDouble { get; }

        // DarkElves
        public static AllowedPlayer DarkElveLineMan =>
            new AllowedPlayer(
                "DE_LineMan",
                16,
                new GoldCoins(70000),
                new PlayerStats(6, 3, 4, 8),
                new List<Skill>(),
                new [] { SkillType.General, SkillType.Agility },
                new [] { SkillType.Strength, SkillType.Passing });

        public static AllowedPlayer DarkElveAssasine =>
            new AllowedPlayer(
                "DE_Assassine",
                2,
                new GoldCoins(90000),
                new PlayerStats(7, 3, 4, 8),
                new [] { Skill.Shadowing, Skill.Stab },
                new [] { SkillType.General, SkillType.Agility },
                new [] { SkillType.Strength, SkillType.Passing });

        public static AllowedPlayer DarkElveBlitzer =>
            new AllowedPlayer(
                "DE_Blitzer",
                4,
                new GoldCoins(100000),
                new PlayerStats(7, 3, 4, 8),
                new [] { Skill.Block },
                new [] { SkillType.General, SkillType.Agility },
                new [] { SkillType.Passing, SkillType.Strength });

        public static AllowedPlayer DarkElveWitchElve =>
            new AllowedPlayer(
                "DE_WitchElve",
                2,
                new GoldCoins(110000),
                new PlayerStats(7, 3, 4, 7),
                new [] { Skill.Dodge, Skill.JumpUp, Skill.Frenzy },
                new [] { SkillType.General },
                new [] { SkillType.General });
        public static AllowedPlayer DarkElveRunner =>
            new AllowedPlayer(
                "DE_Runner",
                2,
                new GoldCoins(80000),
                new PlayerStats(7, 3, 4, 7),
                new [] { Skill.DumpOff },
                new [] { SkillType.General, SkillType.Agility, SkillType.Passing },
                new [] { SkillType.Strength });

        // Humans
        public static AllowedPlayer HumanLineMan =>
            new AllowedPlayer(
                "HU_LineMan",
                16,
                new GoldCoins(50000),
                new PlayerStats(6, 3, 3, 8),
                new List<Skill>(),
                new [] { SkillType.General },
                new [] { SkillType.Agility, SkillType.Strength, SkillType.Passing });

        public static AllowedPlayer HumanBlitzer =>
            new AllowedPlayer(
                "HU_Blitzer",
                4,
                new GoldCoins(90000),
                new PlayerStats(7, 3, 3, 8),
                new [] { Skill.Block },
                new [] { SkillType.General, SkillType.Strength },
                new [] { SkillType.Passing, SkillType.Agility });

        public static AllowedPlayer HumanCatcher =>
            new AllowedPlayer(
                "HU_Catcher",
                4,
                new GoldCoins(70000),
                new PlayerStats(7, 3, 3, 7),
                new [] { Skill.Catch, Skill.Dodge },
                new [] { SkillType.Agility, SkillType.General },
                new [] { SkillType.Strength, SkillType.Passing });

        public static AllowedPlayer HumanThrower =>
            new AllowedPlayer(
                "HU_Thrower",
                2,
                new GoldCoins(70000),
                new PlayerStats(6, 3, 3, 8),
                new [] { Skill.SureHands, Skill.Pass },
                new [] { SkillType.Passing, SkillType.General },
                new [] { SkillType.Agility, SkillType.Strength });

        public static AllowedPlayer HumanOgre =>
            new AllowedPlayer(
                "HU_Ogre",
                1,
                new GoldCoins(140000),
                new PlayerStats(6, 5, 2, 9),
                new [] { Skill.MightyBlow },
                new [] { SkillType.Strength },
                new [] { SkillType.General, SkillType.Agility, SkillType.Passing });

        // Dwarfs
        public static AllowedPlayer DwarfBlocker =>
            new AllowedPlayer(
                "DW_Blocker",
                16,
                new GoldCoins(70000),
                new PlayerStats(4, 3, 3, 9),
                new List<Skill>(),
                new [] { SkillType.General },
                new [] { SkillType.General });

        public static AllowedPlayer DwarfRunner =>
            new AllowedPlayer(
                "DW_Runner",
                2,
                new GoldCoins(80000),
                new PlayerStats(5, 3, 3, 8),
                new [] { Skill.Block },
                new [] { SkillType.General },
                new [] { SkillType.General });

        public static AllowedPlayer DwarfBlitzer =>
            new AllowedPlayer(
                "DW_Blitzer",
                2,
                new GoldCoins(80000),
                new PlayerStats(5, 3, 3, 9),
                new [] { Skill.Block },
                new [] { SkillType.General },
                new [] { SkillType.General });

        public static AllowedPlayer DwarfTrollSlayer =>
            new AllowedPlayer(
                "DW_TrollSlayer",
                2,
                new GoldCoins(90000),
                new PlayerStats(4, 3, 3, 9),
                new [] { Skill.Dodge },
                new [] { SkillType.General },
                new [] { SkillType.General });

        public static AllowedPlayer DwarfDeathRoller =>
            new AllowedPlayer(
                "DW_DeathRoller",
                1,
                new GoldCoins(160000),
                new PlayerStats(4, 6, 3, 10),
                new [] { Skill.Dodge },
                new [] { SkillType.General },
                new [] { SkillType.General });
    }
}