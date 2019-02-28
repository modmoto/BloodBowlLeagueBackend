using System.Linq;
using System.Threading.Tasks;
using Microwave.Application.Results;
using Microwave.Queries;
using Teams.ReadHost.Players;
using Teams.ReadHost.Teams;

namespace Teams.ReadHost.FullTeams
{
    public class OnPlayerUpdatesUpdateFullTeamReadModel :
        ReadModelMerge<PlayerReadModel>,
        ReadModelMerge<TeamReadModel>
    {
        private readonly IReadModelRepository _readModelRepository;

        public OnPlayerUpdatesUpdateFullTeamReadModel(IReadModelRepository readModelRepository)
        {
            _readModelRepository = readModelRepository;
        }

        public async Task Merge(PlayerReadModel mergeUnit)
        {
            var teamReadModelResult = await _readModelRepository.Load<TeamReadModelFull>(mergeUnit.TeamId);
            var teamReadModelFull = teamReadModelResult.Value;
            var playerReadModels = teamReadModelFull.PlayerList.ToList();
            var playerReadModel = playerReadModels.FirstOrDefault(p => p.PlayerId == mergeUnit.PlayerId);
            if (playerReadModel == null)
            {
                playerReadModels.Add(mergeUnit);
            }
            else
            {
                playerReadModels.Remove(playerReadModel);
                playerReadModels.Add(mergeUnit);
            }

            teamReadModelFull.PlayerList = playerReadModels;
        }

        public async Task Merge(TeamReadModel mergeUnit)
        {
            var teamReadModelFull = await _readModelRepository.Load<TeamReadModelFull>(mergeUnit.TeamId);

            teamReadModelFull.Value.Team = mergeUnit;
        }
    }

    public interface ReadModelMerge<T> where T : ReadModel
    {
        Task Merge(T mergeUnit);
    }
}