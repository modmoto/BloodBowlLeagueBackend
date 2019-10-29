using Teams.ReadHost.Teams;

namespace Teams.ReadHost.Races
{
    public class RaceReadModel : ReadModel<RaceCreated>, IHandle<RaceCreated>
    {
        public string RaceConfigId { get; set; }
        public IEnumerable<AllowedPlayer> AllowedPlayers { get; set; }

        public void Handle(RaceCreated domainEvent)
        {
            RaceConfigId = domainEvent.RaceId;
            AllowedPlayers = domainEvent.AllowedPlayers;
        }
    }
}