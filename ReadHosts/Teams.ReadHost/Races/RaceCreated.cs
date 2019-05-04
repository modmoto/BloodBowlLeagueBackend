﻿using System.Collections.Generic;
using Microwave.Domain;
using Teams.ReadHost.Teams;

namespace Teams.ReadHost.Races
{
    public class RaceCreated
    {
        public RaceCreated(StringIdentity raceId, IEnumerable<AllowedPlayer> allowedPlayers, string raceDescription)
        {
            RaceId = raceId;
            AllowedPlayers = allowedPlayers;
            RaceDescription = raceDescription;
        }

        public IEnumerable<AllowedPlayer> AllowedPlayers{ get; }
        public string RaceDescription{ get; }
        public StringIdentity RaceId { get; }
    }
}