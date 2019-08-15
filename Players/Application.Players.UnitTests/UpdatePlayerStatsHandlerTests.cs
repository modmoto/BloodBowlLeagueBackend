using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Players;
using Domain.Players.Events.ForeignEvents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Exceptions;
using Microwave.Domain.Identities;
using Microwave.Domain.Results;
using Microwave.EventStores;
using Moq;

namespace Application.Players.UnitTests
{
    [TestClass]
    public class UpdatePlayerStatsHandlerTests
    {
        private Mock<IEventStore> _eventStore;

        [TestInitialize]
        public void Setup()
        {
            _eventStore = new Mock<IEventStore>();
            _eventStore.Setup(es => es.AppendAsync(It.IsAny<IEnumerable<IDomainEvent>>(), It.IsAny<long>()))
                        .ReturnsAsync(Result.Ok());
        }

        [TestMethod]
        public async Task UploadNewStatEvent_HappyPath()
        {
            var identity = GuidIdentity.Create(Guid.NewGuid());
            _eventStore.Setup(es => es.LoadAsync<Player>(identity))
                .ReturnsAsync(EventStoreResult<Player>.Ok(new Player(), 0));

            var onMatchUploadedUpdatePlayerProgress = new OnMatchFinishedUpdatePlayerProgress(_eventStore.Object);
            var matchResultUploaded = MatchResultUploaded(identity);

            await onMatchUploadedUpdatePlayerProgress.HandleAsync(matchResultUploaded);
            _eventStore.Verify(m => m.AppendAsync(It.IsAny<IEnumerable<IDomainEvent>>(), 0), Times.Exactly(2));
        }

        [TestMethod]
        public async Task UploadNewStatEvent_PlayerNotFound()
        {
            _eventStore.Setup(es => es.LoadAsync<Player>(It.IsAny<GuidIdentity>()))
                .ReturnsAsync(EventStoreResult<Player>.NotFound(GuidIdentity.Create()));

            var onMatchUploadedUpdatePlayerProgress = new OnMatchFinishedUpdatePlayerProgress(_eventStore.Object);
            var matchResultUploaded = MatchResultUploaded(GuidIdentity.Create(Guid.NewGuid()));

            await Assert.ThrowsExceptionAsync<NotFoundException>(
                async () => await onMatchUploadedUpdatePlayerProgress.HandleAsync(matchResultUploaded));
        }

        [TestMethod]
        public async Task UploadNewStatEvent_SecondPlayerNotFound()
        {
            var idNotFound = GuidIdentity.Create();
            var idFound = GuidIdentity.Create();
            _eventStore.Setup(es => es.LoadAsync<Player>(idFound))
                .ReturnsAsync(EventStoreResult<Player>.Ok(new Player(), 0));
            _eventStore.Setup(es => es.LoadAsync<Player>(idNotFound))
                .ReturnsAsync(EventStoreResult<Player>.NotFound(GuidIdentity.Create()));

            var onMatchUploadedUpdatePlayerProgress = new OnMatchFinishedUpdatePlayerProgress(_eventStore.Object);

            var matchResultUploaded = MatchResultUploaded(idFound, idNotFound);

            await Assert.ThrowsExceptionAsync<NotFoundException>(
                async () => await onMatchUploadedUpdatePlayerProgress.HandleAsync(matchResultUploaded));
        }

        private static MatchFinished MatchResultUploaded(params GuidIdentity[] identity)
        {
            var progressions1 = identity.Select(guidIdentity => new PlayerProgression(
                guidIdentity,
                ProgressionEvent.PlayerPassed)).ToList();
            var progressions2 = identity.Select(guidIdentity => new PlayerProgression(
                guidIdentity,
                ProgressionEvent.PlayerMadeTouchdown));
            progressions1.AddRange(progressions2);
            return new MatchFinished(GuidIdentity.Create(), progressions1);
        }
    }
}