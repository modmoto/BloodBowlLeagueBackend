using System;
using System.Collections.Generic;
using System.Linq;
using Microwave.Domain;
using Microwave.Queries;
using Teams.ReadHost.Players.Events;

namespace Teams.ReadHost.Players
{
    public class PlayerReadModel : ReadModel,
        IHandle<PlayerCreated>,
        IHandle<SkillChosen>,
        IHandle<PlayerLeveledUp>
    {
        public GuidIdentity PlayerId { get; private set; }
        public StringIdentity PlayerTypeId{ get; private set; }
        public IEnumerable<StringIdentity> Skills { get; private set; } = new List<StringIdentity>();
        public int Level { get; set; }
        public override Type GetsCreatedOn => typeof(PlayerCreated);

        public void Handle(PlayerCreated domainEvent)
        {
            PlayerId = (GuidIdentity) domainEvent.EntityId;
            PlayerTypeId = domainEvent.PlayerTypeId;
        }

        public void Handle(SkillChosen domainEvent)
        {
            Skills = Skills.Append(domainEvent.NewSkill);
        }

        public void Handle(PlayerLeveledUp domainEvent)
        {
            Level = domainEvent.NewLevel;
        }
    }
}