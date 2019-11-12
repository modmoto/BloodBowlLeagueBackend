using System;
using Microwave.Domain.EventSourcing;

namespace Domain.Teams.DomainEvents
{
    public class PlayerBought : PlayerBoughtBase
    {
        public PlayerBought(
            Guid teamId,
            string playerTypeId,
            int playerPositionNumber,
            Guid playerId,
            GoldCoins newTeamChestBalance)
            : base(
                teamId,
                playerTypeId,
                playerPositionNumber,
                playerId,
                newTeamChestBalance)
        {
        }
    }
}