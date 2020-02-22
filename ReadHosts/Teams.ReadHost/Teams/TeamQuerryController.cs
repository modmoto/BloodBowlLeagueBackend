using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> GetTeam(Guid teamId)
        {
            var teamQuerry = await _queryRepository.LoadAsync<TeamReadModel>(teamId);
            return Ok(teamQuerry.Value);
        }
    }
}