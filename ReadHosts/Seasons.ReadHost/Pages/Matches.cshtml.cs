using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microwave.Domain.Identities;
using Microwave.Queries;
using ReadHosts.Common;
using Seasons.ReadHost.Matches;
using Seasons.ReadHost.Players;
using Seasons.ReadHost.Teams;

namespace Seasons.ReadHost.Pages
{
    public class Matches : PageModel
    {
        private readonly IReadModelRepository _readModelRepository;
        private readonly MessageMitigator _mitigator;

        [BindProperty(SupportsGet = true)]
        public Guid MatchId { get; set; }
        public MatchupReadModel Match { get; set; }
        public IEnumerable<TeamReadModel> Teams { get; set; }
        public IEnumerable<PlayerReadModel> Players { get; set; }
        public TeamReadModel GuestTeam => FullTeam(Match.TeamAsGuest);
        public TeamReadModel HomeTeam => FullTeam(Match.TeamAtHome);
        public IEnumerable<GuidIdentity> AllPlayers
        {
            get
            {
                var guidIdentities = new List<GuidIdentity>();
                guidIdentities.AddRange(Match.GuestTeamPlayers);
                guidIdentities.AddRange(Match.HomeTeamPlayers);
                return guidIdentities;
            }
        }

        public Matches(
            IReadModelRepository readModelRepository,
            MessageMitigator mitigator)
        {
            _readModelRepository = readModelRepository;
            _mitigator = mitigator;
        }

        public async Task OnGet()
        {
            var result = await _readModelRepository.LoadAsync<MatchupReadModel>(GuidIdentity.Create(MatchId));
            var teamResult = await _readModelRepository.LoadAllAsync<TeamReadModel>();
            var playerResult = await _readModelRepository.LoadAllAsync<PlayerReadModel>();
            Match = result.Value;
            Teams = teamResult.Value;
            Players = playerResult.Value;
        }

        public async Task<IActionResult> OnPost()
        {
            var playerId = Request.Form["playerId"].ToString();
            var progressionEventRaw = Request.Form["progressionEvent"].ToString();
            Enum.TryParse<ProgressionEvent>(progressionEventRaw, out var progressionEvent);
            await _mitigator.PostAsync(
                new Uri($"http://localhost:5003/Api/Matches/{MatchId}/progress"),
                new { PlayerProgression = new PlayerProgression(
                        GuidIdentity.Create(new Guid(playerId)),
                        progressionEvent)});
            return Redirect(MatchId.ToString());
        }

        public TeamReadModel FullTeam(GuidIdentity teamId)
        {
            return Teams.Single(t => t.TeamId == teamId);
        }

        public PlayerReadModel FullPlayer(GuidIdentity playerId)
        {
            return Players.Single(t => t.PlayerId == playerId);
        }
    }
}