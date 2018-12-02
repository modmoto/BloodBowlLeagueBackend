using System;
using System.Threading.Tasks;
using Application.Teams;
using Domain.Teams;
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
        private readonly IEventStore _eventStore;

        public TeamController(TeamCommandHandler commandHandler, IQeryRepository queryRepository, IEventStore eventStore)
        {
            _commandHandler = commandHandler;
            _queryRepository = queryRepository;
            _eventStore = eventStore;
        }

        [HttpGet("{teamId}")]
        public async Task<ActionResult> GetTeam(Guid teamId)
        {
            var eventstoreResult = await _eventStore.LoadAsync<Team>(teamId);
            return Ok(eventstoreResult);
        }

        [HttpGet("/querries/team/{teamId}")]
        public async Task<ActionResult> GetTeamQuerry(Guid teamId)
        {
            var teamQuerry = await _queryRepository.Load<TeamQuery>(teamId);
            return Ok(teamQuerry.Value);
        }

        [HttpPost("create")]
        public async Task<ActionResult> GetTeam([FromBody] CreateTeamComand createTeamComand)
        {
            var teamGuid = await _commandHandler.CreateTeam(createTeamComand);
            return Created($"Api/Teams/{teamGuid}", null);
        }
    }
}