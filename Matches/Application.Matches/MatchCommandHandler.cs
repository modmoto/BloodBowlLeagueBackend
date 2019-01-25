using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Matches;
using Domain.Matches.Events;
using Microwave.Domain;
using Microwave.EventStores.Ports;

namespace Application.Matches
{
    public class MatchCommandHandler
    {
        private readonly IEventStore _eventStore;

        public MatchCommandHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task FinishMatch(FinishMatchCommand command)
        {
            var result = (await _eventStore.LoadAsync<Match>(command.MatchId)).Value;
            var match = result.Entity;
            var domainResult = match.Finish(command.PlayerProgressions);
            domainResult.EnsureSucces();
            var storeResult = await _eventStore.AppendAsync(domainResult.DomainEvents, result.Version);
            storeResult.Check();
        }

        public async Task CreateMatch(CreateMatchCommand command)
        {
            var domainResult = Match.Create(command.TrainerAtHome, command.TrainerAsGuest);
            var storeResult = await _eventStore.AppendAsync(domainResult.DomainEvents, 0);
            storeResult.Check();
        }
    }

    public class CreateMatchCommand
    {
        public GuidIdentity TrainerAtHome { get; set; }
        public GuidIdentity TrainerAsGuest { get; set; }
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