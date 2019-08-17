using Microwave.Domain.Validation;

namespace Domain.Teams.DomainErrors
{
    public class CanOnlyRemovePlayerFromDraftTeam : DomainError
    {
        public CanOnlyRemovePlayerFromDraftTeam()
            : base("Can only remove players from a draft team, this team is allready finished")
        {
        }
    }
}