using System;
using System.Collections.Generic;
using Domain.Teams;
using Domain.Teams.DomainEvents;
using Microwave.Domain.EventSourcing;

namespace Teams.WriteHost.Startup
{
    public static class EventSeedsTeams
    {
        public static IEnumerable<IDomainEvent> Seeds
        {
            get
            {
                var events = new List<IDomainEvent>
                {
                    new TeamDraftCreated(
                        new Guid("772F7E84-4237-4634-AF85-5C0D72FF8DBD"),
                        "Humans",
                        "Karlsruher Könige",
                        "Walter",
                        HumanPlayers,
                        new GoldCoins(1000000)),
                    new TeamCreated(
                        new Guid("772F7E84-4237-4634-AF85-5C0D72FF8DBD"),
                        "Humans",
                        "Karlsruher Könige",
                        "Walter",
                        HumanPlayers,
                        new GoldCoins(1000000)),
                    new TeamDraftCreated(
                        new Guid("38C41447-21F6-4941-BD7E-AC97EF866197"),
                        "Dwarfs",
                        "Berghausen Brüglerz",
                        "Rahel",
                        DwarfPlayers,
                        new GoldCoins(1000000)),
                    new TeamCreated(
                        new Guid("38C41447-21F6-4941-BD7E-AC97EF866197"),
                        "Dwarfs",
                        "Berghausen Brüglerz",
                        "Rahel",
                        DwarfPlayers,
                        new GoldCoins(1000000)),
                    new TeamDraftCreated(
                        new Guid("13552A55-D612-40D5-88F9-5106A05CCBAC"),
                        "Dwarfs",
                        "Rat Bullz",
                        "Silas",
                        DwarfPlayers,
                        new GoldCoins(1000000)),
                    new TeamCreated(
                        new Guid("13552A55-D612-40D5-88F9-5106A05CCBAC"),
                        "Dwarfs",
                        "Rat Bullz",
                        "Silas",
                        DwarfPlayers,
                        new GoldCoins(1000000)),
                    new TeamDraftCreated(
                        new Guid("D5BB0FDA-BBE5-4271-8311-460AE5AD3DDA"),
                        "DarkElves",
                        "Spikey Bits",
                        "Merlin",
                        DarkElvPlayers,
                        new GoldCoins(1000000)),
                    new TeamCreated(
                        new Guid("D5BB0FDA-BBE5-4271-8311-460AE5AD3DDA"),
                        "DarkElves",
                        "Spikey Bits",
                        "Merlin",
                        DarkElvPlayers,
                        new GoldCoins(1000000))
                };

                events.AddRange(NightElveTeamWithPlayers);
                events.AddRange(TeamInDraftMode);
                events.AddRange(HumanTeamWithPlayers);

                return events;
            }
        }

        public static IEnumerable<IDomainEvent> TeamInDraftMode
        {
            get
            {
                var team1 = new Guid("FFD92960-33DE-4B63-8EFD-AF0A75E4F017");

                var events = new List<IDomainEvent>
                {
                    new TeamDraftCreated(
                        team1,
                        "DarkElves",
                        "Team in DraftModes",
                        "Mark",
                        DarkElvPlayers,
                        new GoldCoins(1000000)),
                    new PlayerAddedToDraft(
                        team1,
                        "DE_LineMan",
                        1,
                        new Guid("1BF57260-77E5-4E19-99C3-3B6D7205B8BE"),
                        new GoldCoins(930000)),
                    new PlayerAddedToDraft(
                        team1,
                        "DE_LineMan",
                        2,
                        new Guid("5543633B-78B6-40EC-A84D-0F637A3F05EE"),
                        new GoldCoins(860000)),
                    new PlayerAddedToDraft(
                        team1,
                        "DE_LineMan",
                        3,
                        new Guid("F9C331B7-AC52-46FF-B565-CFD6E690106B"),
                        new GoldCoins(790000))
                };

                return events;
            }
        }

