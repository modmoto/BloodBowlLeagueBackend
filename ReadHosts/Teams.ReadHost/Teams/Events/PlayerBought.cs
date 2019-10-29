namespace Teams.ReadHost.Teams.Events
{
    public class PlayerBought : ISubscribedDomainEvent
    {
        public PlayerBought(
            Guid teamId,
            string playerTypeId,
            Guid playerId,
            GoldCoins newTeamChestBalance)
        {
            TeamId = teamId;
            NewTeamChestBalance = newTeamChestBalance;
            PlayerTypeId = playerTypeId;
            PlayerId = playerId;
        }

        public Guid TeamId { get; }
        public GoldCoins NewTeamChestBalance { get; }
        public string PlayerTypeId { get; }
        public Guid PlayerId { get; }
        public string EntityId => TeamId;
    }
}