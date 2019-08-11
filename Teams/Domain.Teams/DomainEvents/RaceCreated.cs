using System.Collections.Generic;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;

namespace Domain.Teams.DomainEvents
{
    public class RaceCreated : IDomainEvent
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