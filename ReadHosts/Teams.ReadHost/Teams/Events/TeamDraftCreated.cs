namespace Teams.ReadHost.Teams.Events
{
    public class TeamDraftCreated : ISubscribedDomainEvent
    {
        public TeamDraftCreated(
            Guid teamId,
            string raceId,
            string teamName,
            string trainerName,
            IEnumerable<AllowedPlayer> allowedPlayers,
            GoldCoins startingMoney)
        {
            TeamId = teamId;
            RaceId = raceId;
            TeamName = teamName;
            TrainerName = trainerName;
            AllowedPlayers = allowedPlayers;
            StartingMoney = startingMoney;
        }

        public string EntityId => TeamId;
        public string RaceId { get; }
        public string TeamName { get; }
        public string TrainerName { get; }
        public IEnumerable<AllowedPlayer> AllowedPlayers { get; }
        public GoldCoins StartingMoney { get; }
        public Guid TeamId { get; }
    }
}