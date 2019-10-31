using System.Collections.Generic;
using Microwave.Domain.Validation;

namespace Domain.Players
{
    public class CanNotPickSkillTwice : DomainError
    {
        public CanNotPickSkillTwice(IEnumerable<string> skillesPicked)
            : base($"You can not pick the same skill twice. Allready picked are: {string.Join(",", skillesPicked)}")
        {
        }
    }
}