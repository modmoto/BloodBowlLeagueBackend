using System;
using System.Threading.Tasks;
using Application.Matches;
using Microsoft.AspNetCore.Mvc;
using Microwave;
using Microwave.WebApi;

namespace Host.Matches
{
    [Route("api/matches")]
    public class MatchController : Controller
    {
        private readonly MatchCommandHandler _commandHandler;
        private readonly DiscoveryPoller _discoveryPoller;
        private readonly AsyncEventDelegator _eventDelegator;

        public MatchController(
            MatchCommandHandler commandHandler,
            DiscoveryPoller discoveryPoller,
            AsyncEventDelegator eventDelegator)
        {
            _commandHandler = commandHandler;
            _discoveryPoller = discoveryPoller;
            _eventDelegator = eventDelegator;
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

        [HttpGet("startpoll")]
        public ActionResult startall()
        {
            _eventDelegator.StartEventPolling();
            return Ok();
        }

        [HttpGet("startDisco")]
        public ActionResult startSisc()
        {
            _discoveryPoller.StartDependencyDiscovery();
            return Ok();
        }

        [HttpGet("gettasks")]
        public ActionResult gettasks()
        {
            return Ok(_eventDelegator.Tasks);
        }

        [HttpGet("task")]
        public ActionResult task()
        {
            return Ok(_discoveryPoller.PollTask);
        }
    }
}