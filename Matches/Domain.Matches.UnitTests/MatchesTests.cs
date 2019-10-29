using System;
using System.Linq;
using Domain.Matches.Errors;
using Domain.Matches.Events;
using Domain.Matches.ForeignEvents;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Matches.UnitTests
{
    [TestClass]
    public class MatchesTests
    {
        [TestMethod]
        public void FinishMatchHappyPath()
        {
            var match = new Matchup();

            var player1Id = Guid.NewGuid();
            var player2Id = Guid.NewGuid();
            var player3Id = Guid.NewGuid();
            var player4Id = Guid.NewGuid();
            var player5Id = Guid.NewGuid();
            var teamReadModel = TeamReadModel(player1Id, player2Id);
            var teamReadModel2 = TeamReadModel(player3Id, player4Id, player5Id);

            var matchId = Guid.NewGuid();
            match.Apply(new MatchCreated(matchId, teamReadModel.TeamId, teamReadModel2.TeamId));
            match.Apply(new MatchStarted(matchId, teamReadModel.Players, teamReadModel2.Players));

            var playerProgression1 = PlayerProgressionTouchdown(player2Id);
            var playerProgression2 = PlayerProgressionNormal(player4Id);

            var prog1 = match.ProgressMatch(playerProgression1);
            var prog2 = match.ProgressMatch(playerProgression2);

            match.Apply(prog1.DomainEvents);
            match.Apply(prog2.DomainEvents);

            var domainResult = match.Finish();

            var domainEvent = domainResult.DomainEvents.First() as MatchFinished;
            Assert.AreEqual(teamReadModel.TeamId, domainEvent.GameResult.Winner);
            Assert.AreEqual(teamReadModel2.TeamId, domainEvent.GameResult.Looser);

            Assert.AreEqual(1, domainEvent.GameResult.HomeTeam.TouchDowns);
            Assert.AreEqual(0, domainEvent.GameResult.GuestTeam.TouchDowns);
        }

        [TestMethod]
        public void FinishMatch_PlayersNotInTeam()
        {
            var match = new Matchup();

            var player1Id = Guid.NewGuid();
            var player2Id = Guid.NewGuid();
            var player3Id = Guid.NewGuid();
            var teamReadModel = TeamReadModel(player1Id);
            var teamReadModel2 = TeamReadModel(player3Id);

            match.Apply(new MatchStarted(Guid.NewGuid(), teamReadModel.Players, teamReadModel2.Players));

            var playerProgression1 = PlayerProgressionTouchdown(player2Id);
            var progressMatch = match.ProgressMatch(playerProgression1);

            Assert.IsTrue(progressMatch.DomainErrors.Single().GetType() == typeof(PlayerWasNotPartOfTheTeamWhenStartingTheMatch));
        }

        private static PlayerProgression PlayerProgressionTouchdown(Guid playerId)
        {
            playerId = playerId ?? Guid.NewGuid();
            var playerProgression = new PlayerProgression(playerId, ProgressionEvent.PlayerMadeTouchdown);
            return playerProgression;
        }

        private static PlayerProgression PlayerProgressionNormal(Guid playerId)
        {
            playerId = playerId ?? Guid.NewGuid();
            var playerProgression = new PlayerProgression(playerId, ProgressionEvent.PlayerPassed);
            return playerProgression;
        }

        private static TeamReadModel TeamReadModel(params Guid[] playerIds)
        {
            var teamReadModel = new TeamReadModel();
            var trainerAsGuest = Guid.NewGuid();
            teamReadModel.Handle(new TeamCreated(trainerAsGuest));
            foreach (var playerId in playerIds)
            {
                teamReadModel.Handle(new PlayerBought(trainerAsGuest, playerId));
            }
            return teamReadModel;
        }
    }
}