using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microwave.Domain.Identities;
using Microwave.Queries;

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
        public async Task<ActionResult> GetPlayer(GuidIdentity playerId)
        {
            var player = await _queryRepository.LoadAsync<PlayerReadModel>(playerId);
            return Ok(player.Value);
        }
    }
}