using System.Collections.Generic;
using System.Linq;
using Microwave.Domain.Identities;
using Microwave.Queries;
using Teams.ReadHost.Players.Events;

namespace Teams.ReadHost.Players
{
    public class PlayerReadModel : ReadModel<PlayerCreated>,
        IHandle<PlayerCreated>,
        IHandle<SkillChosen>,
        IHandle<PlayerLeveledUp>,
        IHandle<PlayerPassed>,
        IHandle<PlayerMadeCasualty>,
        IHandle<PlayerMadeTouchdown>,
        IHandle<PlayerWasNominatedMostValuablePlayer>
    {
        public GuidIdentity PlayerId { get; private set; }
        public GuidIdentity TeamId { get; private set; }
        public StringIdentity PlayerTypeId{ get; private set; }
        public IEnumerable<StringIdentity> Skills { get; private set; } = new List<StringIdentity>();

        public long StarPlayerPoints { get; set; }
        public int Level { get; set; } = 1;
        public string Name { get; private set; }

        public IEnumerable<FreeSkillPoint> FreeSkillPoints { get; private set; } = new List<FreeSkillPoint>();

        public void Handle(PlayerCreated domainEvent)
        {
            PlayerId = domainEvent.PlayerId;
            TeamId = domainEvent.TeamId;
            PlayerTypeId = domainEvent.PlayerTypeId;
            Name = domainEvent.Name;
        }

        public void Handle(SkillChosen domainEvent)
        {
            Skills = Skills.Append(domainEvent.NewSkill);
        }

        public void Handle(PlayerLeveledUp domainEvent)
        {
            Level = domainEvent.NewLevel;
            FreeSkillPoints = FreeSkillPoints.Append(domainEvent.NewFreeSkillPoint);
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
    }
}