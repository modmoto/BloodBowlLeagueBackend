using System.Collections.Generic;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;

namespace Domain.Races.Races.DomainEvents
{
    public class RaceCreated : IDomainEvent
    {
        public RaceCreated(StringIdentity raceId, IEnumerable<AllowedPlayer> allowedPlayers)
        {
            AllowedPlayers = allowedPlayers;
            RaceId = raceId;
        }

        public IEnumerable<AllowedPlayer> AllowedPlayers { get; }

        public Identity EntityId => RaceId;
        public StringIdentity RaceId { get; }
    }
}