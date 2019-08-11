using Microwave.Domain.Identities;
using Microwave.Queries;
using Seasons.ReadHost.Teams.Events;

namespace Seasons.ReadHost.Teams
{
    public class TeamReadModel : ReadModel<TeamCreated>, IHandle<TeamCreated>
    {
        public StringIdentity RaceId { get; set; }
        public string TrainerName { get; set; }
        public string TeamName { get; set; }
        public GuidIdentity TeamId { get; set; }

        public void Handle(TeamCreated domainEvent)
        {
            TeamId = domainEvent.TeamId;
            RaceId = domainEvent.RaceId;
            TeamName = domainEvent.TeamName;
            TrainerName = domainEvent.TrainerName;
        }
    }
}