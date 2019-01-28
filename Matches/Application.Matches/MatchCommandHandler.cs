using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Matches;
using Domain.Matches.Events;
using Microwave.Domain;
using Microwave.EventStores.Ports;
using Microwave.Queries;

namespace Application.Matches
{
    public class MatchCommandHandler
    {
        private readonly IEventStore _eventStore;
        private readonly IReadModelRepository _readModelRepository;

        public MatchCommandHandler(IEventStore eventStore, IReadModelRepository readModelRepository)
        {
            _eventStore = eventStore;
            _readModelRepository = readModelRepository;
        }

        public async Task FinishMatch(FinishMatchCommand command)
        {
            var result = (await _eventStore.LoadAsync<Match>(command.MatchId));
            var match = result.Entity;
            var domainResult = match.Finish(command.PlayerProgressions);
            domainResult.EnsureSucces();
            var storeResult = await _eventStore.AppendAsync(domainResult.DomainEvents, result.Version);
            storeResult.Check();
        }

        public async Task CreateMatch(CreateMatchCommand command)
        {
            var homeTeam = (await _readModelRepository.Load<TeamReadModel>(command.HomeTeam)).ReadModel;
            var guestTeam = (await _readModelRepository.Load<TeamReadModel>(command.GuestTeam)).ReadModel;
            var domainResult = Match.Create(homeTeam, guestTeam);
            var storeResult = await _eventStore.AppendAsync(domainResult.DomainEvents, 0);
            storeResult.Check();
        }
    }

    public class CreateMatchCommand
    {
        public GuidIdentity HomeTeam { get; set; }
        public GuidIdentity GuestTeam { get; set; }
    }

    public class FinishMatchCommand
    {
        public FinishMatchCommand(GuidIdentity matchId, IEnumerable<PlayerProgression> playerProgressions)
        {
            MatchId = matchId;
            PlayerProgressions = playerProgressions;
        }

        public GuidIdentity MatchId { get; }
        public IEnumerable<PlayerProgression> PlayerProgressions { get; }
    }
}