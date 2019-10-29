using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Seasons;
using Domain.Seasons.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Matches.UnitTests
{
    [TestClass]
    public class SeasonTests
    {
        [TestMethod]
        public void AssertMethodTest_HappyPath()
        {
            var match = CreateDefaultMatchup();
            var match2 = CreateDefaultMatchup();
            var gameDays = new List<GameDay>
            {
                CreateGameDay(match, match2)
            };

            AssertMatchIsNeverPlayedTwice(gameDays);
            Assert.IsTrue(AssertMatchIsNeverPlayedTwice(gameDays));
        }

        [TestMethod]
        public void AssertMethodTest_DoubleGame()
        {
            var match = CreateDefaultMatchup();
            var gameDays = new List<GameDay>
            {
                CreateGameDay(match, match)
            };

            AssertMatchIsNeverPlayedTwice(gameDays);
            Assert.IsFalse(AssertMatchIsNeverPlayedTwice(gameDays));
        }

        [TestMethod]
        public void AssertMethodTest_switched()
        {
            var team1 = Guid.NewGuid();
            var team2 = Guid.NewGuid();
            var match = CreateDefaultMatchup(team1, team2);
            var matchSwitched = CreateDefaultMatchup(team1, team2);


            var gameDays = new List<GameDay>
            {
                CreateGameDay(match, matchSwitched)
            };

            Assert.IsFalse(AssertMatchIsNeverPlayedTwice(gameDays));
        }

        private static GameDay CreateGameDay(Matchup match, Matchup matchSwitched)
        {
            var matchups = new List<Matchup>
            {
                match, matchSwitched
            };

            var gameDay = GameDay.Create(matchups);
            return gameDay;
        }

        [TestMethod]
        public void MakePairings_GameDaysOk()
        {
            var season = CreateSeasonWithTeams(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            var domainResult = season.StartSeason();

            var domainEvent = domainResult.DomainEvents.Last() as SeasonStarted;
            Assert.AreEqual(3, domainEvent.GameDays.Count());
        }

        [TestMethod]
        public void MakePairings_PairingsOk_TwoPlayers()
        {
            var team1 = Guid.NewGuid();
            var team2 = Guid.NewGuid();
            var season = CreateSeasonWithTeams(team1, team2);

            var domainResult = season.StartSeason();

            var domainEvent = domainResult.DomainEvents.Last() as SeasonStarted;
            Assert.AreEqual(1, domainEvent.GameDays.Count());
            Assert.IsTrue(AssertMatchIsNeverPlayedTwice(domainEvent.GameDays));
        }

        [TestMethod]
        public void MakePairings_PairingsOk_ThreePlayers()
        {
            var season = CreateSeasonWithTeams(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            var domainResult = season.StartSeason();

            Assert.IsFalse(domainResult.IsOk);
        }

        [TestMethod]
        public void MakePairings_PairingsOk_fourPlayers()
        {
            var team1 = Guid.NewGuid();
            var team2 = Guid.NewGuid();
            var team3 = Guid.NewGuid();
            var team4 = Guid.NewGuid();
            var season = CreateSeasonWithTeams(team1, team2, team3, team4);

            var domainResult = season.StartSeason();

            var domainEvent = domainResult.DomainEvents.Last() as SeasonStarted;
            var domainEventGameDays = domainEvent.GameDays.ToList();
            Assert.AreEqual(3, domainEventGameDays.Count);
            Assert.IsTrue(AssertMatchIsNeverPlayedTwice(domainEventGameDays));
        }

        [TestMethod]
        public void MakePairings_PairingsOk_sixPlayers()
        {
            var team1 = Guid.NewGuid();
            var team2 = Guid.NewGuid();
            var team3 = Guid.NewGuid();
            var team4 = Guid.NewGuid();
            var team5 = Guid.NewGuid();
            var team6 = Guid.NewGuid();
            var season = CreateSeasonWithTeams(team1, team2, team3, team4, team5, team6);

            var domainResult = season.StartSeason();

            var domainEvent = domainResult.DomainEvents.Last() as SeasonStarted;
            var domainEventGameDays = domainEvent.GameDays.ToList();
            Assert.AreEqual(5, domainEventGameDays.Count);
            Assert.IsTrue(AssertMatchIsNeverPlayedTwice(domainEventGameDays));
        }

        [TestMethod]
        public void MakePairings_PairingsOk_eightPlayers()
        {
            var team1 = Guid.NewGuid();
            var team2 = Guid.NewGuid();
            var team3 = Guid.NewGuid();
            var team4 = Guid.NewGuid();
            var team5 = Guid.NewGuid();
            var team6 = Guid.NewGuid();
            var team7 = Guid.NewGuid();
            var team8 = Guid.NewGuid();
            var season = CreateSeasonWithTeams(team1, team2, team3, team4, team5, team6, team7, team8);

            var domainResult = season.StartSeason();

            var domainEvent = domainResult.DomainEvents.Last() as SeasonStarted;
            var domainEventGameDays = domainEvent.GameDays.ToList();
            Assert.AreEqual(7, domainEventGameDays.Count);
            Assert.IsTrue(AssertMatchIsNeverPlayedTwice(domainEventGameDays));
        }


        private static Matchup CreateDefaultMatchup(Guid? team1 = null, Guid? team2 = null)
        {
            var matchup = Matchup.Create(team1 ?? Guid.NewGuid(), team2 ?? Guid.NewGuid());
            return matchup;
        }

        private bool AssertMatchIsNeverPlayedTwice(IEnumerable<GameDay> domainEventGameDays)
        {
            var allMatches = domainEventGameDays.SelectMany(g => g.Matchups).ToList();
            foreach (var match in allMatches)
            {
                var matchWith = allMatches.Where(m => m.TeamAtHome == match.TeamAtHome && m.TeamAsGuest == match.TeamAsGuest
                                                      || m.TeamAtHome == match.TeamAsGuest && m.TeamAsGuest == match.TeamAtHome);
                if (matchWith.Count() != 1) return false;
            }

            return true;
        }

        private static Season CreateSeasonWithTeams(params Guid[] identities)
        {
            var season = new Season();
            var seasonId = Guid.NewGuid();
            season.Apply(new SeasonCreated(seasonId, "DummyName", DateTimeOffset.UtcNow));
            foreach (var guidIdentity in identities)
            {
                season.Apply(new TeamAddedToSeason(seasonId, guidIdentity));
            }

            return season;
        }
    }
}