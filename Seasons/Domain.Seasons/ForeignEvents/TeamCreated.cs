using Microwave.Domain;

namespace Domain.Seasons.ForeignEvents
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