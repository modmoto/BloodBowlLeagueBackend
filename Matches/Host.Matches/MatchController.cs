using Application.Matches;

namespace Host.Matches
{
    [Route("api/matches")]
    public class MatchController : Controller
    {
        private readonly MatchCommandHandler _commandHandler;

        public MatchController(MatchCommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateMatch([FromBody] CreateMatchCommand command)
        {
            var matchId = await _commandHandler.CreateMatch(command);
            return Created($"{Request.Scheme}://{Request.Host}/Api/Matches/{matchId}", matchId);
        }

        [HttpPost("{matchId}/finish")]
        public async Task<ActionResult> FinishMatch(Guid matchId, [FromBody] FinishMatchCommand finishMatchCommand)
        {
            finishMatchCommand.MatchId = matchId;
            await _commandHandler.FinishMatch(finishMatchCommand);
            return Ok();
        }

        [HttpPost("{matchId}/progress")]
        public async Task<ActionResult> FinishMatch(Guid matchId, [FromBody] ProgressMatchCommand progressMatchCommand)
        {
            progressMatchCommand.MatchId = matchId;
            await _commandHandler.ProgressMatch(progressMatchCommand);
            return Ok();
        }

        [HttpPost("{matchId}/start")]
        public async Task<ActionResult> StartMatch(Guid matchId)
        {
            var startMatchCommand = new StartMatchCommand(matchId);
            await _commandHandler.StartMatch(startMatchCommand);
            return Ok();
        }
    }
}