using System;

namespace Domain.Teams
{
    public class PlayerReadModel
    {
        public PlayerReadModel(
            Guid playerId,
            string playerTypeId)
        {
            PlayerTypeId = playerTypeId;
            PlayerId = playerId;
        }

        public string PlayerTypeId { get; }
        public Guid PlayerId { get; }
    }
}