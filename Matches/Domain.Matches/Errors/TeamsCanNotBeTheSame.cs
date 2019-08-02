using Microwave.Domain.Identities;
using Microwave.Domain.Validation;

namespace Domain.Matches.Errors
{
    public class TeamsCanNotBeTheSame : DomainError
    {
        public TeamsCanNotBeTheSame(GuidIdentity teamAtHome, GuidIdentity teamAsGuest)
            : base($"Teams can not be the same {teamAtHome} and {teamAsGuest}")
        {
        }
    }
}