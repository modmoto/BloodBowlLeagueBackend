using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microwave.Queries;
using Querries.Teams;

namespace QuerryHost.Teams
{
    [Route("api/teams")]
    public class TeamQuerryController : Controller
    {
        private readonly IQeryRepository _queryRepository;

        public TeamQuerryController(IQeryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        [HttpGet("{teamId}")]
        public async Task<ActionResult> GetTeam(Guid teamId)
        {
            var teamQuerry = await _queryRepository.Load<TeamReadModel>(teamId);
            return Ok(teamQuerry.Value);
        }
    }
}