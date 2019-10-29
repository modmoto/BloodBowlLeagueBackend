using Microwave.Domain.Validation;

namespace Domain.Teams.DomainErrors
{
    public class CanNotUsePlayerInThisRaceError : DomainError
    {
        public CanNotUsePlayerInThisRaceError(string playerTypeId) : base($"Can not use playertyp: {playerTypeId} in this team.")
        {
        }
    }
}