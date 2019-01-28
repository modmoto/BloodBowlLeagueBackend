using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Players;
using Domain.Players.Events.ForeignEvents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microwave.Application.Exceptions;
using Microwave.Application.Results;
using Microwave.Domain;
using Microwave.EventStores.Ports;
using Moq;

namespace Application.Players.UnitTests
{
    [TestClass]
    public class UpdatePlayerStatsHandlerTests
    {
        [TestMethod]
        public async Task UploadNewStatEvent_HappyPath()
        {
            var mock = new Mock<IEventStore>();
            var identity = GuidIdentity.Create(Guid.NewGuid());
            mock.Setup(es => es.LoadAsync<Player>(identity))
                .ReturnsAsync(EventStoreResult<Player>.Ok(new Player(), 0));

            var onMatchUploadedUpdatePlayerProgress = new OnMatchFinishedUpdatePlayerProgress(mock.Object);
            var matchResultUploaded = MatchResultUploaded(identity);

            await onMatchUploadedUpdatePlayerProgress.HandleAsync(matchResultUploaded);
            mock.Verify(m => m.AppendAsync(It.IsAny<IEnumerable<IDomainEvent>>(), 0), Times.Once);
        }

        [TestMethod]
        public async Task UploadNewStatEvent_PlayerNotFound()
        {
            var mock = new Mock<IEventStore>();
            mock.Setup(es => es.LoadAsync<Player>(It.IsAny<GuidIdentity>()))
                .ReturnsAsync(EventStoreResult<Player>.NotFound(GuidIdentity.Create()));

            var onMatchUploadedUpdatePlayerProgress = new OnMatchFinishedUpdatePlayerProgress(mock.Object);
            var matchResultUploaded = MatchResultUploaded(GuidIdentity.Create(Guid.NewGuid()));

            await Assert.ThrowsExceptionAsync<NotFoundException>(
                async () => await onMatchUploadedUpdatePlayerProgress.HandleAsync(matchResultUploaded));
        }

        [TestMethod]
        public async Task UploadNewStatEvent_SecondPlayerNotFound()
        {
            var mock = new Mock<IEventStore>();
            var idNotFound = GuidIdentity.Create();
            var idFound = GuidIdentity.Create();
            mock.Setup(es => es.LoadAsync<Player>(idFound))
                .ReturnsAsync(EventStoreResult<Player>.Ok(new Player(), 0));
            mock.Setup(es => es.LoadAsync<Player>(idNotFound))
                .ReturnsAsync(EventStoreResult<Player>.NotFound(GuidIdentity.Create()));
            var onMatchUploadedUpdatePlayerProgress = new OnMatchFinishedUpdatePlayerProgress(mock.Object);

            var matchResultUploaded = MatchResultUploaded(idFound, idNotFound);

            await Assert.ThrowsExceptionAsync<NotFoundException>(
                async () => await onMatchUploadedUpdatePlayerProgress.HandleAsync(matchResultUploaded));
        }

        private static MatchFinished MatchResultUploaded(params GuidIdentity[] identity)
        {
            var progressionEvents = new []{ ProgressionEvent.PlayerPassed, ProgressionEvent.PlayerMadeCasualty };
            var progressions = identity.Select(guidIdentity => new PlayerProgression(guidIdentity, progressionEvents));
            return new MatchFinished(GuidIdentity.Create(Guid.NewGuid()), progressions);
        }
    }
}