using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microwave.Queries;
using Querries.Teams;

namespace QuerryHost.Teams
{
    [Route("api/races")]
    public class RaceQuerryController : Controller
    {
        private readonly IReadModelRepository _queryRepository;

        public RaceQuerryController(IReadModelRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        [HttpGet("{raceId}")]
        public async Task<ActionResult> GetTeam(Guid raceId)
        {
            var teamQuerry = await _queryRepository.Load<RaceReadModel>(raceId);
            return Ok(teamQuerry.Value);
        }
    }
}