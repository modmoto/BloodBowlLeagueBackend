using System.Collections.Generic;
using Domain.Teams.DomainEvents;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Domain.Teams
{
    public class RaceReadModel :
        ReadModel<RaceCreated>,
        IHandle<RaceCreated>
    {
        public IEnumerable<AllowedPlayer> AllowedPlayers { get; private set; } = new List<AllowedPlayer>();
        public StringIdentity RaceId { get; private set; }

        public void Handle(RaceCreated raceCreated)
        {
            RaceId = raceCreated.RaceId;
            AllowedPlayers = raceCreated.AllowedPlayers;
        }
    }
}