using System.Collections.Generic;
using Domain.Races.Races;
using Domain.Races.Races.DomainEvents;
using Domain.Races.Skills;
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
                    Skill.Block,
                    Skill.Catch,
                    Skill.Dodge,
                    Skill.Pass,
                    Skill.Throw,
                    Skill.MightyBlow,
                    Skill.PickUp,
                    Skill.PlusOneStrength,
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