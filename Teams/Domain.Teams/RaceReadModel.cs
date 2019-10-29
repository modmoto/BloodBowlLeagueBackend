using System.Collections.Generic;
using Domain.Teams.ForeignEvents;
using Microwave.Queries;

namespace Domain.Teams
{
    public class RaceReadModel : ReadModel<RaceCreated>, IHandle<RaceCreated>
    {
        public string RaceConfigId { get; set; }
        public IEnumerable<AllowedPlayer> AllowedPlayers { get; set; }

        public void Handle(RaceCreated domainEvent)
        {
            RaceConfigId = domainEvent.RaceId;
            AllowedPlayers = domainEvent.AllowedPlayers;
        }
    }
}