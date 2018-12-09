using System;
using System.Threading.Tasks;
using Microwave.Queries;
using Querries.Teams.DomainEvents;

namespace Querries.Teams
{
    public class AsyncEventHandler : IHandleAsync<TeamCreated>
    {
        public Task HandleAsync(TeamCreated domainEvent)
        {
            Console.WriteLine("dasda");
            return Task.CompletedTask;
        }
    }
}