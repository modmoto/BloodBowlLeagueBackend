namespace Teams.ReadHost.Players.Events
{
    public class PlayerCreated : ISubscribedDomainEvent
    {
        public PlayerCreated(
            Guid playerId,
            PlayerConfig playerConfig,
            Guid teamId,
            string name)
        {
            PlayerId = playerId;
            PlayerConfig = playerConfig;
            TeamId = teamId;
            Name = name;
        }

        public Guid PlayerId { get; }
        public PlayerConfig PlayerConfig { get; }
        public string PlayerTypeId => PlayerConfig.PlayerTypeId;
        public Guid TeamId { get; }
        public string EntityId => PlayerId;
        public string Name { get;  }
    }
}