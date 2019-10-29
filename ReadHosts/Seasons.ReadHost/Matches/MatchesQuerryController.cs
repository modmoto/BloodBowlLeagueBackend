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
        public async Task<ActionResult> GetSeason(Guid matchupId)
        {
            var teamQuerry = await _queryRepository.LoadAsync<MatchupReadModel>(matchupId);
            return Ok(teamQuerry.Value);
        }

        [HttpGet]
        public async Task<ActionResult> GetSeasons()
        {
            var teamQuerry = await _queryRepository.LoadAllAsync<MatchupReadModel>();
            return Ok(teamQuerry.Value);
        }
    }
}