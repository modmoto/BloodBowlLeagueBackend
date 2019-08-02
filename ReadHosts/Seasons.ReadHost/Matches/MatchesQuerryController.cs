using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Seasons.ReadHost.Matches
{
    [Route("api/matches")]
    public class MatchesQuerryController : Controller
    {
        private readonly IReadModelRepository _queryRepository;

        public MatchesQuerryController(IReadModelRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        [HttpGet("{matchupId}")]
        public async Task<ActionResult> GetSeason(GuidIdentity matchupId)
        {
            var teamQuerry = await _queryRepository.Load<MatchupReadModel>(matchupId);
            return Ok(teamQuerry.Value);
        }

        [HttpGet]
        public async Task<ActionResult> GetSeasons()
        {
            var teamQuerry = await _queryRepository.LoadAll<MatchupReadModel>();
            return Ok(teamQuerry.Value);
        }
    }
}