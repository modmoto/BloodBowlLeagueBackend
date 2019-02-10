using System.Linq;
using System.Threading.Tasks;
using Microwave.Domain;
using Microwave.Queries;
using Teams.ReadHost.Players;
using Teams.ReadHost.Players.Events;
using Teams.ReadHost.Teams;

namespace Teams.ReadHost.FullTeams
{
    public class OnPlayerUpdatesUpdateFullTeamReadModel :
        IHandleAsync<PlayerCreated>,
        IHandleAsync<SkillChosen>,
        IHandleAsync<PlayerLeveledUp>
    {
        private readonly IReadModelRepository _readModelRepository;

        public OnPlayerUpdatesUpdateFullTeamReadModel(IReadModelRepository readModelRepository)
        {
            _readModelRepository = readModelRepository;
        }

        private async Task UpdateTeamReadModelFull(IDomainEvent domainEvent)
        {
            var player = (await _readModelRepository.Load<PlayerReadModel>(domainEvent.EntityId)).Value;
            var teams = await _readModelRepository.LoadAll<TeamReadModel>();

            var teamWithPlayer = teams.Value.Single(team =>
                team.PlayerList.Count(p => p.PlayerId == domainEvent.EntityId) == 1);
            var teamId = teamWithPlayer.TeamId;
            var teamsFull = (await _readModelRepository.Load<TeamReadModelFull>(teamId)).Value;
            teamsFull.PlayerList = teamsFull.PlayerList.Append(player);

            (await _readModelRepository.Save(new ReadModelResult<TeamReadModelFull>(teamsFull, teamId, 0))).Check();
        }

        public async Task HandleAsync(PlayerCreated domainEvent)
        {
            await UpdateTeamReadModelFull(domainEvent);
        }

        public async Task HandleAsync(SkillChosen domainEvent)
        {
            await UpdateTeamReadModelFull(domainEvent);
        }

        public async Task HandleAsync(PlayerLeveledUp domainEvent)
        {
            await UpdateTeamReadModelFull(domainEvent);
        }
    }
}