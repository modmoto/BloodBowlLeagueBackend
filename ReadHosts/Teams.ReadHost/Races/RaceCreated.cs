using System.Collections.Generic;
using Microwave.Domain;
using Teams.ReadHost.Teams;

namespace Teams.ReadHost.Races
{
    public class RaceCreated : IDomainEvent
    {
        public RaceCreated(StringIdentity raceId, IEnumerable<AllowedPlayer> allowedPlayers, string raceDescription)
        {
            RaceId = raceId;
            AllowedPlayers = allowedPlayers;
            RaceDescription = raceDescription;
        }

        public IEnumerable<AllowedPlayer> AllowedPlayers{ get; set; }
        public string RaceDescription{ get; set; }
        public Identity EntityId => RaceId;
        public StringIdentity RaceId { get; }
    }
}