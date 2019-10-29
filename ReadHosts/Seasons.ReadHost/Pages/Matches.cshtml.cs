using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microwave.Queries;
using ReadHosts.Common;
using Seasons.ReadHost.Matches;
using Seasons.ReadHost.Teams;
using ServiceConfig;

namespace Seasons.ReadHost.Pages
{
    public class Matches : PageModel
    {
        private readonly IReadModelRepository _readModelRepository;
        private readonly MessageMitigator _mitigator;
        public IEnumerable<MatchupReadModel> MatchList { get; set; }
        public IEnumerable<TeamReadModel> Teams { get; set; }

        public Matches(
            IReadModelRepository readModelRepository,
            MessageMitigator mitigator)
        {
            _readModelRepository = readModelRepository;
            _mitigator = mitigator;
        }

        public async Task OnGet()
        {
            var result = await _readModelRepository.LoadAllAsync<MatchupReadModel>();
            var teamResult = await _readModelRepository.LoadAllAsync<TeamReadModel>();
            MatchList = result.Value;
            Teams = teamResult.Value;
        }

        public async Task<IActionResult> OnPost()
        {
            var homeTeam = Request.Form["homeTeam"].ToString();
            var guestTeam = Request.Form["guestTeam"].ToString();
            await _mitigator.PostAsync(
                new Uri($"{ServiceConfiguration.MatchHost}Api/Matches/create"),
                new { homeTeam, guestTeam });
            return Redirect($"{ServiceConfiguration.SeasonReadHost}Matches");
        }

        public TeamReadModel FullTeam(Guid teamId)
        {
            return Teams.Single(t => t.TeamId == teamId);
        }
    }
}