using System;
using Domain.Players.Events;
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

        public static DomainResult Create(
            GuidIdentity teamId,
            GuidIdentity playerId,
            StringIdentity playerTypeId,
            PlayerConfig playerConfig)
        {
            var playerCreated = new PlayerCreated(playerId, playerTypeId, teamId, playerConfig);
            return DomainResult.Ok(playerCreated);
        }
    }
}