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
            foreach (var match in matches)
            {
                var guestTeam = await _queryRepository.LoadAsync<TeamReadModel>(match.TeamAsGuest);
                var homeTeam = await _queryRepository.LoadAsync<TeamReadModel>(match.TeamAtHome);
                minimalMatches.Add(new MinimalMatchHto(
                    match.MatchId,
                    match.GameResult,
                    match.IsStarted,
                    match.IsFinished,
                    guestTeam.Value.TeamName,
                    homeTeam.Value.TeamName));
            }

            return Ok(minimalMatches);
        }
    }
}