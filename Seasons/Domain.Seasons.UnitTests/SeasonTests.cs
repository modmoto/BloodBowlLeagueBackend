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
            Assert.AreEqual(team1, domainEvent.GameDays.First().Matchups.Single().HomeTeam);
            Assert.AreEqual(team2, domainEvent.GameDays.First().Matchups.Single().GuestTeam);
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

            var matchesOnDay1 = domainEventGameDays[0].Matchups.ToList();
            Assert.AreEqual(team1, matchesOnDay1[0].HomeTeam);
            Assert.AreEqual(team2, matchesOnDay1[0].GuestTeam);
            Assert.AreEqual(team3, matchesOnDay1[1].HomeTeam);
            Assert.AreEqual(team4, matchesOnDay1[1].GuestTeam);

            var matchesOnDay2 = domainEventGameDays[1].Matchups.ToList();
            Assert.AreEqual(team4, matchesOnDay2[0].HomeTeam);
            Assert.AreEqual(team1, matchesOnDay2[0].GuestTeam);
            Assert.AreEqual(team2, matchesOnDay2[1].HomeTeam);
            Assert.AreEqual(team3, matchesOnDay2[1].GuestTeam);

            var matchesOnDay3 = domainEventGameDays[2].Matchups.ToList();
            Assert.AreEqual(team2, matchesOnDay3[0].HomeTeam);
            Assert.AreEqual(team4, matchesOnDay3[0].GuestTeam);
            Assert.AreEqual(team3, matchesOnDay3[1].HomeTeam);
            Assert.AreEqual(team1, matchesOnDay3[1].GuestTeam);
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

            var matchesOnDay1 = domainEventGameDays[0].Matchups.ToList();
            Assert.AreEqual(team1, matchesOnDay1[0].HomeTeam);
            Assert.AreEqual(team2, matchesOnDay1[0].GuestTeam);
            Assert.AreEqual(team3, matchesOnDay1[1].HomeTeam);
            Assert.AreEqual(team4, matchesOnDay1[1].GuestTeam);
            Assert.AreEqual(team5, matchesOnDay1[2].HomeTeam);
            Assert.AreEqual(team6, matchesOnDay1[2].GuestTeam);

            var matchesOnDay2 = domainEventGameDays[1].Matchups.ToList();
            Assert.AreEqual(team1, matchesOnDay2[0].HomeTeam);
            Assert.AreEqual(team3, matchesOnDay2[0].GuestTeam);
            Assert.AreEqual(team2, matchesOnDay2[1].HomeTeam);
            Assert.AreEqual(team4, matchesOnDay2[1].GuestTeam);
            Assert.AreEqual(team5, matchesOnDay2[2].HomeTeam);
            Assert.AreEqual(team6, matchesOnDay2[2].GuestTeam);

            var matchesOnDay3 = domainEventGameDays[2].Matchups.ToList();
            Assert.AreEqual(team1, matchesOnDay3[0].HomeTeam);
            Assert.AreEqual(team4, matchesOnDay3[0].GuestTeam);
            Assert.AreEqual(team2, matchesOnDay3[1].HomeTeam);
            Assert.AreEqual(team3, matchesOnDay3[1].GuestTeam);
            Assert.AreEqual(team5, matchesOnDay3[2].HomeTeam);
            Assert.AreEqual(team6, matchesOnDay3[2].GuestTeam);

            var matchesOnDay4 = domainEventGameDays[3].Matchups.ToList();
            Assert.AreEqual(team1, matchesOnDay4[0].HomeTeam);
            Assert.AreEqual(team4, matchesOnDay4[0].GuestTeam);
            Assert.AreEqual(team2, matchesOnDay4[1].HomeTeam);
            Assert.AreEqual(team3, matchesOnDay4[1].GuestTeam);
            Assert.AreEqual(team5, matchesOnDay4[2].HomeTeam);
            Assert.AreEqual(team6, matchesOnDay4[2].GuestTeam);

            var matchesOnDay5 = domainEventGameDays[4].Matchups.ToList();
            Assert.AreEqual(team1, matchesOnDay5[0].HomeTeam);
            Assert.AreEqual(team4, matchesOnDay5[0].GuestTeam);
            Assert.AreEqual(team2, matchesOnDay5[1].HomeTeam);
            Assert.AreEqual(team3, matchesOnDay5[1].GuestTeam);
            Assert.AreEqual(team5, matchesOnDay5[2].HomeTeam);
            Assert.AreEqual(team6, matchesOnDay5[2].GuestTeam);
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
            Assert.AreEqual(5, domainEventGameDays.Count);

            var matchesOnDay1 = domainEventGameDays[0].Matchups.ToList();
            Assert.AreEqual(team1, matchesOnDay1[0].HomeTeam);
            Assert.AreEqual(team2, matchesOnDay1[0].GuestTeam);
            Assert.AreEqual(team3, matchesOnDay1[1].HomeTeam);
            Assert.AreEqual(team4, matchesOnDay1[1].GuestTeam);
            Assert.AreEqual(team5, matchesOnDay1[2].HomeTeam);
            Assert.AreEqual(team6, matchesOnDay1[2].GuestTeam);

            var matchesOnDay2 = domainEventGameDays[1].Matchups.ToList();
            Assert.AreEqual(team1, matchesOnDay2[0].HomeTeam);
            Assert.AreEqual(team3, matchesOnDay2[0].GuestTeam);
            Assert.AreEqual(team2, matchesOnDay2[1].HomeTeam);
            Assert.AreEqual(team4, matchesOnDay2[1].GuestTeam);
            Assert.AreEqual(team5, matchesOnDay2[2].HomeTeam);
            Assert.AreEqual(team6, matchesOnDay2[2].GuestTeam);

            var matchesOnDay3 = domainEventGameDays[2].Matchups.ToList();
            Assert.AreEqual(team1, matchesOnDay3[0].HomeTeam);
            Assert.AreEqual(team4, matchesOnDay3[0].GuestTeam);
            Assert.AreEqual(team2, matchesOnDay3[1].HomeTeam);
            Assert.AreEqual(team3, matchesOnDay3[1].GuestTeam);
            Assert.AreEqual(team5, matchesOnDay3[2].HomeTeam);
            Assert.AreEqual(team6, matchesOnDay3[2].GuestTeam);

            var matchesOnDay4 = domainEventGameDays[3].Matchups.ToList();
            Assert.AreEqual(team1, matchesOnDay4[0].HomeTeam);
            Assert.AreEqual(team4, matchesOnDay4[0].GuestTeam);
            Assert.AreEqual(team2, matchesOnDay4[1].HomeTeam);
            Assert.AreEqual(team3, matchesOnDay4[1].GuestTeam);
            Assert.AreEqual(team5, matchesOnDay4[2].HomeTeam);
            Assert.AreEqual(team6, matchesOnDay4[2].GuestTeam);

            var matchesOnDay5 = domainEventGameDays[4].Matchups.ToList();
            Assert.AreEqual(team1, matchesOnDay5[0].HomeTeam);
            Assert.AreEqual(team4, matchesOnDay5[0].GuestTeam);
            Assert.AreEqual(team2, matchesOnDay5[1].HomeTeam);
            Assert.AreEqual(team3, matchesOnDay5[1].GuestTeam);
            Assert.AreEqual(team5, matchesOnDay5[2].HomeTeam);
            Assert.AreEqual(team6, matchesOnDay5[2].GuestTeam);
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
