using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Teams.ReadHost.Teams.Events
{
    public class PlayerBought : ISubscribedDomainEvent
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

        public GuidIdentity TeamId { get; }
        public GoldCoins NewTeamChestBalance { get; }
        public StringIdentity PlayerTypeId { get; }
        public GuidIdentity PlayerId { get; }
        public Identity EntityId => TeamId;
    }
}