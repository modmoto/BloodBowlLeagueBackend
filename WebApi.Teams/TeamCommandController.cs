using System;
using System.Threading.Tasks;
using Application.Teams;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Teams
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
            var teamGuid = await _commandHandler.CreateTeam(createTeamCommand);
            return Created($"Api/Teams/{teamGuid}", null);
        }

        [HttpPost("{teamId}/buyPlayer")]
        public async Task<ActionResult> BuyPlayer(Guid teamId, [FromBody] BuyPlayerCommand createTeamCommand)
        {
            createTeamCommand.TeamId = teamId;
            await _commandHandler.BuyPlayer(createTeamCommand);
            return Ok();
        }
    }
}