using Microwave.Domain;

namespace Domain.Teams.DomainErrors
{
    public class CanNotUsePlayerInThisRaceError : DomainError
    {
        public CanNotUsePlayerInThisRaceError(Identity playerTypeId) : base($"Can not use playertyp: {playerTypeId.Id} in this team.")
        {
        }
    }
}