        public static IEnumerable<IDomainEvent> NightElveTeamWithPlayers
        {
            get
            {
                var team1 = new Guid("2798435C-9C72-4ECE-BD7D-00BECBACCED7");

                var events = new List<IDomainEvent>
                {
                    new TeamDraftCreated(
                        team1,
                        "DarkElves",
                        "3er Team",
                        "Der Simon",
                        DarkElvPlayers,
                        new GoldCoins(1000000)),
                    new PlayerAddedToDraft(
                        team1,
                        "DE_LineMan",
                        1,
                        new Guid("EC48B7FF-B76D-471F-99B0-761EC43C4101"),
                        new GoldCoins(930000)),
                    new PlayerAddedToDraft(
                        team1,
                        "DE_LineMan",
                        2,
                        new Guid("C2DEDB29-C59D-4D8F-B854-6B44D04E6C7A"),
                        new GoldCoins(860000)),
                    new PlayerAddedToDraft(
                        team1,
                        "DE_LineMan",
                        3,
                        new Guid("E86E63E2-8C3C-4CFF-8719-68BD844CD7F7"),
                        new GoldCoins(790000)),
                    new TeamCreated(team1,
                        "DarkElves",
                        "3er Team",
                        "Der Simon",
                        DarkElvPlayers,
                        new GoldCoins(790000)),
                    new PlayerBought(
                        team1,
                        "DE_LineMan",
                        1,
                        new Guid("EC48B7FF-B76D-471F-99B0-761EC43C4101"),
                        new GoldCoins(930000)),
                    new PlayerBought(
                        team1,
                        "DE_LineMan",
                        2,
                        new Guid("C2DEDB29-C59D-4D8F-B854-6B44D04E6C7A"),
                        new GoldCoins(860000)),
                    new PlayerBought(
                        team1,
                        "DE_LineMan",
                        3,
                        new Guid("E86E63E2-8C3C-4CFF-8719-68BD844CD7F7"),
                        new GoldCoins(790000)),
                };

                return events;
            }
        }

        public static IEnumerable<IDomainEvent> HumanTeamWithPlayers
        {
            get
            {
                var team2 = new Guid("406D35EE-421A-4D45-9F34-1834D5ACD215");

                var events = new List<IDomainEvent>
                {
                    new TeamDraftCreated(
                        team2,
                        "Humans",
                        "2er Team",
                        "Der Simon Poppinga",
                        HumanPlayers,
                        new GoldCoins(1000000)),
                    new PlayerAddedToDraft(
                        team2,
                        "HU_Blitzer",
                        1,
                        new Guid("9CF84B11-5852-4D09-BB08-5357E6DA04C8"),
                        new GoldCoins(950000)),
                    new PlayerAddedToDraft(
                        team2,
                        "HU_LineMan",
                        2,
                        new Guid("1796B724-B55F-47A3-A498-153379C516EA"),
                        new GoldCoins(900000)),
                    new TeamCreated(
                        team2,
                        "Humans",
                        "2er Team",
                        "Der Simon Poppinga",
                        HumanPlayers,
                        new GoldCoins(900000)),
                    new PlayerBought(
                        team2,
                        "HU_Blitzer",
                        1,
                        new Guid("9CF84B11-5852-4D09-BB08-5357E6DA04C8"),
                        new GoldCoins(950000)),
                    new PlayerBought(
                        team2,
                        "HU_LineMan",
                        2,
                        new Guid("1796B724-B55F-47A3-A498-153379C516EA"),
                        new GoldCoins(900000))
                };

                return events;
            }
        }

        public static IEnumerable<AllowedPlayer> DarkElvPlayers =>
            new List<AllowedPlayer>
            {
                new AllowedPlayer("DE_Assassine", 2, new GoldCoins(90000)),
                new AllowedPlayer("DE_LineMan", 16, new GoldCoins(70000)),
                new AllowedPlayer("DE_Blitzer", 4, new GoldCoins(100000)),
                new AllowedPlayer("DE_WitchElve", 2, new GoldCoins(110000)),
                new AllowedPlayer("DE_Runner", 2, new GoldCoins(80000))
            };

        public static IEnumerable<AllowedPlayer> HumanPlayers =>
            new List<AllowedPlayer>
            {
                new AllowedPlayer("HU_LineMan", 16, new GoldCoins(50000)),
                new AllowedPlayer("HU_Blitzer", 4, new GoldCoins(90000)),
                new AllowedPlayer("HU_Catcher", 4, new GoldCoins(70000)),
                new AllowedPlayer("HU_Thrower", 2, new GoldCoins(70000)),
                new AllowedPlayer("HU_Ogre", 1, new GoldCoins(150000))
            };

        public static IEnumerable<AllowedPlayer> DwarfPlayers =>
            new List<AllowedPlayer>
            {
                new AllowedPlayer("DW_Blocker", 16, new GoldCoins(70000)),
                new AllowedPlayer("DW_Runner", 2, new GoldCoins(80000)),
                new AllowedPlayer("DW_Blitzer", 2, new GoldCoins(80000)),
                new AllowedPlayer("DW_TrollSlayer", 2, new GoldCoins(90000)),
                new AllowedPlayer("DW_DeathRoller", 1, new GoldCoins(160000))
            };
    }
}