﻿using Teams.ReadHost.Teams;

namespace Teams.ReadHost.Races
{
    public class RaceCreated : ISubscribedDomainEvent
    {
        public RaceCreated(string raceId, IEnumerable<AllowedPlayer> allowedPlayers)
        {
            AllowedPlayers = allowedPlayers;
            RaceId = raceId;
        }

        public IEnumerable<AllowedPlayer> AllowedPlayers { get; }
        public string EntityId => RaceId;
        public string RaceId { get; }
    }
}