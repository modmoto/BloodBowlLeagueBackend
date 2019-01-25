using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Matches;
using Domain.Matches.Events;
using Microsoft.AspNetCore.Mvc;
using Microwave.Domain;

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
            await _commandHandler.CreateMatch(command);
            return Ok();
        }

        [HttpPost("{matchId}/finish")]
        public async Task<ActionResult> FinishMatch(GuidIdentity guidIdentity, [FromBody] IEnumerable<PlayerProgression> progressions)
        {
            var finishMatchCommand = new FinishMatchCommand(guidIdentity, progressions);
            await _commandHandler.FinishMatch(finishMatchCommand);
            return Ok();
        }
    }
}