using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microwave.Queries;
using Teams.ReadHost.Players;

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
        public async Task<ActionResult> GetTeam(Guid teamId)
        {
            var teamQuerry = await _queryRepository.LoadAsync<TeamReadModel>(teamId);
            return Ok(teamQuerry.Value);
        }

        [HttpGet]
        public async Task<ActionResult> GetTeamsOverviews()
        {
            var teamQuerry = await _queryRepository.LoadAllAsync<TeamReadModel>();
            var teamOverviewHtos = teamQuerry.Value.Select(t => new TeamOverviewHto(t));
            return Ok(teamOverviewHtos);
        }

        [HttpGet("{teamId}/full")]
        public async Task<ActionResult> GetTeamWithPlayers(Guid teamId)
        {
            var result = await _queryRepository.LoadAsync<TeamReadModel>(teamId);
            var players = await _queryRepository.LoadAllAsync<PlayerReadModel>();

            var team = result.Value;
            var playersValue = players.Value;
            var playerReadModels = playersValue.Where(p =>
                team.PlayerList.Any(a => a.PlayerId == p.PlayerId)).OrderBy(p => p.PlayerPositionNumber).ToList();
            return Ok(new TeamWithPlayersHto(team, playerReadModels));
        }
    }

    public class TeamWithPlayersHto
    {
        public TeamReadModel Team { get; }
        public IEnumerable<PlayerReadModel> PlayerList { get; }

        public TeamWithPlayersHto(TeamReadModel team, IEnumerable<PlayerReadModel> playerReadModels)
        {
            Team = team;
            PlayerList = playerReadModels;
        }
    }

    public class TeamOverviewHto
    {
        public string TeamTeamName { get; }
        public Guid TeamTeamId { get; }
        public string TeamTrainerName { get; }
        public string RaceId { get; }
        public bool TeamIsFinished { get; }

        public TeamOverviewHto(TeamReadModel team)
        {
            TeamTeamName = team.TeamName;
            TeamTeamId = team.TeamId;
            TeamTrainerName = team.TrainerName;
            RaceId = team.RaceId;
            TeamIsFinished = team.IsFinished;
        }
    }
}