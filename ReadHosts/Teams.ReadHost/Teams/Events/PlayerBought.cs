using System;
using Microwave.Queries;

namespace Teams.ReadHost.Teams.Events
{
    public class PlayerBought : ISubscribedDomainEvent
    {
        public PlayerBought(
            Guid teamId,
            string playerTypeId,
            int playerPositionNumber,
            Guid playerId,
            GoldCoins newTeamChestBalance)
        {
            TeamId = teamId;
            NewTeamChestBalance = newTeamChestBalance;
            PlayerTypeId = playerTypeId;
            PlayerPositionNumber = playerPositionNumber;
            PlayerId = playerId;
        }

        public Guid TeamId { get; }
        public GoldCoins NewTeamChestBalance { get; }
        public string PlayerTypeId { get; }
        public int PlayerPositionNumber { get; }
        public Guid PlayerId { get; }
        public string EntityId => TeamId.ToString();
    }
}