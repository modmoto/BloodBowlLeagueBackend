using Microwave.Domain.Identities;

namespace Domain.Teams
{
    public class PlayerReadModel
    {
        public PlayerReadModel(
            GuidIdentity playerId,
            StringIdentity playerTypeId)
        {
            PlayerTypeId = playerTypeId;
            PlayerId = playerId;
        }
        public StringIdentity PlayerTypeId { get; }
        public GuidIdentity PlayerId { get; }
    }
}