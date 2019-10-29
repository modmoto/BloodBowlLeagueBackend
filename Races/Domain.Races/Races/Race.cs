using System.Collections.Generic;
using Domain.Races.Races.DomainEvents;
using Microwave.Domain.EventSourcing;

namespace Domain.Races.Races
{
    public class Race : Entity, IApply<RaceCreated>
    {
        public IEnumerable<AllowedPlayer> AllowedPlayers { get; private set; } = new List<AllowedPlayer>();
        public string Id { get; private set; }

        public void Apply(RaceCreated domainEvent)
        {
            Id = domainEvent.RaceId;
            AllowedPlayers = domainEvent.AllowedPlayers;
        }
    }
}