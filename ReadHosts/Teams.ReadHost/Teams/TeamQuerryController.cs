using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microwave.Domain;
using Microwave.Queries;
using Teams.ReadHost.Teams.PlayerReadModels;
using Teams.ReadHost.Teams.TeamReadmodels;

namespace Teams.ReadHost.Teams
{
    [Route("api/teams")]
    public class TeamQuerryController : Controller
    {
        private readonly IReadModelRepository _queryRepository;

        public TeamQuerryController(IReadModelRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        [HttpGet("{teamId}")]
        public async Task<ActionResult> GetTeam(GuidIdentity teamId)
        {
            var teamQuerry = await _queryRepository.Load<TeamReadModel>(teamId);
            return Ok(teamQuerry);
        }

        [HttpGet("{teamId}/full")]
        public async Task<ActionResult> GetTeamFull(GuidIdentity teamId)
        {
            var teamQuerry = await _queryRepository.Load<TeamReadModel>(teamId);
            var players = new List<PlayerReadModel>();
            foreach (var playerDto in teamQuerry.Value.PlayerList)
            {
                var player = await _queryRepository.Load<PlayerReadModel>(playerDto.PlayerId);
                players.Add(player.Value);
            }

            return Ok(new TeamReadModelFull { PlayerList = players, Team = teamQuerry.Value });
        }

        [HttpGet]
        public async Task<ActionResult> GetTeams()
        {
            var teamQuerry = await _queryRepository.LoadAll<TeamOverviewReadModel>();
            return Ok(teamQuerry.Value);
        }
    }
}