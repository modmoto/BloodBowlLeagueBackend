using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microwave.Queries;
using ReadHosts.Common;
using Teams.ReadHost.Races;
using Teams.ReadHost.Teams;

namespace Teams.ReadHost.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IReadModelRepository _readModelRepository;
        private readonly MessageMitigator _mitigator;

        public IEnumerable<TeamReadModel> AllTeams { get; set; }
        public IEnumerable<RaceReadModel> AllRaces { get; set; }

        public IndexModel(
            IReadModelRepository readModelRepository,
            MessageMitigator mitigator)
        {
            _readModelRepository = readModelRepository;
            _mitigator = mitigator;
        }

        public async Task OnGetAsync()
        {
            var teams = await _readModelRepository.LoadAllAsync<TeamReadModel>();
            var races = await _readModelRepository.LoadAllAsync<RaceReadModel>();
            AllTeams = teams.Value;
            AllRaces = races.Value;
        }

        public async Task<IActionResult> OnPost()
        {
            var teamName = Request.Form["teamNameTextInput"].ToString();
            var trainerName = Request.Form["trainerNameTextInput"].ToString();
            var raceId = Request.Form["raceIdDropDownInput"].ToString();
            await _mitigator.PostAsync(
                new Uri("http://localhost:5001/Api/Teams/create"),
                new { teamName, trainerName, raceId });
            return Redirect("http://localhost:5000");
        }
    }
}