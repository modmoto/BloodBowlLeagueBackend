using System.Linq;
using System.Threading.Tasks;
using Domain.Teams;
using Domain.Teams.DomainEvents;
using Microwave.Domain.Identities;
using Microwave.EventStores.Ports;

namespace Application.Teams
{
    public class TeamCommandHandler
    {
        private readonly IEventStore _eventStore;

        public TeamCommandHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Identity> CreateTeam(CreateTeamCommand createTeamCommand)
        {
            var readModelResult = await _eventStore.LoadAsync<RaceConfig>(createTeamCommand.RaceId);
            var race = readModelResult.Value;
            var domainResult = Team.Create(race.Id, createTeamCommand.TeamName, createTeamCommand.TrainerName, race.AllowedPlayers);
            await _eventStore.AppendAsync(domainResult.DomainEvents, 0);
            return domainResult.DomainEvents.First().EntityId;
        }

        public async Task<Identity> BuyPlayer(BuyPlayerCommand buyPlayerCommand)
        {
            var teamResult = await _eventStore.LoadAsync<Team>(buyPlayerCommand.TeamId);
            var team = teamResult.Value;
            var buyPlayer = team.BuyPlayer(buyPlayerCommand.PlayerTypeId);
            (await _eventStore.AppendAsync(buyPlayer.DomainEvents, buyPlayerCommand.TeamVersion)).Check();
            return (buyPlayer.DomainEvents.First() as PlayerBought)?.PlayerId;
        }
    }

    public class BuyPlayerCommand
    {
        public GuidIdentity TeamId { get; set; }
        public StringIdentity PlayerTypeId { get; set; }
        public long TeamVersion { get; set; }
    }

    public class CreateTeamCommand
    {
        public string TrainerName { get; set; }
        public string TeamName { get; set; }
        public StringIdentity RaceId { get; set; }
    }
}