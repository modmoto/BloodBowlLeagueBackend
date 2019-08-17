using Microwave.Domain.Validation;

namespace Domain.Teams.DomainErrors
{
    public class CanNotRemovePlayerFromTeam : DomainError
    {
        public CanNotRemovePlayerFromTeam()
            : base("Can not remove players from a this team")
        {
        }
    }
}