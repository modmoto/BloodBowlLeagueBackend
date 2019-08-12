using System.Collections.Generic;
using Domain.Players.Events.ForeignEvents;
using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Domain.Players
{
    public class RaceReadModel : ReadModel<RaceCreated>, IHandle<RaceCreated>
    {
        public IEnumerable<AllowedPlayer> AllowedPlayers { get; private set; } = new List<AllowedPlayer>();
        public StringIdentity Id { get; private set; }

        public void Handle(RaceCreated raceCreated)
        {
            Id = raceCreated.RaceConfigId;
            AllowedPlayers = raceCreated.AllowedPlayers;
        }
    }
}