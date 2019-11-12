using System;

namespace Domain.Teams
{
    public class PlayerReadModel
    {
        public PlayerReadModel(
            Guid playerId,
            string playerTypeId,
            int playerPositionNumber)
        {
            PlayerTypeId = playerTypeId;
            PlayerPositionNumber = playerPositionNumber;
            PlayerId = playerId;
        }

        public string PlayerTypeId { get; }
        public int PlayerPositionNumber { get; }
        public Guid PlayerId { get; }
    }
}