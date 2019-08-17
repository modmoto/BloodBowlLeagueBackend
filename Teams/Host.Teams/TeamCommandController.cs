using System;
using System.Threading.Tasks;
using Application.Teams;
using Microsoft.AspNetCore.Mvc;
using Microwave.Domain.Identities;

namespace Teams.WriteHost
{
    [Route("api/teams")]
    public class TeamController : Controller
    {
        private readonly TeamCommandHandler _commandHandler;

        public TeamController(TeamCommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateTeam([FromBody] CreateTeamCommand createTeamCommand)
        {
            var teamId = await _commandHandler.CreateTeam(createTeamCommand);
            return Created($"{Request.Scheme}://{Request.Host}/Api/Teams/{teamId}", teamId);
        }

        [HttpPost("{teamId}/buy-player")]
        public async Task<ActionResult> BuyPlayer(GuidIdentity teamId, [FromBody] BuyPlayerCommand buyPlayerCommand)
        {
            buyPlayerCommand.TeamId = teamId;
            var playerId = await _commandHandler.BuyPlayer(buyPlayerCommand);
            return Ok(playerId);
        }

        [HttpPost("{teamId}/remove-player")]
        public async Task<ActionResult> RemovePlayer(GuidIdentity teamId, [FromBody] RemovePlayerCommand buyPlayerCommand)
        {
            buyPlayerCommand.TeamId = teamId;
            await _commandHandler.RemovePlayer(buyPlayerCommand);
            return Ok();
        }


        [HttpPost("{teamId}/finish")]
        public async Task<ActionResult> FinishTeam([FromBody] FinishTeamCommand finishTeamCommand)
        {
            await _commandHandler.FinishTeam(finishTeamCommand);
            return Ok();
        }
    }
}