using System;
using Microwave.Domain;

namespace Domain.Players
{
    public class Player : Entity
    {
        public GuidIdentity EntityId { get; }
        public StringIdentity PlayerTypeId { get; }
        public GuidIdentity TeamId { get; }

        private Player(GuidIdentity entityId, StringIdentity playerTypeId, GuidIdentity teamId)
        {
            EntityId = entityId;
            PlayerTypeId = playerTypeId;
            TeamId = teamId;
        }

        public static DomainResult Create(GuidIdentity teamId, StringIdentity playerTypeId, PlayerConfig playerConfig)
        {
            var playerCreated = new PlayerCreated(GuidIdentity.Create(Guid.NewGuid()), playerTypeId, teamId);
            return DomainResult.Ok(playerCreated);
        }
    }

    public class PlayerCreated : IDomainEvent
    {
        public PlayerCreated(GuidIdentity entityId, StringIdentity playerTypeId, GuidIdentity teamId)
        {
            EntityId = entityId;
            PlayerTypeId = playerTypeId;
            TeamId = teamId;
        }

        public Identity EntityId { get; }
        public StringIdentity PlayerTypeId { get; }
        public GuidIdentity TeamId { get; }
    }
}