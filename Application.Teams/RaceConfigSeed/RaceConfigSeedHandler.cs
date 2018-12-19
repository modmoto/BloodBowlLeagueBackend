using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Domain.Teams.DomainEvents;
using Microwave.Domain;
using Microwave.Queries;
using Newtonsoft.Json;

namespace Application.Teams.RaceConfigSeed
{
    public class RaceConfigSeedHandler
    {
        private readonly IEventRepository _eventTypes;

        public RaceConfigSeedHandler(IEventRepository eventTypes)
        {
            _eventTypes = eventTypes;
        }

        public async Task EnsureRaceConfigSeed()
        {
            var eventsInSeedRaw = File.ReadAllText($"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}/RaceConfigSeed/RaceConfigSeed.json");
            var domainEventsInSeed = JsonConvert.DeserializeObject<IEnumerable<IDomainEvent>>(eventsInSeedRaw,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });
            var result = await _eventTypes.LoadEventsByTypeAsync(nameof(RaceCreated), 0);
            var eventsAllreadyAdded = result.Value.Count();
            var remainingEvents = domainEventsInSeed.Skip(eventsAllreadyAdded);
            await _eventTypes.AppendAsync(remainingEvents, eventsAllreadyAdded);
        }
    }
}