using System.Collections.Generic;
using Domain.Races;
using Domain.Races.Races;
using Domain.Races.Races.DomainEvents;
using Domain.Races.Skills;
using Domain.Races.Skills.DomainEvents;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;

namespace Teams.WriteHost.Startup
{
    public class EventSeeds
    {
        public static IEnumerable<IDomainEvent> Seeds
        {
            get
            {
                var darkElvesCreated = new RaceCreated(StringIdentity.Create("DarkElves"),
                    new List<AllowedPlayer>
                        {
                            AllowedPlayer.DarkElveAssasine,
                            AllowedPlayer.DarkElveBlitzer,
                            AllowedPlayer.DarkElveLineMan,
                            AllowedPlayer.DarkElveWitchElve
                        });

                var humansCreated = new RaceCreated(StringIdentity.Create("Humans"),
                    new List<AllowedPlayer>
                    {
                        AllowedPlayer.HumanBlitzer,
                        AllowedPlayer.HumanCatcher,
                        AllowedPlayer.HumanOgre,
                        AllowedPlayer.HumanThrower,
                        AllowedPlayer.HumanLineMan
                    });

                var dwarfsCreated = new RaceCreated(StringIdentity.Create("Dwarfs"),
                    new List<AllowedPlayer>
                    {
                        AllowedPlayer.DwarfBlitzer,
                        AllowedPlayer.DwarfBlocker,
                        AllowedPlayer.DwarfRunner,
                        AllowedPlayer.DwarfTrollSlayer,
                        AllowedPlayer.DwarfDeathRoller,
                    });

                var events = new List<IDomainEvent>
                {
                    darkElvesCreated,
                    humansCreated,
                    dwarfsCreated,
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
    }
}