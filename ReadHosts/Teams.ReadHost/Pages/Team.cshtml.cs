using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microwave.Queries;
using ReadHosts.Common;
using ServiceConfig;
using Teams.ReadHost.Players;
using Teams.ReadHost.Teams;

namespace Teams.ReadHost.Pages
{
    public class TeamModel : PageModel
    {
        private readonly IReadModelRepository _readModelRepository;
        private readonly MessageMitigator _mitigator;

        public TeamReadModel Team { get; set; }
        public IEnumerable<PlayerDto> Players => Team.PlayerList;
        public IEnumerable<PlayerReadModel> FullPlayers { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid TeamId { get; set; }


        public TeamModel(
            IReadModelRepository readModelRepository,
            MessageMitigator mitigator)
        {
            _readModelRepository = readModelRepository;
            _mitigator = mitigator;
        }

        public async Task OnGet()
        {
            var result = await _readModelRepository.LoadAsync<TeamReadModel>(TeamId);
            var players = await _readModelRepository.LoadAllAsync<PlayerReadModel>();

            var team = result.Value;
            var playersValue = players.Value;
            var playerReadModels = playersValue.Where(p =>
                team.PlayerList.Any(a => a.PlayerId == p.PlayerId));
            FullPlayers = playerReadModels;
            Team = team;
        }

        public async Task<IActionResult> OnPostAddPlayer()
        {
            var playerTypeId = Request.Form["playerType"].ToString();
            var result = await _readModelRepository.LoadAsync<TeamReadModel>(TeamId);

            await _mitigator.PostAsync(
                new Uri($"{ServiceConfiguration.TeamHost}Api/Teams/{TeamId}/buy-player"),
                new { playerTypeId , TeamVersion = result.Value.Version });
            return Redirect(TeamId.ToString());
        }

        public async Task<IActionResult> OnPostRemovePlayer(Guid playerId)
        {
            var result = await _readModelRepository.LoadAsync<TeamReadModel>(TeamId);

            await _mitigator.PostAsync(
                new Uri($"{ServiceConfiguration.TeamHost}Api/Teams/{TeamId}/remove-player"),
                new { playerId , TeamVersion = result.Value.Version });
            return Redirect(TeamId.ToString());
        }

        public async Task<IActionResult> OnPostFinishTeam()
        {
            var result = await _readModelRepository.LoadAsync<TeamReadModel>(TeamId);

            await _mitigator.PostAsync(
                new Uri($"{ServiceConfiguration.TeamHost}Api/Teams/{TeamId}/finish"),
                new { TeamId , TeamVersion = result.Value.Version });
            return Redirect(TeamId.ToString());
        }
    }
}