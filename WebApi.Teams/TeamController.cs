using System;
using System.Threading.Tasks;
using Application.Teams;
using Microsoft.AspNetCore.Mvc;
using Microwave.Application;
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
            var seasons = await _queryRepository.Load<TeamQuery>(teamId);
            return Ok(seasons.Value);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> GetTeam(CreateTeamComand createTeamComand)
        {
            var teamGuid = await _commandHandler.CreateTeam(createTeamComand);
            return Created($"Api/Teams/{teamGuid}", null);
        }
    }
}