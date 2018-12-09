using Microwave.Queries;
using Querries.Teams.DomainEvents;

namespace Querries.Teams
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