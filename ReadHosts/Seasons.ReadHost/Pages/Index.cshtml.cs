using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microwave.Domain.Results;
using Microwave.Queries;
using Newtonsoft.Json;
using Seasons.ReadHost.Seasons;

namespace Pages
{
    public class IndexModel : PageModel
    {
        private readonly IReadModelRepository _readModelRepository;

        public AllSeasonsOverview AllSeasons { get; set; }

        public IndexModel(IReadModelRepository readModelRepository)
        {
            _readModelRepository = readModelRepository;
        }

        public async Task OnGetAsync()
        {
            var loadAll = await _readModelRepository.Load<AllSeasonsOverview>();
            AllSeasons = loadAll.Is<Ok>() ? loadAll.Value : new AllSeasonsOverview();
        }

        public async Task<IActionResult> OnPost()
        {
            var seasonName = Request.Form["seasonNameTextInput"];
            var httpClient = new HttpClient();
            var teamObject = JsonConvert.SerializeObject(new { seasonName = seasonName.ToString() });
            var content = new StringContent(teamObject, Encoding.UTF8, "application/json");
            var requestUri = new Uri("http://localhost:5004/Api/Seasons/create");
            await httpClient.PostAsync(requestUri, content);
            return Redirect("http://localhost:5006");
        }
    }
}