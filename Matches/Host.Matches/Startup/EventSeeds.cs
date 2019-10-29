using System;
using System.Collections.Generic;
using Domain.Matches;
using Domain.Matches.Events;
using Microwave.Domain.EventSourcing;

namespace Host.Matches.Startup
{
    public static class EventSeeds
    {
        public static IEnumerable<IDomainEvent> Seeds
        {
            get
            {
                var teamAtHome = new Guid("2798435C-9C72-4ECE-BD7D-00BECBACCED7");
                var teamAsGuest = new Guid("406D35EE-421A-4D45-9F34-1834D5ACD215");
                var matchCreated = new MatchCreated(
                    new Guid("8A8CF5CC-1027-44BF-A9B7-B294856396B0"),
                    teamAtHome,
                    teamAsGuest
                );

                var playerHome = new Guid("EC48B7FF-B76D-471F-99B0-761EC43C4101");
                var playerGuest = new Guid("9CF84B11-5852-4D09-BB08-5357E6DA04C8");
                var matchStarted = new MatchStarted(
                    matchCreated.MatchId,
                    new List<Guid>
                    {

                        playerHome,
                        new Guid("C2DEDB29-C59D-4D8F-B854-6B44D04E6C7A"),
                        new Guid("E86E63E2-8C3C-4CFF-8719-68BD844CD7F7"),
                    },
                    new List<Guid>
                    {
                        playerGuest,
                        new Guid("1796B724-B55F-47A3-A498-153379C516EA"),
                    });

                var matchCreated2 = new MatchCreated(
                    new Guid("D1660304-1AAF-45C9-A980-1B519E9CAF9C"),
                    teamAtHome,
                    teamAsGuest
                );

                var matchStarted2 = new MatchStarted(
                    matchCreated2.MatchId,
                    new List<Guid>
                    {

                        playerHome,
                        new Guid("C2DEDB29-C59D-4D8F-B854-6B44D04E6C7A"),
                        new Guid("E86E63E2-8C3C-4CFF-8719-68BD844CD7F7"),
                    },
                    new List<Guid>
                    {
                        playerGuest,
                        new Guid("1796B724-B55F-47A3-A498-153379C516EA"),
                    });

                var matchProgressed = new MatchProgressed(
                    matchCreated2.MatchId,
                    new PlayerProgression(playerHome, ProgressionEvent.PlayerMadeCasualty),
                    GameResult.CreatGameResult(new PointsOfTeam(teamAtHome, 0), new PointsOfTeam(teamAsGuest, 0)));

                var matchProgressed2 = new MatchProgressed(
                    matchCreated2.MatchId,
                    new PlayerProgression(playerGuest, ProgressionEvent.PlayerMadeTouchdown),
                    GameResult.CreatGameResult(new PointsOfTeam(teamAtHome, 0), new PointsOfTeam(teamAsGuest, 1)));

                var matchProgressed3 = new MatchProgressed(
                    matchCreated2.MatchId,
                    new PlayerProgression(playerGuest, ProgressionEvent.PlayerMadeTouchdown),
                    GameResult.CreatGameResult(new PointsOfTeam(teamAtHome, 0), new PointsOfTeam(teamAsGuest, 2)));

                return new List<IDomainEvent>
                {
                    matchCreated,
                    matchStarted,
                    matchCreated2,
                    matchStarted2,
                    matchProgressed,
                    matchProgressed2,
                    matchProgressed3
                };
            }
        }
    }
}