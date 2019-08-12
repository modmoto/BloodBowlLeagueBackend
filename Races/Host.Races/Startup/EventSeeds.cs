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

//                var humansCreated = new RaceCreated(StringIdentity.Create("Humans"), new List<AllowedPlayer>
//                {
//                    new AllowedPlayer(StringIdentity.Create("HU_LineMan"), 16, new GoldCoins(50000)),
//                    new AllowedPlayer(StringIdentity.Create("HU_Blitzer"), 4, new GoldCoins(90000)),
//                    new AllowedPlayer(StringIdentity.Create("HU_Catcher"), 4, new GoldCoins(70000)),
//                    new AllowedPlayer(StringIdentity.Create("HU_Thrower"), 2, new GoldCoins(70000)),
//                    new AllowedPlayer(StringIdentity.Create("HU_Ogre"), 1, new GoldCoins(70000))
//                });
//
//                var dwarfsCreated = new RaceCreated(StringIdentity.Create("Dwarfs"), new List<AllowedPlayer>
//                {
//                    new AllowedPlayer(StringIdentity.Create("DW_Blocker"), 16, new GoldCoins(70000)),
//                    new AllowedPlayer(StringIdentity.Create("DW_Runner"), 2, new GoldCoins(80000)),
//                    new AllowedPlayer(StringIdentity.Create("DW_Blitzer"), 2, new GoldCoins(80000)),
//                    new AllowedPlayer(StringIdentity.Create("DW_TrollSlayer"), 2, new GoldCoins(90000)),
//                    new AllowedPlayer(StringIdentity.Create("DW_DeathRoller"), 1, new GoldCoins(160000))
//                });

                var events = new List<IDomainEvent>
                {
                    darkElvesCreated,
                    Skill.Block,
                    Skill.Catch,
                    Skill.Dodge,
                    Skill.Pass,
                    Skill.Throw,
                    Skill.MightyBlow,
                    Skill.PickUp,
                    Skill.PlusOneStrength,
//                    humansCreated,
//                    dwarfsCreated
                };
                return events;
            }
        }
    }
}