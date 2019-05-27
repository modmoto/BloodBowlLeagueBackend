using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;

namespace Domain.Teams.DomainEvents
{
    public class PlayerBought : IDomainEvent
    {
        public PlayerBought(
            GuidIdentity teamId,
            StringIdentity playerTypeId,
            GuidIdentity playerId,
            GoldCoins newTeamChestBalance)
        {
            TeamId = teamId;
            NewTeamChestBalance = newTeamChestBalance;
            PlayerTypeId = playerTypeId;
            PlayerId = playerId;
        }

        public Identity EntityId => TeamId;
        public GuidIdentity TeamId { get; }
        public GoldCoins NewTeamChestBalance { get; }
        public StringIdentity PlayerTypeId { get; }
        public GuidIdentity PlayerId { get; }
    }
}