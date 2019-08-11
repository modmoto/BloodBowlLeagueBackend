using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microwave.Domain.Identities;
using Microwave.Queries;
using Teams.ReadHost.Players;
using Teams.ReadHost.Teams;

namespace Teams.ReadHost.Pages
{
    public class Teams : PageModel
    {
        private readonly IReadModelRepository _readModelRepository;

        public TeamReadModel Team { get; set; }
        public IEnumerable<PlayerReadModel> Players { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid TeamId { get; set; }


        public Teams(
            IReadModelRepository readModelRepository)
        {
            _readModelRepository = readModelRepository;
        }

        public async Task OnGet()
        {
            var result = await _readModelRepository.Load<TeamReadModel>(GuidIdentity.Create(TeamId));
            var players = await _readModelRepository.LoadAll<PlayerReadModel>();

            var team = result.Value;
            var playerReadModels = players.Value.Where(p => team.PlayerList.Any(t => t.PlayerId == p.PlayerId));
            Players = playerReadModels;
            Team = team;
        }
    }
}