using System.Linq;
using Domain.Matches.Errors;
using Domain.Matches.Events;
using Domain.Matches.ForeignEvents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microwave.Domain.Identities;

namespace Domain.Matches.UnitTests
{
    [TestClass]
    public class MatchesTests
    {
        [TestMethod]
        public void FinishMatchHappyPath()
        {
            var match = new Matchup();

            var player1Id = GuidIdentity.Create();
            var player2Id = GuidIdentity.Create();
            var player3Id = GuidIdentity.Create();
            var player4Id = GuidIdentity.Create();
            var player5Id = GuidIdentity.Create();
            var teamReadModel = TeamReadModel(player1Id, player2Id);
            var teamReadModel2 = TeamReadModel(player3Id, player4Id, player5Id);

            var matchId = GuidIdentity.Create();
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
        }

        [TestMethod]
        public void FinishMatch_PlayersNotInTeam()
        {
            var match = new Matchup();

            var player1Id = GuidIdentity.Create();
            var player2Id = GuidIdentity.Create();
            var player3Id = GuidIdentity.Create();
            var teamReadModel = TeamReadModel(player1Id);
            var teamReadModel2 = TeamReadModel(player3Id);

            match.Apply(new MatchStarted(GuidIdentity.Create(), teamReadModel.Players, teamReadModel2.Players));

            var playerProgression1 = PlayerProgressionTouchdown(player2Id);
            var progressMatch = match.ProgressMatch(playerProgression1);

            Assert.IsTrue(progressMatch.DomainErrors.Single().GetType() == typeof(PlayerWasNotPartOfTheTeamWhenStartingTheMatch));
        }

        private static PlayerProgression PlayerProgressionTouchdown(GuidIdentity playerId)
        {
            playerId = playerId ?? GuidIdentity.Create();
            var playerProgression = new PlayerProgression(playerId, ProgressionEvent.PlayerMadeTouchdown);
            return playerProgression;
        }

        private static PlayerProgression PlayerProgressionNormal(GuidIdentity playerId)
        {
            playerId = playerId ?? GuidIdentity.Create();
            var playerProgression = new PlayerProgression(playerId, ProgressionEvent.PlayerPassed);
            return playerProgression;
        }

        private static TeamReadModel TeamReadModel(params GuidIdentity[] playerIds)
        {
            var teamReadModel = new TeamReadModel();
            var trainerAsGuest = GuidIdentity.Create();
            teamReadModel.Handle(new TeamCreated(trainerAsGuest));
            foreach (var playerId in playerIds)
            {
                teamReadModel.Handle(new PlayerBought(trainerAsGuest, playerId));
            }
            return teamReadModel;
        }
    }
}