namespace Teams.ReadHost.Players
{
    [Route("api/players")]
    public class PlayerQuerryController : Controller
    {
        private readonly IReadModelRepository _queryRepository;

        public PlayerQuerryController(IReadModelRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        [HttpGet("{playerId}")]
        public async Task<ActionResult> GetPlayer(Guid playerId)
        {
            var player = await _queryRepository.LoadAsync<PlayerReadModel>(playerId);
            return Ok(player.Value);
        }
    }
}