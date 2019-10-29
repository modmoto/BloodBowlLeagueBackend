using System.Collections.Generic;
using Microwave.Domain.EventSourcing;

namespace Domain.Races.Races.DomainEvents
{
    public class RaceCreated : IDomainEvent
    {
        public RaceCreated(string raceId, IEnumerable<AllowedPlayer> allowedPlayers)
        {
            AllowedPlayers = allowedPlayers;
            RaceId = raceId;
        }

        public IEnumerable<AllowedPlayer> AllowedPlayers { get; }
        public string EntityId => RaceId.ToString();
        public string RaceId { get; }
    }
}