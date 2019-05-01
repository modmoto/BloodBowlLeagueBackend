using Microwave.Domain;

namespace Domain.Matches.ForeignEvents
{
    public class TeamCreated
    {
        public TeamCreated(GuidIdentity teamId)
        {
            TeamId = teamId;
        }
        public GuidIdentity TeamId { get; }
    }
}