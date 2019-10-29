using System.Collections.Generic;
using Domain.Players.Events.ForeignEvents;
using Microwave.Queries;

namespace Domain.Players
{
    public class RaceReadModel : ReadModel<RaceCreated>, IHandle<RaceCreated>
    {
        public IEnumerable<AllowedPlayer> AllowedPlayers { get; private set; } = new List<AllowedPlayer>();
        public string RaceId { get; private set; }

        public void Handle(RaceCreated domainEvent)
        {
            RaceId = domainEvent.RaceId;
            AllowedPlayers = domainEvent.AllowedPlayers;
        }
    }
}