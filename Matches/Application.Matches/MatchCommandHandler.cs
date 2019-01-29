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
            var eventStoreResult = await _eventStore.LoadAsync<Match>(command.MatchId);
            var match = eventStoreResult.Value;
            var domainResult = match.Finish(command.PlayerProgressions);
            domainResult.EnsureSucces();
            var storeResult = await _eventStore.AppendAsync(domainResult.DomainEvents, eventStoreResult.Version);
            storeResult.Check();
        }

        public async Task StartMatch(StartMatchCommand command)
        {
            var eventStoreResult = await _eventStore.LoadAsync<Match>(command.MatchId);
            var match = eventStoreResult.Value;
            var homeTeam = (await _readModelRepository.Load<TeamReadModel>(match.TeamAtHome)).Value;
            var guestTeam = (await _readModelRepository.Load<TeamReadModel>(match.TeamAsGuest)).Value;

            var domainResult = match.Start(homeTeam, guestTeam);
            domainResult.EnsureSucces();
            var storeResult = await _eventStore.AppendAsync(domainResult.DomainEvents, eventStoreResult.Version);
            storeResult.Check();
        }

        public async Task CreateMatches(CreateMatchCommand command)
        {
            var homeTeam = (await _readModelRepository.Load<TeamReadModel>(command.HomeTeam)).Value;
            var guestTeam = (await _readModelRepository.Load<TeamReadModel>(command.GuestTeam)).Value;
            var domainResult = Match.Create(homeTeam.TeamId, guestTeam.TeamId);
            var storeResult = await _eventStore.AppendAsync(domainResult.DomainEvents, 0);
            storeResult.Check();
        }
    }

    public class StartMatchCommand
    {
        public StartMatchCommand(GuidIdentity matchId)
        {
            MatchId = matchId;
        }

        public GuidIdentity MatchId { get; }
    }

    public class CreateMatchCommand
    {
        public CreateMatchCommand(GuidIdentity homeTeam, GuidIdentity guestTeam)
        {
            HomeTeam = homeTeam;
            GuestTeam = guestTeam;
        }

        public GuidIdentity HomeTeam { get; }
        public GuidIdentity GuestTeam { get; }
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