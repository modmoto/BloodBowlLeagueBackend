using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Seasons.ReadHost.Seasons
{
    [Route("api/seasons")]
    public class SeasonQuerryController : Controller
    {
        private readonly IReadModelRepository _queryRepository;

        public SeasonQuerryController(IReadModelRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        [HttpGet("{teamId}")]
        public async Task<ActionResult> GetSeason(GuidIdentity teamId)
        {
            var teamQuerry = await _queryRepository.Load<SeasonReadModel>(teamId);
            return Ok(teamQuerry.Value);
        }

        [HttpGet]
        public async Task<ActionResult> GetSeasons()
        {
            var teamQuerry = await _queryRepository.LoadAll<SeasonReadModel>();
            return Ok(teamQuerry.Value);
        }
    }
}