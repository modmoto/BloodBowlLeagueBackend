using System;
using System.Collections.Generic;
using Microwave.Queries;

namespace Teams.ReadHost.Teams.Events
{
    public class TeamDraftCreated : ISubscribedDomainEvent
    {
        public TeamDraftCreated(
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