using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microwave.Domain.Identities;
using Microwave.Queries;
using Seasons.ReadHost.Matches;

namespace Seasons.ReadHost.Pages
{
    public class Matches : PageModel
    {
        private readonly IReadModelRepository _readModelRepository;

        [BindProperty(SupportsGet = true)]
        public Guid MatchId { get; set; }
        public MatchupReadModel Match { get; set; }

        public Matches(IReadModelRepository readModelRepository)
        {
            _readModelRepository = readModelRepository;
        }

        public async Task OnGet()
        {
            var result = await _readModelRepository.LoadAsync<MatchupReadModel>(GuidIdentity.Create(MatchId));
            Match = result.Value;
        }
    }
}