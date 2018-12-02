using System.Collections.Generic;
using Domain.Teams;
using Domain.Teams.DomainEvents;
using Microwave.Queries;

namespace Querries.Teams
{
    public class RaceConfig : IdentifiableQuery, IHandle<RaceCreated>
    {
        public IEnumerable<AllowedPlayer> AllowedPlayers { get; private set; } = new List<AllowedPlayer>();

        public void Handle(RaceCreated raceCreated)
        {
            Id = raceCreated.EntityId;
            AllowedPlayers = raceCreated.AllowedPlayers;
        }
    }
}