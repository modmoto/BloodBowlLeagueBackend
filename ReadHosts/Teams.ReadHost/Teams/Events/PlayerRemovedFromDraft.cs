namespace Teams.ReadHost.Teams.Events
{
    public class PlayerRemovedFromDraft : ISubscribedDomainEvent
    {
        public Guid TeamId { get; }
        public string EntityId => TeamId;
        public Guid PlayerId { get; }
        public GoldCoins NewTeamChestBalance { get; }

        public PlayerRemovedFromDraft(Guid teamId, Guid playerId, GoldCoins newTeamChestBalance)
        {
            TeamId = teamId;
            PlayerId = playerId;
            NewTeamChestBalance = newTeamChestBalance;
        }
    }
}