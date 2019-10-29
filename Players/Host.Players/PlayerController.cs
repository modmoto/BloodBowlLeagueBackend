using Application.Players;

namespace Host.Players
{
    [Route("api/players")]
    public class PlayerController : Controller
    {
        private readonly PlayerCommandHandler _commandHandler;

        public PlayerController(PlayerCommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        [HttpPost("{playerId}/level-up")]
        public async Task<ActionResult> LevelUpPlayer(
            Guid playerId,
            [FromBody] LevelUpPlayerComand levelUpCommand)
        {
            await _commandHandler.LevelUp(playerId, levelUpCommand);
            return Ok();
        }
    }
}