using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microwave.Domain.Results;
using Microwave.Queries;
using ReadHosts.Common;
using Seasons.ReadHost.Seasons;
using ServiceConfig;

namespace Seasons.ReadHost.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IReadModelRepository _readModelRepository;
        private readonly MessageMitigator _mitigator;

        public AllSeasonsOverview AllSeasons { get; set; }

        public IndexModel(
            IReadModelRepository readModelRepository,
            MessageMitigator mitigator)
        {
            _readModelRepository = readModelRepository;
            _mitigator = mitigator;
        }

        public async Task OnGetAsync()
        {
            var loadAll = await _readModelRepository.LoadAsync<AllSeasonsOverview>();
            AllSeasons = loadAll.Is<Ok>() ? loadAll.Value : new AllSeasonsOverview();
        }

        public async Task<IActionResult> OnPost()
        {
            var seasonName = Request.Form["seasonNameTextInput"].ToString();
            var ob = new { seasonName };
            await _mitigator.PostAsync(new Uri($"{ServiceConfiguration.SeasonHost}Api/Seasons/create"), ob);
            return Redirect($"{ServiceConfiguration.SeasonReadHost}");
        }
    }
}