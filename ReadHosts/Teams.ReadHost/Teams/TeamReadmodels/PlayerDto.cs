using Microwave.Domain;

namespace Teams.ReadHost.Teams.TeamReadmodels
{
    public class PlayerDto
    {
        public PlayerDto(GuidIdentity playerId, StringIdentity playerTypeId)
        {
            PlayerId = playerId;
            PlayerTypeId = playerTypeId;
        }

        public GuidIdentity PlayerId { get; }
        public StringIdentity PlayerTypeId{ get; }
    }
}