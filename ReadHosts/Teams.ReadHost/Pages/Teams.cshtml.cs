using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microwave.Domain.Identities;
using Microwave.Queries;
using ReadHosts.Common;
using Teams.ReadHost.Players;
using Teams.ReadHost.Teams;

namespace Teams.ReadHost.Pages
{
    public class Teams : PageModel
    {
        private readonly IReadModelRepository _readModelRepository;
        private readonly MessageMitigator _mitigator;

        public TeamReadModel Team { get; set; }
        public IEnumerable<PlayerReadModel> Players { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid TeamId { get; set; }


        public Teams(
            IReadModelRepository readModelRepository,
            MessageMitigator mitigator)
        {
            _readModelRepository = readModelRepository;
            _mitigator = mitigator;
        }

        public async Task OnGet()
        {
            var result = await _readModelRepository.LoadAsync<TeamReadModel>(GuidIdentity.Create(TeamId));
            var players = await _readModelRepository.LoadAllAsync<PlayerReadModel>();

            var team = result.Value;
            var playerReadModels = players.Value.Where(p => team.PlayerList.Any(t => t.PlayerId == p.PlayerId));
            Players = playerReadModels;
            Team = team;
        }

        public async Task<IActionResult> OnPost()
        {
            var teamId = Request.Form["playerType"];
            var result = await _readModelRepository.LoadAsync<TeamReadModel>(GuidIdentity.Create(TeamId));

            await _mitigator.PostAsync(
                new Uri($"http://localhost:5001/Api/Teams/{TeamId}/buy-player"),
                new { playerTypeId = teamId.ToString(), TeamVersion = result.Value.Version });
            return Redirect(TeamId.ToString());
        }
    }
}