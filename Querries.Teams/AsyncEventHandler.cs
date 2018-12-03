using System;
using System.Threading.Tasks;
using Domain.Teams.DomainEvents;
using Microwave.Application.Ports;

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