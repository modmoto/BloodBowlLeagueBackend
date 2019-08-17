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

        // DarkElves
        public static AllowedPlayer DarkElveLineMan =>
            new AllowedPlayer(
                StringIdentity.Create("DE_LineMan"),
                16,
                new GoldCoins(70000),
                new List<Skill>(),
                new [] { SkillType.General, SkillType.Agility },
                new [] { SkillType.Strength, SkillType.Passing });

        public static AllowedPlayer DarkElveAssasine =>
            new AllowedPlayer(
                StringIdentity.Create("DE_Assassine"),
                2,
                new GoldCoins(90000),
                new [] { Skill.Shadowing, Skill.Stab },
                new [] { SkillType.General, SkillType.Agility },
                new [] { SkillType.Strength, SkillType.Passing });

        public static AllowedPlayer DarkElveBlitzer =>
            new AllowedPlayer(
                StringIdentity.Create("DE_Blitzer"),
                4,
                new GoldCoins(100000),
                new [] { Skill.Block },
                new [] { SkillType.General, SkillType.Agility },
                new [] { SkillType.Passing, SkillType.Strength });

        public static AllowedPlayer DarkElveWitchElve =>
            new AllowedPlayer(
                StringIdentity.Create("DE_WitchElve"),
                2,
                new GoldCoins(110000),
                new [] { Skill.Dodge, Skill.JumpUp, Skill.Frenzy },
                new [] { SkillType.General },
                new [] { SkillType.General });
        public static AllowedPlayer DarkElveRuner =>
            new AllowedPlayer(
                StringIdentity.Create("DE_Runner"),
                2,
                new GoldCoins(80000),
                new [] { Skill.DumpOff },
                new [] { SkillType.General, SkillType.Agility, SkillType.Passing },
                new [] { SkillType.Strength });

        // Humans
        public static AllowedPlayer HumanLineMan =>
            new AllowedPlayer(
                StringIdentity.Create("HU_LineMan"),
                16,
                new GoldCoins(50000),
                new List<Skill>(),
                new [] { SkillType.General },
                new [] { SkillType.Agility, SkillType.Strength, SkillType.Passing });

        public static AllowedPlayer HumanBlitzer =>
            new AllowedPlayer(
                StringIdentity.Create("HU_Blitzer"),
                4,
                new GoldCoins(90000),
                new [] { Skill.Block },
                new [] { SkillType.General, SkillType.Strength },
                new [] { SkillType.Passing, SkillType.Agility });

        public static AllowedPlayer HumanCatcher =>
            new AllowedPlayer(
                StringIdentity.Create("HU_Catcher"),
                4,
                new GoldCoins(70000),
                new [] { Skill.Catch, Skill.Dodge },
                new [] { SkillType.Agility, SkillType.General },
                new [] { SkillType.Strength, SkillType.Passing });

        public static AllowedPlayer HumanThrower =>
            new AllowedPlayer(
                StringIdentity.Create("HU_Thrower"),
                2,
                new GoldCoins(70000),
                new [] { Skill.SureHands, Skill.Pass },
                new [] { SkillType.Passing, SkillType.General },
                new [] { SkillType.Agility, SkillType.Strength });

        public static AllowedPlayer HumanOgre =>
            new AllowedPlayer(
                StringIdentity.Create("HU_Ogre"),
                1,
                new GoldCoins(140000),
                new [] { Skill.MightyBlow },
                new [] { SkillType.Strength },
                new [] { SkillType.General, SkillType.Agility, SkillType.Passing });

        // Dwarfs
        public static AllowedPlayer DwarfBlocker =>
            new AllowedPlayer(
                StringIdentity.Create("DW_Blocker"),
                16,
                new GoldCoins(70000),
                new List<Skill>(),
                new [] { SkillType.General },
                new [] { SkillType.General });

        public static AllowedPlayer DwarfRunner =>
            new AllowedPlayer(
                StringIdentity.Create("DW_Runner"),
                2,
                new GoldCoins(80000),
                new [] { Skill.Block },
                new [] { SkillType.General },
                new [] { SkillType.General });

        public static AllowedPlayer DwarfBlitzer =>
            new AllowedPlayer(
                StringIdentity.Create("DW_Blitzer"),
                2,
                new GoldCoins(80000),
                new [] { Skill.Block },
                new [] { SkillType.General },
                new [] { SkillType.General });

        public static AllowedPlayer DwarfTrollSlayer =>
            new AllowedPlayer(
                StringIdentity.Create("DW_TrollSlayer"),
                2,
                new GoldCoins(90000),
                new [] { Skill.Dodge },
                new [] { SkillType.General },
                new [] { SkillType.General });

        public static AllowedPlayer DwarfDeathRoller =>
            new AllowedPlayer(
                StringIdentity.Create("DW_DeathRoller"),
                1,
                new GoldCoins(160000),
                new [] { Skill.Dodge },
                new [] { SkillType.General },
                new [] { SkillType.General });
    }
}