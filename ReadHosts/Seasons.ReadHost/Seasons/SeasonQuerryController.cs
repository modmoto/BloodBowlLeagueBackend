using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microwave.Queries;
using Seasons.ReadHost.Matches;
using Seasons.ReadHost.Teams;

namespace Seasons.ReadHost.Seasons
{
    [Route("api/seasons")]
    public class SeasonQuerryController : Controller
    {
        private readonly IReadModelRepository _queryRepository;

        public SeasonQuerryController(IReadModelRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        [HttpGet("{seasonId}")]
        public async Task<ActionResult> GetSeason(Guid seasonId)
        {
            var teamQuerry = await _queryRepository.LoadAsync<SeasonReadModel>(seasonId);
            return Ok(teamQuerry.Value);
        }

        [HttpGet("{seasonId}/gameDays")]
        public async Task<ActionResult> GetGameDaysOfSeason(Guid seasonId)
        {
            var teamQuerry = await _queryRepository.LoadAsync<SeasonReadModel>(seasonId);
            var gameDays = teamQuerry.Value.GameDays.ToList();

            var minimalGameDayHtos = new List<MinimalGameDayHto>();
            foreach (var gameDay in gameDays)
            {
                var matchupReadModels = new List<MinimalMatchHto>();
                foreach (var matchUp in gameDay.Matchups)
                {
                    var match = await _queryRepository.LoadAsync<MatchupReadModel>(matchUp.MatchId);

                    var matchupReadModel = match.Value;
                    var homeTeam = await _queryRepository.LoadAsync<TeamReadModel>(matchupReadModel.TeamAtHome);
                    var guestTeam = await _queryRepository.LoadAsync<TeamReadModel>(matchupReadModel.TeamAsGuest);

                    var readModel = new MinimalMatchHto(
                        matchupReadModel.MatchId,
                        matchupReadModel.GameResult,
                        matchupReadModel.IsStarted, homeTeam.Value.TeamName, guestTeam.Value.TeamName);
                    matchupReadModels.Add(readModel);
                }

                var minimalGameDayHto = new MinimalGameDayHto(gameDay.Id, matchupReadModels);
                minimalGameDayHtos.Add(minimalGameDayHto);
            }

            return Ok(minimalGameDayHtos);
        }

        [HttpGet]
        public async Task<ActionResult> GetSeasons()
        {
            var teamQuerry = await _queryRepository.LoadAllAsync<SeasonReadModel>();
            return Ok(teamQuerry.Value);
        }
    }
}