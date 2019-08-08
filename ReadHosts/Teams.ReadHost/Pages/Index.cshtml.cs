using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microwave.Queries;
using Newtonsoft.Json;
using Teams.ReadHost.Races;
using Teams.ReadHost.Teams;

namespace Pages
{
    public class IndexModel : PageModel
    {
        private readonly IReadModelRepository _readModelRepository;

        public IEnumerable<TeamOverviewReadModel> AllTeams { get; set; }
        public IEnumerable<RaceReadModel> AllRaces { get; set; }

        public IndexModel(IReadModelRepository readModelRepository)
        {
            _readModelRepository = readModelRepository;
        }

        public async Task OnGetAsync()
        {
            var teams = await _readModelRepository.LoadAll<TeamOverviewReadModel>();
            var races = await _readModelRepository.LoadAll<RaceReadModel>();
            AllTeams = teams.Value;
            AllRaces = races.Value;
        }

        public async Task<IActionResult> OnPost()
        {
            var teamName = Request.Form["teamNameTextInput"];
            var trainerName = Request.Form["trainerNameTextInput"];
            var raceId = Request.Form["raceIdDropDownInput"];
            var httpClient = new HttpClient();
            var teamObject = JsonConvert.SerializeObject(new { teamName, trainerName, raceId });
            var content = new StringContent(teamObject, Encoding.UTF8, "application/json");
            var requestUri = new Uri("http://localhost:5001/Api/Teams/create");
            await httpClient.PostAsync(requestUri, content);
            return Redirect("http://localhost:5006");
        }
    }
}