using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> GetSeason(Guid seasonId)
        {
            var teamQuerry = await _queryRepository.LoadAsync<SeasonReadModel>(seasonId);
            return Ok(teamQuerry.Value);
        }

        [HttpGet]
        public async Task<ActionResult> GetSeasons()
        {
            var teamQuerry = await _queryRepository.LoadAllAsync<SeasonReadModel>();
            return Ok(teamQuerry.Value);
        }
    }
}