using System.Collections.Generic;
using System.Linq;
using Domain.Teams.DomainEvents;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Teams.UnitTests
{
    [TestClass]
    public class TeamTests
    {
        [TestMethod]
        public void BuyPlayer()
        {
            var playerTypeId = "de_Nelf";
            var domainResult = Team.Draft(
                "Elves",
                "King Kingz",
                "Simon",
                new List<AllowedPlayer>
                {
                    new AllowedPlayer(
                        playerTypeId,
                        10,
                        new GoldCoins(50000))
                });
            var team = new Team();
            team.Apply(domainResult.DomainEvents);

            var bought = team.BuyPlayer(playerTypeId);

            Assert.IsTrue(bought.IsOk);
            var playerBought = (PlayerAddedToDraft) bought.DomainEvents.Single();
            Assert.AreEqual(playerTypeId, playerBought.PlayerTypeId);
            Assert.AreEqual(new GoldCoins(950000), playerBought.NewTeamChestBalance);
            Assert.AreEqual(1, playerBought.PlayerPositionNumber);


            var bought2 = team.BuyPlayer(playerTypeId);
            var domainEvent = (PlayerAddedToDraft) bought2.DomainEvents.Single();
            Assert.AreEqual(2, domainEvent.PlayerPositionNumber);
        }

        [TestMethod]
        public void CommitDraft()
        {
            var playerTypeId = "de_Nelf";
            var domainResult = Team.Draft(
                "Elves",
                "King Kingz",
                "Simon",
                new List<AllowedPlayer>
                {
                    new AllowedPlayer(
                        playerTypeId,
                        30,
                        new GoldCoins(10000))
                });
            var team = new Team();
            team.Apply(domainResult.DomainEvents);

            team.BuyPlayer(playerTypeId);
            team.BuyPlayer(playerTypeId);
            team.BuyPlayer(playerTypeId);
            team.BuyPlayer(playerTypeId);
            team.BuyPlayer(playerTypeId);
            team.BuyPlayer(playerTypeId);
            team.BuyPlayer(playerTypeId);
            team.BuyPlayer(playerTypeId);
            team.BuyPlayer(playerTypeId);
            team.BuyPlayer(playerTypeId);
            team.BuyPlayer(playerTypeId);

            var commitDraft = team.CommitDraft();

            var commitDraftDomainEvents = commitDraft.DomainEvents.ToList();
            Assert.AreEqual(12, commitDraftDomainEvents.Count);
            Assert.IsInstanceOfType(commitDraftDomainEvents[0], typeof(TeamCreated));
            Assert.AreEqual(1, ((PlayerBought) commitDraftDomainEvents[1]).PlayerPositionNumber);
            Assert.AreEqual(2, ((PlayerBought) commitDraftDomainEvents[2]).PlayerPositionNumber);
            Assert.AreEqual(11, ((PlayerBought) commitDraftDomainEvents[11]).PlayerPositionNumber);


            team.BuyPlayer(playerTypeId);
            team.BuyPlayer(playerTypeId);
            team.BuyPlayer(playerTypeId);
            team.BuyPlayer(playerTypeId);
            team.BuyPlayer(playerTypeId);
            var events = team.BuyPlayer(playerTypeId);
            var buyAfterEvents = events.DomainEvents.ToList();

            Assert.AreEqual(17, ((PlayerBought)buyAfterEvents.Single()).PlayerPositionNumber);
        }

        [TestMethod]
        public void RemovePlayersMessesUpTeamNumbers()
        {
            var playerTypeId = "de_Nelf";
            var domainResult = Team.Draft(
                "Elves",
                "King Kingz",
                "Simon",
                new List<AllowedPlayer>
                {
                    new AllowedPlayer(
                        playerTypeId,
                        30,
                        new GoldCoins(10000))
                });
            var team = new Team();
            team.Apply(domainResult.DomainEvents);

            team.BuyPlayer(playerTypeId);
            team.BuyPlayer(playerTypeId);
            team.BuyPlayer(playerTypeId);
            var forthPlayer = team.BuyPlayer(playerTypeId);
            team.BuyPlayer(playerTypeId);
            team.BuyPlayer(playerTypeId);
            team.BuyPlayer(playerTypeId);
            team.BuyPlayer(playerTypeId);
            team.BuyPlayer(playerTypeId);
            team.BuyPlayer(playerTypeId);
            team.BuyPlayer(playerTypeId);

            team.CommitDraft();

            var playerId = ((PlayerAddedToDraft)forthPlayer.DomainEvents.Single()).PlayerId;
            team.RemovePlayer(playerId);

            var events = team.BuyPlayer(playerTypeId);
            var buyAfterEvents = events.DomainEvents.ToList();

            Assert.AreEqual(4, ((PlayerBought)buyAfterEvents.Single()).PlayerPositionNumber);
        }

        [TestMethod]
        public void FindFirstFreeNumber()
        {
            var team = new Team();
            var firstFreeNumber = team.FindFirstFreeNumber(new List<int> { 1, 2 }, 1);
            Assert.AreEqual(3, firstFreeNumber);
        }

        [TestMethod]
        public void FindFirstFreeNumber2()
        {
            var team = new Team();
            var firstFreeNumber = team.FindFirstFreeNumber(new List<int> { 1, 3}, 1);
            Assert.AreEqual(2, firstFreeNumber);
        }

        [TestMethod]
        public void FindFirstFreeNumber3()
        {
            var team = new Team();
            var firstFreeNumber = team.FindFirstFreeNumber(new List<int> { 2, 3}, 1);
            Assert.AreEqual(1, firstFreeNumber);
        }

        [TestMethod]
        public void FindFirstFreeNumber4()
        {
            var team = new Team();
            var firstFreeNumber = team.FindFirstFreeNumber(new List<int> { 2 }, 1);
            Assert.AreEqual(1, firstFreeNumber);
        }

        [TestMethod]
        public void FindFirstFreeNumber5()
        {
            var team = new Team();
            var firstFreeNumber = team.FindFirstFreeNumber(new List<int> { 1 }, 1);
            Assert.AreEqual(2, firstFreeNumber);
        }

        [TestMethod]
        public void FindFirstFreeNumber6()
        {
            var team = new Team();
            var firstFreeNumber = team.FindFirstFreeNumber(new List<int>(), 1);
            Assert.AreEqual(1, firstFreeNumber);
        }
    }
}