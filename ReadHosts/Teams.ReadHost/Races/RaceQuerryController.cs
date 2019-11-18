using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microwave.Queries;

namespace Teams.ReadHost.Races
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
        public async Task<ActionResult> GetRace(string raceId)
        {
            var teamQuerry = await _queryRepository.LoadAsync<RaceReadModel>(raceId);
            return Ok(teamQuerry.Value);
        }

        [HttpGet]
        public async Task<ActionResult> GetRaces()
        {
            var teamQuerry = await _queryRepository.LoadAllAsync<RaceReadModel>();
            return Ok(teamQuerry.Value);
        }
    }
}