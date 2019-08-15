using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microwave.Domain.Identities;
using Microwave.Queries;
using Teams.ReadHost.Players;

namespace Teams.ReadHost.Pages
{
    public class PlayerModel : PageModel
    {
        private readonly IReadModelRepository _readModelRepository;

        public PlayerReadModel Player { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid PlayerId { get; set; }


        public PlayerModel(IReadModelRepository readModelRepository)
        {
            _readModelRepository = readModelRepository;
        }

        public async Task OnGet()
        {
            var result = await _readModelRepository.LoadAsync<PlayerReadModel>(GuidIdentity.Create(PlayerId));
            Player = result.Value;
        }
    }
}