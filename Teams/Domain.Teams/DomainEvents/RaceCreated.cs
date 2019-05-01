using System.Collections.Generic;
using Microwave.Domain;

namespace Domain.Teams.DomainEvents
{
    public class RaceCreated : IDomainEvent
    {
        public RaceCreated(StringIdentity raceConfigId, IEnumerable<AllowedPlayer> allowedPlayers, string raceDescription)
        {
            AllowedPlayers = allowedPlayers;
            RaceDescription = raceDescription;
            RaceConfigId = raceConfigId;
        }

        public IEnumerable<AllowedPlayer> AllowedPlayers { get; }
        public string RaceDescription { get; }
        public Identity EntityId => RaceConfigId;
        public StringIdentity RaceConfigId { get; }
    }
}