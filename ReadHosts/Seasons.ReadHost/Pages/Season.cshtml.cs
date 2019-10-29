using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microwave.Queries;
using ReadHosts.Common;
using Seasons.ReadHost.Matches;
using Seasons.ReadHost.Seasons;
using Seasons.ReadHost.Teams;
using ServiceConfig;

namespace Seasons.ReadHost.Pages
{
    public class Seasons : PageModel
    {
        private readonly IReadModelRepository _readModelRepository;
        private readonly MessageMitigator _mitigator;

        [BindProperty(SupportsGet = true)]
        public Guid SeasonId { get; set; }
        public SeasonReadModel Season { get; set; }
        public IEnumerable<TeamReadModel> Teams { get; set; }
        public IEnumerable<TeamReadModel> AddedTeams => Teams.Where(t => Season.Teams.Contains(t.TeamId));
        public IEnumerable<MatchupReadModel> Matches { get; set; }

        public Seasons(
            IReadModelRepository readModelRepository,
            MessageMitigator mitigator)
        {
            _readModelRepository = readModelRepository;
            _mitigator = mitigator;
        }

        public async Task OnGet()
        {
            var season = await _readModelRepository.LoadAsync<SeasonReadModel>(Guid.Create(SeasonId));
            var teams = await _readModelRepository.LoadAllAsync<TeamReadModel>();
            var matches = await _readModelRepository.LoadAllAsync<MatchupReadModel>();
            if (season.Is<Ok>())
            {
                Season = season.Value;
            }
            Teams = teams.Value;
            Matches = matches.Value;
        }

        public async Task<IActionResult> OnPostAddTeam(string teamId)
        {
            await _mitigator.PostAsync(
                new Uri($"{ServiceConfiguration.SeasonHost}Api/Seasons/{SeasonId}/add-team"),
                new { teamId });
            return Redirect(SeasonId.ToString());
        }

        public async Task<IActionResult> OnPostStartSeason()
        {
            await _mitigator.PostAsync(
                new Uri($"{ServiceConfiguration.SeasonHost}Api/Seasons/{SeasonId}/start"),
                new { });
            return Redirect(SeasonId.ToString());
        }

        public TeamReadModel FullTeam(Guid teamId)
        {
            return Teams.Single(t => t.TeamId == teamId);
        }

        public MatchupReadModel FullMatch(Guid matchId)
        {
            return Matches.Single(m => m.MatchId == matchId);
        }
    }
}