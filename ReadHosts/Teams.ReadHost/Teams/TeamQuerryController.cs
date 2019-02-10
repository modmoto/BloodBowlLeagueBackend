using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microwave.Domain;
using Microwave.Queries;

namespace Teams.ReadHost.Teams
{
    [Route("api/teams")]
    public class TeamQuerryController : Controller
    {
        private readonly IReadModelRepository _queryRepository;

        public TeamQuerryController(IReadModelRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        [HttpGet("{teamId}")]
        public async Task<ActionResult> GetTeam(GuidIdentity teamId)
        {
            var teamQuerry = await _queryRepository.Load<TeamReadModel>(teamId);
            return Ok(teamQuerry);
        }

        [HttpGet]
        public async Task<ActionResult> GetTeams()
        {
            var teamQuerry = await _queryRepository.LoadAll<TeamOverviewReadModel>();
            return Ok(teamQuerry.Value);
        }
    }
}