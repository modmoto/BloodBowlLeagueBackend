using Microwave.Domain.Identities;

namespace Teams.ReadHost.Teams
{
    public class PlayerDto
    {
        public PlayerDto(GuidIdentity playerId, StringIdentity playerTypeId)
        {
            PlayerId = playerId;
            PlayerTypeId = playerTypeId;
        }

        public GuidIdentity PlayerId { get; }
        public StringIdentity PlayerTypeId { get; }
    }
}