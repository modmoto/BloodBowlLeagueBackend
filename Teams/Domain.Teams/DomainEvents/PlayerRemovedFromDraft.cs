using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;

namespace Domain.Teams.DomainEvents
{
    public class PlayerRemovedFromDraft : IDomainEvent
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