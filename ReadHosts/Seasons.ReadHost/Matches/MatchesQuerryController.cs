using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microwave.Queries;
using Seasons.ReadHost.Teams;

namespace Seasons.ReadHost.Matches
{
    [Route("api/matches")]
    public class MatchesQuerryController : Controller
    {
        private readonly IReadModelRepository _queryRepository;

        public MatchesQuerryController(IReadModelRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        [HttpGet("{matchupId}")]
        public async Task<ActionResult> GetMatch(Guid matchupId)
        {
            var teamQuerry = await _queryRepository.LoadAsync<MatchupReadModel>(matchupId);
            return Ok(teamQuerry.Value);
        }

        [HttpGet]
        public async Task<ActionResult> GetMatches()
        {
            var teamQuerry = await _queryRepository.LoadAllAsync<MatchupReadModel>();
            foreach (var team in teamQuerry.Value)
            {
                var guestTeam = await _queryRepository.LoadAsync<TeamReadModel>(team.TeamAsGuest);
                var homeTeam = await _queryRepository.LoadAsync<TeamReadModel>(team.TeamAtHome);
                team.SetFullTeams(guestTeam.Value, homeTeam.Value);
            }
            return Ok(teamQuerry.Value);
        }
    }
}