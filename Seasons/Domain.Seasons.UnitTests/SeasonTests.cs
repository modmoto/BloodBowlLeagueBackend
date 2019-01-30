using System.Collections.Generic;
using System.Linq;
using Domain.Seasons.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microwave.Domain;

namespace Domain.Seasons.UnitTests
{
    [TestClass]
    public class SeasonTests
    {
        [TestMethod]
        public void MakePairings_GameDaysOk()
        {
            var season = CreateSeasonWithTeams(GuidIdentity.Create(), GuidIdentity.Create(), GuidIdentity.Create(), GuidIdentity.Create());

            var domainResult = season.StartSeason();

            var domainEvent = domainResult.DomainEvents.First() as SeasonStarted;
            Assert.AreEqual(3, domainEvent.GameDays.Count());
        }

        [TestMethod]
        public void MakePairings_PairingsOk_TwoPlayers()
        {
            var team1 = GuidIdentity.Create();
            var team2 = GuidIdentity.Create();
            var season = CreateSeasonWithTeams(team1, team2);

            var domainResult = season.StartSeason();

            var domainEvent = domainResult.DomainEvents.First() as SeasonStarted;
            Assert.AreEqual(1, domainEvent.GameDays.Count());
            AssertMatchIsNeverPlayedTwice(domainEvent.GameDays);
        }

        [TestMethod]
        public void MakePairings_PairingsOk_ThreePlayers()
        {
            var season = CreateSeasonWithTeams(GuidIdentity.Create(), GuidIdentity.Create(), GuidIdentity.Create());

            var domainResult = season.StartSeason();

            Assert.IsFalse(domainResult.IsOk);
        }

        [TestMethod]
        public void MakePairings_PairingsOk_fourPlayers()
        {
            var team1 = GuidIdentity.Create();
            var team2 = GuidIdentity.Create();
            var team3 = GuidIdentity.Create();
            var team4 = GuidIdentity.Create();
            var season = CreateSeasonWithTeams(team1, team2, team3, team4);

            var domainResult = season.StartSeason();

            var domainEvent = domainResult.DomainEvents.First() as SeasonStarted;
            var domainEventGameDays = domainEvent.GameDays.ToList();
            Assert.AreEqual(3, domainEventGameDays.Count);
            AssertMatchIsNeverPlayedTwice(domainEventGameDays);
        }

        private void AssertMatchIsNeverPlayedTwice(IEnumerable<GameDay> domainEventGameDays)
        {
            var allMatches = domainEventGameDays.SelectMany(g => g.Matchups).ToList();
            foreach (var match in allMatches)
            {
                var matchWith = allMatches.SingleOrDefault(m => m.HomeTeam == match.HomeTeam && m.GuestTeam == match.GuestTeam
                                                               || m.HomeTeam == match.GuestTeam && m.GuestTeam == match.HomeTeam);
                Assert.IsNotNull(matchWith);
            }
        }

        [TestMethod]
        public void MakePairings_PairingsOk_sixPlayers()
        {
            var team1 = GuidIdentity.Create();
            var team2 = GuidIdentity.Create();
            var team3 = GuidIdentity.Create();
            var team4 = GuidIdentity.Create();
            var team5 = GuidIdentity.Create();
            var team6 = GuidIdentity.Create();
            var season = CreateSeasonWithTeams(team1, team2, team3, team4, team5, team6);

            var domainResult = season.StartSeason();

            var domainEvent = domainResult.DomainEvents.First() as SeasonStarted;
            var domainEventGameDays = domainEvent.GameDays.ToList();
            Assert.AreEqual(5, domainEventGameDays.Count);
            AssertMatchIsNeverPlayedTwice(domainEvent.GameDays);
        }

        [TestMethod]
        public void MakePairings_PairingsOk_eightPlayers()
        {
            var team1 = GuidIdentity.Create();
            var team2 = GuidIdentity.Create();
            var team3 = GuidIdentity.Create();
            var team4 = GuidIdentity.Create();
            var team5 = GuidIdentity.Create();
            var team6 = GuidIdentity.Create();
            var team7 = GuidIdentity.Create();
            var team8 = GuidIdentity.Create();
            var season = CreateSeasonWithTeams(team1, team2, team3, team4, team5, team6, team7, team8);

            var domainResult = season.StartSeason();

            var domainEvent = domainResult.DomainEvents.First() as SeasonStarted;
            var domainEventGameDays = domainEvent.GameDays.ToList();
            Assert.AreEqual(7, domainEventGameDays.Count);
            AssertMatchIsNeverPlayedTwice(domainEvent.GameDays);
        }

        private static Season CreateSeasonWithTeams(params GuidIdentity[] identities)
        {
            var season = new Season();
            var seasonId = GuidIdentity.Create();
            season.Apply(new SeasonCreated(seasonId));
            foreach (var guidIdentity in identities)
            {
                season.Apply(new TeamAddedToSeason(seasonId, guidIdentity));
            }

            return season;
        }
    }
}
