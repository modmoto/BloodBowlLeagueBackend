using System;
using Microwave.Queries;

namespace Seasons.ReadHost.Teams.Events
{
    public class TeamCreated : ISubscribedDomainEvent
    {
        public TeamCreated(
            Guid teamId,
            string raceId,
            string teamName,
            string trainerName)
        {
            TeamId = teamId;
            RaceId = raceId;
            TeamName = teamName;
            TrainerName = trainerName;
        }

        public string EntityId => TeamId.ToString();
        public string RaceId { get; }
        public string TeamName { get; }
        public string TrainerName { get; }
        public Guid TeamId { get; }
    }
}