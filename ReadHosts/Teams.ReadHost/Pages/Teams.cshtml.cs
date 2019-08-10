using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microwave.Queries;
using Newtonsoft.Json;
using ReadHosts.Common;

namespace Teams.ReadHost.Pages
{
    public class Teams : PageModel
    {
        private readonly IReadModelRepository _readModelRepository;
        private readonly MessageMitigator _mitigator;

        [BindProperty(SupportsGet = true)]
        public Guid SeasonId { get; set; }

        public Teams(
            IReadModelRepository readModelRepository,
            MessageMitigator mitigator)
        {
            _readModelRepository = readModelRepository;
            _mitigator = mitigator;
        }

        public async Task<IActionResult> OnPost()
        {
            var teamId = Request.Form["teamId"];
            await _mitigator.PostAsync(
                new Uri($"http://localhost:5004/Api/Seasons/{SeasonId}/add-team"),
                new { teamId = teamId.ToString() });
            return Redirect(SeasonId.ToString());
        }
    }
}