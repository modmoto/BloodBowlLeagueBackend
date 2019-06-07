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

        [HttpPost("{teamId}/buyPlayer")]
        public async Task<ActionResult> BuyPlayer(Guid teamId, [FromBody] BuyPlayerCommand buyPlayerCommand)
        {
            buyPlayerCommand.TeamId = GuidIdentity.Create(teamId);
            var playerId = await _commandHandler.BuyPlayer(buyPlayerCommand);
            return Ok(playerId);
        }
    }
}