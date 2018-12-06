using System;
using Microwave.Domain;

namespace Domain.Teams.DomainErrors
{
    public class CanNotUsePlayerInThisRaceError : DomainError
    {
        public CanNotUsePlayerInThisRaceError(Guid playerTypeId) : base($"Can not use playertyp: {playerTypeId} in this team.")
        {
        }
    }
}