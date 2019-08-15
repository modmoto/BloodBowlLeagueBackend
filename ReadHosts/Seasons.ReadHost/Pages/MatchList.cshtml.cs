using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microwave.Domain.Identities;
using Microwave.Queries;
using Seasons.ReadHost.Matches;
using Seasons.ReadHost.Teams;

namespace Seasons.ReadHost.Pages
{
    public class MatchList : PageModel
    {
        private readonly IReadModelRepository _readModelRepository;

        public IEnumerable<MatchupReadModel> Matches { get; set; }
        public IEnumerable<TeamReadModel> Teams { get; set; }

        public MatchList(IReadModelRepository readModelRepository)
        {
            _readModelRepository = readModelRepository;
        }

        public async Task OnGet()
        {
            var result = await _readModelRepository.LoadAllAsync<MatchupReadModel>();
            var teamResult = await _readModelRepository.LoadAllAsync<TeamReadModel>();
            Matches = result.Value;
            Teams = teamResult.Value;
        }

        public TeamReadModel FullTeam(GuidIdentity teamId)
        {
            return Teams.Single(t => t.TeamId == teamId);
        }
    }
}