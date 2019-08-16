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
using Teams.ReadHost.Players.Events;
using Teams.ReadHost.Races;

namespace Teams.ReadHost.Pages
{
    public class PlayerModel : PageModel
    {
        private readonly IReadModelRepository _readModelRepository;
        private readonly MessageMitigator _mitigator;
        private IEnumerable<SkillReadModel> _skills;

        public PlayerReadModel Player { get; set; }

        public IEnumerable<SkillReadModel> RelevantSkills
        {
            get
            {
                if (!Player.HasFreeSkill) return new List<SkillReadModel>();
                var playerFreeSkillPoint = Player.FreeSkillPoint;
                var skillReadModels = _skills.Where(s => Player.PlayerConfig.SkillsOnDefault.Contains(s.SkillType)).ToList();

                if (playerFreeSkillPoint >= FreeSkillPoint.Double)
                {
                    var readModels = _skills.Where(s =>
                        Player.PlayerConfig.SkillsOnDouble.Contains(s.SkillType)).ToList();
                    skillReadModels.AddRange(readModels);
                }

                if (playerFreeSkillPoint >= FreeSkillPoint.PlusOneArmorOrMovement)
                {
                    skillReadModels.AddRange(_skills.Where(s => s.SkillId == StringIdentity.Create("PlusOneArmor")));
                    skillReadModels.AddRange(_skills.Where(s => s.SkillId == StringIdentity.Create("PlusOneMovement")));
                }

                if (playerFreeSkillPoint >= FreeSkillPoint.PlusOneAgility)
                {
                    skillReadModels.AddRange(_skills.Where(s => s.SkillId == StringIdentity.Create("PlusOneAgility")));
                }

                if (playerFreeSkillPoint == FreeSkillPoint.PlusOneStrength)
                {
                    skillReadModels.AddRange(_skills.Where(s => s.SkillId == StringIdentity.Create("PlusOneStrength")));
                }

                return skillReadModels;
            }
        }

        [BindProperty(SupportsGet = true)]
        public Guid PlayerId { get; set; }


        public PlayerModel(IReadModelRepository readModelRepository, MessageMitigator mitigator)
        {
            _readModelRepository = readModelRepository;
            _mitigator = mitigator;
        }

        public async Task OnGet()
        {
            var resultPlayer = await _readModelRepository.LoadAsync<PlayerReadModel>(GuidIdentity.Create(PlayerId));
            var resultSkills = await _readModelRepository.LoadAllAsync<SkillReadModel>();
            Player = resultPlayer.Value;
            _skills = resultSkills.Value;
        }

         public async Task<IActionResult> OnPost()
        {
            var skillId = Request.Form["skillId"].ToString();

            await _mitigator.PostAsync(
                new Uri($"http://localhost:5002/Api/Players/{PlayerId}/level-up"),
                new { skillId });
            return Redirect(PlayerId.ToString());
        }
    }
}