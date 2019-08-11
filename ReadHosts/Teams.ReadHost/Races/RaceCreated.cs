using System.Collections.Generic;
using Microwave.Domain.Identities;
using Microwave.Queries;
using Teams.ReadHost.Teams;

namespace Teams.ReadHost.Races
{
    public class RaceCreated : ISubscribedDomainEvent
    {
        public RaceCreated(StringIdentity raceConfigId, IEnumerable<AllowedPlayer> allowedPlayers)
        {
            AllowedPlayers = allowedPlayers;
            RaceConfigId = raceConfigId;
        }

        public IEnumerable<AllowedPlayer> AllowedPlayers { get; }
        public Identity EntityId => RaceConfigId;
        public StringIdentity RaceConfigId { get; }
    }
}