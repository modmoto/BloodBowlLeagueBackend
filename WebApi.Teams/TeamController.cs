using System;
using System.Threading.Tasks;
using Application.Teams;
using Domain.Teams;
using Microsoft.AspNetCore.Mvc;
using Microwave.Application.Ports;
using Microwave.Queries;
using Querries.Teams;

namespace WebApi.Teams
{
    [Route("api/teams")]
    public class TeamController : Controller
    {
        private readonly TeamCommandHandler _commandHandler;
        private readonly IQeryRepository _queryRepository;

        public TeamController(TeamCommandHandler commandHandler, IQeryRepository queryRepository)
        {
            _commandHandler = commandHandler;
            _queryRepository = queryRepository;
        }

        [HttpGet("{teamId}")]
        public async Task<ActionResult> GetTeam(Guid teamId)
        {
            var teamQuerry = await _queryRepository.Load<TeamReadModel>(teamId);
            return Ok(teamQuerry.Value);
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