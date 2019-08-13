using System.Collections.Generic;
using Domain.Races.Races;
using Domain.Races.Races.DomainEvents;
using Domain.Races.Skills;
using Domain.Races.Skills.DomainEvents;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;

namespace Host.Races.Startup
{
    public class EventSeeds
    {
        public static IEnumerable<IDomainEvent> Seeds
        {
            get
            {
                var events = new List<IDomainEvent>
                {
                    DarkElfTeam,
                    HumanTeam,
                    DwarfTeam,
                    new SkillCreated(Skill.Block.SkillId, Skill.Block.SkillType),
                    new SkillCreated(Skill.Catch.SkillId, Skill.Catch.SkillType),
                    new SkillCreated(Skill.Dodge.SkillId, Skill.Dodge.SkillType),
                    new SkillCreated(Skill.Pass.SkillId, Skill.Pass.SkillType),
                    new SkillCreated(Skill.Throw.SkillId, Skill.Throw.SkillType),
                    new SkillCreated(Skill.MightyBlow.SkillId, Skill.MightyBlow.SkillType),
                    new SkillCreated(Skill.PickUp.SkillId, Skill.PickUp.SkillType),
                    new SkillCreated(Skill.PlusOneStrength.SkillId, Skill.PlusOneStrength.SkillType),
                };
                return events;
            }
        }

        public static IDomainEvent DwarfTeam => new RaceCreated(StringIdentity.Create("Dwarfs"),
            new List<AllowedPlayer>
            {
                AllowedPlayer.DwarfBlitzer,
                AllowedPlayer.DwarfBlocker,
                AllowedPlayer.DwarfRunner,
                AllowedPlayer.DwarfTrollSlayer,
                AllowedPlayer.DwarfDeathRoller,
            });

        public static IDomainEvent HumanTeam => new RaceCreated(StringIdentity.Create("Humans"),
            new List<AllowedPlayer>
            {
                AllowedPlayer.HumanBlitzer,
                AllowedPlayer.HumanCatcher,
                AllowedPlayer.HumanOgre,
                AllowedPlayer.HumanThrower,
                AllowedPlayer.HumanLineMan
            });

        public static IDomainEvent DarkElfTeam => new RaceCreated(StringIdentity.Create("DarkElves"),
            new List<AllowedPlayer>
            {
                AllowedPlayer.DarkElveAssasine,
                AllowedPlayer.DarkElveBlitzer,
                AllowedPlayer.DarkElveLineMan,
                AllowedPlayer.DarkElveWitchElve
            });
    }
}