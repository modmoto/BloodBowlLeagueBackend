using System.Collections.Generic;
using System.Linq;
using Domain.Teams.DomainEvents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microwave.Domain.Identities;

namespace Domain.Teams.UnitTests
{
    [TestClass]
    public class TeamTests
    {
        [TestMethod]
        public void BuyPlayer()
        {
            var playerTypeId = StringIdentity.Create("de_Nelf");
            var domainResult = Team.Draft(
                StringIdentity.Create("Elves"),
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
        }
    }
}