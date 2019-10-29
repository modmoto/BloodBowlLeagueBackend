using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Players;
using Domain.Players.Events.ForeignEvents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Exceptions;
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
            var identity = Guid.NewGuid();
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
            _eventStore.Setup(es => es.LoadAsync<Player>(It.IsAny<Guid>()))
                .ReturnsAsync(EventStoreResult<Player>.NotFound(Guid.NewGuid().ToString()));

            var onMatchUploadedUpdatePlayerProgress = new OnMatchFinishedUpdatePlayerProgress(_eventStore.Object);
            var matchResultUploaded = MatchResultUploaded(Guid.NewGuid());

            await Assert.ThrowsExceptionAsync<NotFoundException>(
                async () => await onMatchUploadedUpdatePlayerProgress.HandleAsync(matchResultUploaded));
        }

        [TestMethod]
        public async Task UploadNewStatEvent_SecondPlayerNotFound()
        {
            var idNotFound = Guid.NewGuid();
            var idFound = Guid.NewGuid();
            _eventStore.Setup(es => es.LoadAsync<Player>(idFound))
                .ReturnsAsync(EventStoreResult<Player>.Ok(new Player(), 0));
            _eventStore.Setup(es => es.LoadAsync<Player>(idNotFound))
                .ReturnsAsync(EventStoreResult<Player>.NotFound(Guid.NewGuid()));

            var onMatchUploadedUpdatePlayerProgress = new OnMatchFinishedUpdatePlayerProgress(_eventStore.Object);

            var matchResultUploaded = MatchResultUploaded(idFound, idNotFound);

            await Assert.ThrowsExceptionAsync<NotFoundException>(
                async () => await onMatchUploadedUpdatePlayerProgress.HandleAsync(matchResultUploaded));
        }

        private static MatchFinished MatchResultUploaded(params Guid[] identity)
        {
            var progressions1 = identity.Select(guidIdentity => new PlayerProgression(
                guidIdentity,
                ProgressionEvent.PlayerPassed)).ToList();
            var progressions2 = identity.Select(guidIdentity => new PlayerProgression(
                guidIdentity,
                ProgressionEvent.PlayerMadeTouchdown));
            progressions1.AddRange(progressions2);
            return new MatchFinished(Guid.NewGuid(), progressions1);
        }
    }
}