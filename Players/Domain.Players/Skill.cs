using Domain.Players.Events.Skills;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;

namespace Domain.Players
{
    public class Skill : Entity, IApply<SkillCreated>
    {
        public StringIdentity SkillId { get; private set; }
        public SkillType SkillType { get; private set; }

        public void Apply(SkillCreated skillCreated)
        {
            SkillId = skillCreated.SkillId;
            SkillType = skillCreated.SkillType;
        }
    }
}