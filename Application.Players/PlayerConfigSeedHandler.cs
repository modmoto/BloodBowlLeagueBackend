using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Players.Events;
using Application.Players.Skills;
using Microwave.Application.Results;
using Microwave.Domain;
using Microwave.EventStores.Ports;

namespace Application.Players
{
    public class PlayerConfigSeedHandler
    {
        private readonly IEventRepository _eventTypes;

        public PlayerConfigSeedHandler(IEventRepository eventTypes)
        {
            _eventTypes = eventTypes;
        }

        public async Task EnsureRaceConfigSeed()
        {
            var result = await _eventTypes.LoadEventsByTypeAsync(nameof(PlayerConfigCreated));
            var eventsAllreadyAdded = 0;
            if (result.Is<Ok>()) eventsAllreadyAdded = result.Value.Count();
            var remainingEvents = DomainEventsInSeed.Skip(eventsAllreadyAdded);
            foreach (var domainEvent in remainingEvents)
            {
                await _eventTypes.AppendAsync(new []{ domainEvent }, eventsAllreadyAdded);
            }
        }

        private static IEnumerable<IDomainEvent> DomainEventsInSeed => new List<IDomainEvent>
        {
            new PlayerConfigCreated(StringIdentity.Create("DE_LineMan"),
                new List<Skill>(),
                new List<SkillType>
                {
                    SkillType.General
                },
                new List<SkillType>
                {
                    SkillType.General
                }
            )
        };
    }
}