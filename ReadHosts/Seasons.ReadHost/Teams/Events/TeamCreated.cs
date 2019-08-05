using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Seasons.ReadHost.Teams.Events
{
    public class TeamCreated : ISubscribedDomainEvent
    {
        public TeamCreated(
            GuidIdentity teamId,
            StringIdentity raceId,
            string teamName,
            string trainerName)
        {
            TeamId = teamId;
            RaceId = raceId;
            TeamName = teamName;
            TrainerName = trainerName;
        }

        public Identity EntityId => TeamId;
        public StringIdentity RaceId { get; }
        public string TeamName { get; }
        public string TrainerName { get; }
        public GuidIdentity TeamId { get; }
    }
}