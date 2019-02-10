using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microwave.Domain;
using Microwave.Queries;

namespace Teams.ReadHost.FullTeams
{
    [Route("api/teams")]
    public class TeamFullController : Controller
    {
        private readonly IReadModelRepository _queryRepository;

        public TeamFullController(IReadModelRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        [HttpGet("{teamId}/full")]
        public async Task<ActionResult> GetTeam(GuidIdentity teamId)
        {
            var teamQuerry = await _queryRepository.Load<TeamReadModelFull>(teamId);
            return Ok(teamQuerry);
        }
    }
}