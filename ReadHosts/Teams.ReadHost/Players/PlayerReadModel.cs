using System;
using System.Collections.Generic;
using System.Linq;
using Microwave.Queries;
using Teams.ReadHost.Players.Events;

namespace Teams.ReadHost.Players
{
    public class PlayerReadModel : ReadModel<PlayerCreated>,
        IHandle<PlayerCreated>,
        IHandle<SkillChosen>,
        IHandle<PlayerLeveledUp>,
        IHandle<PlayerLevelUpPossibilitiesChosen>,
        IHandle<PlayerPassed>,
        IHandle<PlayerMadeCasualty>,
        IHandle<PlayerMadeTouchdown>,
        IHandle<PlayerWasNominatedMostValuablePlayer>
    {
        public Guid PlayerId { get; private set; }
        public Guid TeamId { get; private set; }
        public string PlayerTypeId { get; private set; }
        public PlayerConfig PlayerConfig { get; private set; }
        public IEnumerable<string> Skills { get; private set; } = new List<string>();

        public long StarPlayerPoints { get; set; }
        public int Level { get; set; } = 1;

        public int ChoosableSkills { get; set; }

        public IEnumerable<FreeSkillPoint> FreeSkillPoints { get; private set; } = new List<FreeSkillPoint>();
        public bool HasFreeSkill => FreeSkillPoints.Any();
        public bool CanRegisterLevelUpSkillPointRoll => ChoosableSkills > 0;

        public void Handle(PlayerCreated domainEvent)
        {
            PlayerId = domainEvent.PlayerId;
            TeamId = domainEvent.TeamId;
            PlayerTypeId = domainEvent.PlayerTypeId;
            PlayerConfig = domainEvent.PlayerConfig;
            var startingSkills = Skills.ToList();
            startingSkills.AddRange(domainEvent.PlayerConfig.StartingSkills.Select(s => s.SkillId));
            Skills = startingSkills;
        }

        public void Handle(SkillChosen domainEvent)
        {
            Skills = Skills.Append(domainEvent.NewSkill.SkillId);
            FreeSkillPoints = domainEvent.NewFreeSkillPoints;
        }

        public void Handle(PlayerLeveledUp domainEvent)
        {
            Level = domainEvent.NewLevel;
            ChoosableSkills += 1;
        }

        public void Handle(PlayerPassed domainEvent)
        {
            StarPlayerPoints = domainEvent.NewStarPlayerPoints;
        }

        public void Handle(PlayerMadeCasualty domainEvent)
        {
            StarPlayerPoints = domainEvent.NewStarPlayerPoints;
        }

        public void Handle(PlayerMadeTouchdown domainEvent)
        {
            StarPlayerPoints = domainEvent.NewStarPlayerPoints;
        }

        public void Handle(PlayerWasNominatedMostValuablePlayer domainEvent)
        {
            StarPlayerPoints = domainEvent.NewStarPlayerPoints;
        }

        public void Handle(PlayerLevelUpPossibilitiesChosen domainEvent)
        {
            FreeSkillPoints = FreeSkillPoints.Append(domainEvent.NewFreeSkillPoint);
            ChoosableSkills -= 1;
        }
    }
}