using System.Threading.Tasks;
using Application.Matches;
using Microsoft.AspNetCore.Mvc;
using Microwave.Domain.Identities;

namespace Host.Matches
{
    [Route("api/seasons")]
    public class SeasonController : Controller
    {
        private readonly SeasonCommandHandler _commandHandler;

        public SeasonController(SeasonCommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        [HttpGet("{seasonId}")]
        public async Task<ActionResult> GetSeason(GuidIdentity seasonId)
        {
            var command = new GetSeasonCommand(seasonId);
            var season = await _commandHandler.GetSeason(command);
            return Ok(season);
        }

        [HttpPost("{seasonId}/start")]
        public async Task<ActionResult> StartSeason(GuidIdentity seasonId)
        {
            var command = new StartSeasonCommand(seasonId);
            await _commandHandler.StartSeason(command);
            return Ok();
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateSeason()
        {
            var result = await _commandHandler.CreateSeason();
            return Created($"{Request.Scheme}{Request.Path}/seasons/{result.Id}", null);
        }

        [HttpPost("{seasonId}/add-team")]
        public async Task<ActionResult> StartMatch(GuidIdentity seasonId, [FromBody] AddTeamToSeasonCommand command)
        {
            command.SeasonId = seasonId;
            await _commandHandler.AddTeamToSeason(command);
            return Ok();
        }
    }
}