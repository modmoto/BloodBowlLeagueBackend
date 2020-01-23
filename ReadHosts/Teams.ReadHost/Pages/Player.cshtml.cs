using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microwave.Queries;
using ReadHosts.Common;
using ServiceConfig;
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
                var skillReadModels = _skills.Where(s => Player.PlayerConfig.SkillsOnDefault.Contains(s.SkillType)).ToList();

                foreach (var freeSkillPoint in Player.FreeSkillPoints)
                {
                    if (freeSkillPoint >= FreeSkillPoint.Double)
                    {
                        var readModels = _skills.Where(s =>
                            Player.PlayerConfig.SkillsOnDouble.Contains(s.SkillType)).ToList();
                        skillReadModels.AddRange(readModels);
                    }

                    if (freeSkillPoint >= FreeSkillPoint.PlusOneArmorOrMovement)
                    {
                        skillReadModels.AddRange(_skills.Where(s => s.SkillId == "PlusOneArmor"));
                        skillReadModels.AddRange(_skills.Where(s => s.SkillId == "PlusOneMovement"));
                    }

                    if (freeSkillPoint >= FreeSkillPoint.PlusOneAgility)
                    {
                        skillReadModels.AddRange(_skills.Where(s => s.SkillId == "PlusOneAgility"));
                    }

                    if (freeSkillPoint == FreeSkillPoint.PlusOneStrength)
                    {
                        skillReadModels.AddRange(_skills.Where(s => s.SkillId == "PlusOneStrength"));
                    }
                }

                return skillReadModels;
            }
        }

        [BindProperty(SupportsGet = true)]
        public Guid PlayerId { get; set; }

        public IEnumerable<FreeSkillPoint> ChoosableSkills => new List<FreeSkillPoint>
        {
            FreeSkillPoint.Normal,
            FreeSkillPoint.Double,
            FreeSkillPoint.PlusOneArmorOrMovement,
            FreeSkillPoint.PlusOneAgility,
            FreeSkillPoint.PlusOneStrength,
        };


        public PlayerModel(IReadModelRepository readModelRepository, MessageMitigator mitigator)
        {
            _readModelRepository = readModelRepository;
            _mitigator = mitigator;
        }

        public async Task OnGet()
        {
            var resultPlayer = await _readModelRepository.LoadAsync<PlayerReadModel>(PlayerId);
            var resultSkills = await _readModelRepository.LoadAllAsync<SkillReadModel>();
            Player = resultPlayer.Value;
            _skills = resultSkills.Value;
        }

        public async Task<IActionResult> OnPostChooseSkill()
        {
            var skillId = Request.Form["skillId"].ToString();

            await _mitigator.PostAsync(
                new Uri($"{ServiceConfiguration.PlayerHost}Api/Players/{PlayerId}/choose-skill"),
                new { skillId });
            return Redirect(PlayerId.ToString());
        }

        public async Task<IActionResult> OnPostRegisterLevelUpSkillPointRoll()
        {
            var freeSkillPoint = Request.Form["freeSkillType"].ToString();

            await _mitigator.PostAsync(
                new Uri($"{ServiceConfiguration.PlayerHost}Api/Players/{PlayerId}/register-skill"),
                new { freeSkillPoint });
            return Redirect(PlayerId.ToString());
        }
    }
}