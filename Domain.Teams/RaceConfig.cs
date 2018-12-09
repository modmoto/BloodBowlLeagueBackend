using System;
using System.Collections.Generic;
using Domain.Teams.DomainEvents;
using Microwave.Domain;

namespace Domain.Teams
{
    public class RaceConfig : Entity
    {
        public IEnumerable<AllowedPlayer> AllowedPlayers { get; private set; } = new List<AllowedPlayer>();
        public Guid Id { get; private set; }

        public void Apply(RaceCreated raceCreated)
        {
            Id = raceCreated.EntityId;
            AllowedPlayers = raceCreated.AllowedPlayers;
        }
    }
}