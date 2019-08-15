using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microwave.Queries;
using Teams.ReadHost.Players;

namespace Teams.ReadHost.Pages
{
    public class PlayerListModel : PageModel
    {
        private readonly IReadModelRepository _readModelRepository;
        public IEnumerable<PlayerReadModel> PlayerList { get; set; }

        public PlayerListModel(
            IReadModelRepository readModelRepository)
        {
            _readModelRepository = readModelRepository;
        }

        public async Task OnGet()
        {
            var players = await _readModelRepository.LoadAllAsync<PlayerReadModel>();
            PlayerList = players.Value;
        }
    }
}