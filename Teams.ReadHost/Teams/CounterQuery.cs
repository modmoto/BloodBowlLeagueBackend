using Microwave.Queries;

namespace Teams.ReadHost.Teams
{
    public class CounterQuery : Query, IHandle<TeamCreated>
    {
        public int CreatedCount { get; set; }

        public void Handle(TeamCreated domainEvent)
        {
            CreatedCount = CreatedCount + 1;
        }
    }
}