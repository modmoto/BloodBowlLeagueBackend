using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microwave.Domain.Identities;
using Microwave.Domain.Results;
using Microwave.Queries;
using Newtonsoft.Json;
using Seasons.ReadHost.Seasons;
using Seasons.ReadHost.Teams;

namespace Pages
{
    public class Seasons : PageModel
    {
        private readonly IReadModelRepository _readModelRepository;

        [BindProperty(SupportsGet = true)]
        public Guid SeasonId { get; set; }
        public SeasonReadModel Season { get; set; }
        public IEnumerable<TeamReadModel> Teams { get; set; }

        public Seasons(IReadModelRepository readModelRepository)
        {
            _readModelRepository = readModelRepository;
        }

        public async Task OnGet()
        {
            var season = await _readModelRepository.Load<SeasonReadModel>(GuidIdentity.Create(SeasonId));
            var teams = await _readModelRepository.LoadAll<TeamReadModel>();
            if (season.Is<Ok>())
            {
                Season = season.Value;
            }
            Teams = teams.Value;
        }

        public async Task<IActionResult> OnPost()
        {
            var teamId = Request.Form["teamId"];
            var httpClient = new HttpClient();
            var teamObject = JsonConvert.SerializeObject(new { teamId = teamId.ToString() });
            var content = new StringContent(teamObject, Encoding.UTF8, "application/json");
            var requestUri = new Uri($"http://localhost:5004/Api/Seasons/{SeasonId}/add-team");
            var result = await httpClient.PostAsync(requestUri, content);
            return Redirect(SeasonId.ToString());
        }
    }
}