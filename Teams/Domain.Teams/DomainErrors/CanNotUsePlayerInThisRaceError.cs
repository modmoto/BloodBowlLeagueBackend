using Microwave.Domain.Identities;
using Microwave.Domain.Validation;

namespace Domain.Teams.DomainErrors
{
    public class CanNotUsePlayerInThisRaceError : DomainError
    {
        public CanNotUsePlayerInThisRaceError(Identity playerTypeId) : base($"Can not use playertyp: {playerTypeId.Id} in this team.")
        {
        }
    }
}