using System;
using System.Collections.Generic;
using System.Linq;
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
            var matches = teamQuerry.Value.ToList();

            var minimalMatches = new List<MinimalMatchHto>();
            foreach (var team in matches)
            {
                var guestTeam = await _queryRepository.LoadAsync<TeamReadModel>(team.TeamAsGuest);
                var homeTeam = await _queryRepository.LoadAsync<TeamReadModel>(team.TeamAtHome);
                minimalMatches.Add(new MinimalMatchHto(team.MatchId, guestTeam.Value.TeamName, homeTeam.Value.TeamName));
            }

            return Ok(minimalMatches);
        }
    }
}