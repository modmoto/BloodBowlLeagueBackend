using Domain.Players.Events;
using Microwave.Domain;

namespace Domain.Players
{
    public class Player : Entity
    {
        public GuidIdentity EntityId { get; private set; }
        public StringIdentity PlayerTypeId { get; private set; }
        public PlayerConfig PlayerConfig { get; private set; }

        public static DomainResult Create(
            GuidIdentity playerId,
            StringIdentity playerTypeId,
            PlayerConfig playerConfig)
        {
            var playerCreated = new PlayerCreated(playerId, playerTypeId, playerConfig);
            return DomainResult.Ok(playerCreated);
        }

        public void Apply(PlayerCreated playerCreated)
        {
            EntityId = (GuidIdentity) playerCreated.EntityId;
            PlayerTypeId = playerCreated.PlayerTypeId;
            PlayerConfig = playerCreated.PlayerConfig;
        }
    }
}