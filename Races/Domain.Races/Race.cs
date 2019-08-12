using System.Collections.Generic;
using Domain.Races.DomainEvents;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;

namespace Domain.Races
{
    public class Race : Entity, IApply<RaceCreated>
    {
        public IEnumerable<AllowedPlayer> AllowedPlayers { get; private set; } = new List<AllowedPlayer>();
        public StringIdentity Id { get; private set; }

        public void Apply(RaceCreated raceCreated)
        {
            Id = raceCreated.RaceConfigId;
            AllowedPlayers = raceCreated.AllowedPlayers;
        }
    }
}