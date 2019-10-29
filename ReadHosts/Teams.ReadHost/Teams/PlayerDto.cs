namespace Teams.ReadHost.Teams
{
    public class PlayerDto
    {
        public PlayerDto(Guid playerId, string playerTypeId)
        {
            PlayerId = playerId;
            PlayerTypeId = playerTypeId;
        }

        public Guid PlayerId { get; }
        public string PlayerTypeId { get; }
    }
}