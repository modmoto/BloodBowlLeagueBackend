using System;

namespace Teams.ReadHost.Teams
{
    public class PlayerDto
    {
        public PlayerDto(
            Guid playerId,
            string playerTypeId,
            int playerPositionNumber)
        {
            PlayerId = playerId;
            PlayerTypeId = playerTypeId;
            PlayerPositionNumber = playerPositionNumber;
        }

        public Guid PlayerId { get; }
        public string PlayerTypeId { get; }
        public int PlayerPositionNumber { get; }
    }
}