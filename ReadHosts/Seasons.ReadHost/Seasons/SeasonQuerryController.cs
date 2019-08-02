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

        [HttpGet("{seasonId}")]
        public async Task<ActionResult> GetSeason(GuidIdentity seasonId)
        {
            var teamQuerry = await _queryRepository.Load<SeasonReadModel>(seasonId);
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