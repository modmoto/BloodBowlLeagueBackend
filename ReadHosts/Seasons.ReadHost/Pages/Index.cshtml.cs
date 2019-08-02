using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microwave.Domain.Results;
using Microwave.Queries;
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
    }
}