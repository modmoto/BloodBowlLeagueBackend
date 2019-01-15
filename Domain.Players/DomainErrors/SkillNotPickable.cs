using System.Collections.Generic;
using Microwave.Domain;

namespace Domain.Players.DomainErrors
{
    public class SkillNotPickable : DomainError
    {
        public SkillNotPickable(IEnumerable<SkillType> pickableSkills) : base($"Skill is not pickable, can only choose from: {string.Join(",", pickableSkills)}")
        {
        }
    }
}