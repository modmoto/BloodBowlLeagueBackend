using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microwave.Queries;
using Newtonsoft.Json;

namespace Teams.ReadHost.Pages
{
    public class Teams : PageModel
    {
        private readonly IReadModelRepository _readModelRepository;

        [BindProperty(SupportsGet = true)]
        public Guid SeasonId { get; set; }

        public Teams(IReadModelRepository readModelRepository)
        {
            _readModelRepository = readModelRepository;
        }

        public async Task<IActionResult> OnPost()
        {
            var teamId = Request.Form["teamId"];
            var httpClient = new HttpClient();
            var teamObject = JsonConvert.SerializeObject(new { teamId = teamId.ToString() });
            var content = new StringContent(teamObject, Encoding.UTF8, "application/json");
            var requestUri = new Uri($"http://localhost:5004/Api/Seasons/{SeasonId}/add-team");
            await httpClient.PostAsync(requestUri, content);

            return Redirect(SeasonId.ToString());
        }
    }
}