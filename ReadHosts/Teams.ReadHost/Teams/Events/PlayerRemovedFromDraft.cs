using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Teams.ReadHost.Teams.Events
{
    public class PlayerRemovedFromDraft : ISubscribedDomainEvent
    {
        public GuidIdentity TeamId { get; }
        public Identity EntityId => TeamId;
        public GuidIdentity PlayerId { get; }
        public GoldCoins NewTeamChestBalance { get; }

        public PlayerRemovedFromDraft(GuidIdentity teamId, GuidIdentity playerId, GoldCoins newTeamChestBalance)
        {
            TeamId = teamId;
            PlayerId = playerId;
            NewTeamChestBalance = newTeamChestBalance;
        }
    }
}