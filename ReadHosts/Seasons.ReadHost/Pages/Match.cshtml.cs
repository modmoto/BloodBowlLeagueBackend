using ReadHosts.Common;
using Seasons.ReadHost.Matches;
using Seasons.ReadHost.Players;
using Seasons.ReadHost.Teams;
using ServiceConfig;

namespace Seasons.ReadHost.Pages
{
    public class Match : PageModel
    {
        private readonly IReadModelRepository _readModelRepository;
        private readonly MessageMitigator _mitigator;

        [BindProperty(SupportsGet = true)]
        public Guid MatchId { get; set; }
        public MatchupReadModel SingleMatch { get; set; }
        public IEnumerable<TeamReadModel> Teams { get; set; }
        public IEnumerable<PlayerReadModel> Players { get; set; }
        public TeamReadModel GuestTeam => FullTeam(SingleMatch.TeamAsGuest);
        public TeamReadModel HomeTeam => FullTeam(SingleMatch.TeamAtHome);
        public IEnumerable<Guid> AllPlayers
        {
            get
            {
                var guidIdentities = new List<Guid>();
                guidIdentities.AddRange(SingleMatch.GuestTeamPlayers);
                guidIdentities.AddRange(SingleMatch.HomeTeamPlayers);
                return guidIdentities;
            }
        }

        public Match(
            IReadModelRepository readModelRepository,
            MessageMitigator mitigator)
        {
            _readModelRepository = readModelRepository;
            _mitigator = mitigator;
        }

        public async Task OnGet()
        {
            var result = await _readModelRepository.LoadAsync<MatchupReadModel>(Guid.Create(MatchId));
            var teamResult = await _readModelRepository.LoadAllAsync<TeamReadModel>();
            var playerResult = await _readModelRepository.LoadAllAsync<PlayerReadModel>();
            SingleMatch = result.Value;
            Teams = teamResult.Value;
            Players = playerResult.Value;
        }

        public async Task<IActionResult> OnPostAddProgress()
        {
            var playerId = Request.Form["playerId"].ToString();
            var progressionEventRaw = Request.Form["progressionEvent"].ToString();
            Enum.TryParse<ProgressionEvent>(progressionEventRaw, out var progressionEvent);
            await _mitigator.PostAsync(
                new Uri($"{ServiceConfiguration.MatchHost}Api/Matches/{MatchId}/progress"),
                new { PlayerProgression = new PlayerProgression(
                        Guid.Create(new Guid(playerId)),
                        progressionEvent)});
            return Redirect(MatchId.ToString());
        }

        public async Task<IActionResult> OnPostStartMatch(Guid matchId)
        {
            await _mitigator.PostAsync(
                new Uri($"{ServiceConfiguration.MatchHost}Api/Matches/{MatchId}/start"),
                new { });
            return Redirect(MatchId.ToString());
        }
        
        public async Task<IActionResult> OnPostFinishMatch(Guid matchId)
        {
            await _mitigator.PostAsync(
                new Uri($"{ServiceConfiguration.MatchHost}Api/Matches/{MatchId}/finish"),
                new { });
            return Redirect(MatchId.ToString());
        }

        public TeamReadModel FullTeam(Guid teamId)
        {
            return Teams.Single(t => t.TeamId == teamId);
        }

        public PlayerReadModel FullPlayer(Guid playerId)
        {
            return Players.Single(t => t.PlayerId == playerId);
        }
    }
}