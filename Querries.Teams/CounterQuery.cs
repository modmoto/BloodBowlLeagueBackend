using Domain.Teams.DomainEvents;
using Microwave.Queries;

namespace Querries.Teams
{
    public class CounterQuery : Query, IHandle<TeamCreated>
    {
        public int CreatedCount { get; set; }

        public void Handle(TeamCreated domainEvent)
        {
            CreatedCount = CreatedCount + 1;
        }

        //nuget delete Microwave.Queries 1.0.0.0 -ApiKey e7jrjjfqck2ajelalqvp5sl6 -Source https://ci.appveyor.com/nuget/lauchi-jwih9ne6cq4t/api/v2/package
    }
}