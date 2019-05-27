using System.Collections.Generic;
using Microwave.Domain.Validation;

namespace Domain.Players.DomainErrors
{
    public class SkillNotPickable : DomainError
    {
        public SkillNotPickable(IEnumerable<FreeSkillPoint> pickableSkills) : base($"Skill is not pickable, can only choose from: {string.Join(",", pickableSkills)}")
        {
        }
    }
}