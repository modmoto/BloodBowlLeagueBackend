using System;
using System.Threading.Tasks;
using Domain.Matches;
using Domain.Matches.Events;
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
            var eventStoreResult = await _eventStore.LoadAsync<Matchup>(command.MatchId);
            var match = eventStoreResult.Value;
            var domainResult = match.Finish();
            domainResult.EnsureSucces();
            var storeResult = await _eventStore.AppendAsync(domainResult.DomainEvents, eventStoreResult.Version);
            storeResult.Check();
        }

        public async Task StartMatch(StartMatchCommand command)
        {
            var eventStoreResult = await _eventStore.LoadAsync<Matchup>(command.MatchId);
            var match = eventStoreResult.Value;
            var homeTeam = (await _readModelRepository.LoadAsync<TeamReadModel>(match.TeamAtHome)).Value;
            var guestTeam = (await _readModelRepository.LoadAsync<TeamReadModel>(match.TeamAsGuest)).Value;

            var domainResult = match.Start(homeTeam, guestTeam);
            var storeResult = await _eventStore.AppendAsync(domainResult.DomainEvents, eventStoreResult.Version);
            storeResult.Check();
        }

        public async Task<string> CreateMatch(CreateMatchCommand command)
        {
            var homeTeam = (await _readModelRepository.LoadAsync<TeamReadModel>(command.HomeTeam)).Value;
            var guestTeam = (await _readModelRepository.LoadAsync<TeamReadModel>(command.GuestTeam)).Value;
            var domainResult = Matchup.Create(homeTeam, guestTeam);
            var storeResult = await _eventStore.AppendAsync(domainResult.DomainEvents, 0);
            storeResult.Check();
            return domainResult.DomainEvents.Single().EntityId;
        }

        public async Task ProgressMatch(ProgressMatchCommand command)
        {
            var eventStoreResult = await _eventStore.LoadAsync<Matchup>(command.MatchId);
            var match = eventStoreResult.Value;
            var domainResult = match.ProgressMatch(command.PlayerProgression);
            domainResult.EnsureSucces();
            var storeResult = await _eventStore.AppendAsync(domainResult.DomainEvents, eventStoreResult.Version);
            storeResult.Check();
        }
    }

    public class StartMatchCommand
    {
        public StartMatchCommand(Guid matchId)
        {
            MatchId = matchId;
        }

        public Guid MatchId { get; }
    }

    public class CreateMatchCommand
    {
        public CreateMatchCommand(Guid homeTeam, Guid guestTeam)
        {
            HomeTeam = homeTeam;
            GuestTeam = guestTeam;
        }

        public Guid HomeTeam { get; }
        public Guid GuestTeam { get; }
    }

    public class FinishMatchCommand
    {
        public Guid MatchId { get; set; }
    }

    public class ProgressMatchCommand
    {
        public Guid MatchId { get; set; }

        public PlayerProgression PlayerProgression { get; set; }
    }
}