using System.Collections.Generic;
using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Domain.Teams.DomainEvents
{
    public class RaceCreated : ISubscribedDomainEvent
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