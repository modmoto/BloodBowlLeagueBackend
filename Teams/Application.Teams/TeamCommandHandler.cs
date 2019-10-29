using System;
using System.Threading.Tasks;
using Domain.Teams;
using Domain.Teams.DomainEvents;
using Microwave.EventStores.Ports;
using Microwave.Queries;

namespace Application.Teams
{
    public class TeamCommandHandler
    {
        private readonly IEventStore _eventStore;
        private readonly IReadModelRepository _readModelRepository;

        public TeamCommandHandler(IEventStore eventStore, IReadModelRepository readModelRepository)
        {
            _eventStore = eventStore;
            _readModelRepository = readModelRepository;
        }

        public async Task<string> CreateTeam(CreateTeamCommand createTeamCommand)
        {
            var readModelResult = await _readModelRepository.LoadAsync<RaceReadModel>(createTeamCommand.RaceId);
            var race = readModelResult.Value;
            var domainResult = Team.Draft(
                race.RaceConfigId,
                createTeamCommand.TeamName,
                createTeamCommand.TrainerName, race.AllowedPlayers);
            await _eventStore.AppendAsync(domainResult.DomainEvents, 0);
            return domainResult.DomainEvents.First().EntityId;
        }

        public async Task<string> BuyPlayer(BuyPlayerCommand buyPlayerCommand)
        {
            var teamResult = await _eventStore.LoadAsync<Team>(buyPlayerCommand.TeamId);
            var team = teamResult.Value;
            var buyPlayer = team.BuyPlayer(buyPlayerCommand.PlayerTypeId);
            (await _eventStore.AppendAsync(buyPlayer.DomainEvents, buyPlayerCommand.TeamVersion)).Check();
            return (buyPlayer.DomainEvents.First() as PlayerBought)?.PlayerId;
        }

        public async Task FinishTeam(FinishTeamCommand command)
        {
            var teamResult = await _eventStore.LoadAsync<Team>(command.TeamId);
            var team = teamResult.Value;
            var finishTeam = team.CommitDraft();
            (await _eventStore.AppendAsync(finishTeam.DomainEvents, command.TeamVersion)).Check();
        }

        public async Task RemovePlayer(RemovePlayerCommand removePlayerCommand)
        {
            var teamResult = await _eventStore.LoadAsync<Team>(removePlayerCommand.TeamId);
            var team = teamResult.Value;
            var removePlayer = team.RemovePlayer(removePlayerCommand.PlayerId);
            (await _eventStore.AppendAsync(removePlayer.DomainEvents, removePlayerCommand.TeamVersion)).Check();
        }
    }

    public class BuyPlayerCommand
    {
        public Guid TeamId { get; set; }
        public string PlayerTypeId { get; set; }
        public long TeamVersion { get; set; }
    }

    public class RemovePlayerCommand
    {
        public Guid TeamId { get; set; }
        public Guid PlayerId { get; set; }
        public long TeamVersion { get; set; }
    }

    public class FinishTeamCommand
    {
        public Guid TeamId { get; set; }
        public long TeamVersion { get; set; }
    }

    public class CreateTeamCommand
    {
        public string TrainerName { get; set; }
        public string TeamName { get; set; }
        public string RaceId { get; set; }
    }
}