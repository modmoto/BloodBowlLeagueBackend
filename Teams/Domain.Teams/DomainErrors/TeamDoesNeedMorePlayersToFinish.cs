using Microwave.Domain.Validation;

namespace Domain.Teams.DomainErrors
{
    public class TeamDoesNeedMorePlayersToFinish : DomainError
    {
        public TeamDoesNeedMorePlayersToFinish(int count)
            : base($"Team needs at least 11 players, currently only has {count}")
        {
        }
    }
}