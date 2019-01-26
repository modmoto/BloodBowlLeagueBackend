using System.Linq;
using Domain.Matches.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microwave.Domain;

namespace Domain.Matches.UnitTests
{
    [TestClass]
    public class MatchesTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var match = new Match();
            var trainerAsGuest = GuidIdentity.Create();
            var trainerAtHome = GuidIdentity.Create();
            match.Apply(new MatchCreated(GuidIdentity.Create(), trainerAtHome, trainerAsGuest));

            var playerProgression = new PlayerProgression(GuidIdentity.Create(), new []{ ProgressionEvent.PlayerMadeTouchdown, ProgressionEvent.PlayerMadeCasualty});
            var playerProgression2 = new PlayerProgression(GuidIdentity.Create(), new []{ ProgressionEvent.PlayerMadeTouchdown});
            var domainResult = match.Finish(new []{ playerProgression, playerProgression2 });

            Assert.IsTrue(match.IsFinished);
            var domainEvent = domainResult.DomainEvents.First() as MatchFinished;
            Assert.AreEqual(trainerAtHome, domainEvent.GameResult.WinnerId);
            Assert.AreEqual(trainerAsGuest, domainEvent.GameResult.LooserId);
        }
    }
}