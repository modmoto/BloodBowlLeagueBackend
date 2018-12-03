using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Domain.Teams.DomainEvents;
using Microwave.Application.Ports;
using Microwave.Domain;

namespace Application.Teams.RaceConfigSeed
{
    public class RaceConfigSeedHandler
    {
        private readonly IEventRepository _eventTypes;
        private readonly IObjectConverter _objectConverter;

        public RaceConfigSeedHandler(IEventRepository eventTypes, IObjectConverter objectConverter)
        {
            _eventTypes = eventTypes;
            _objectConverter = objectConverter;
        }

        public async Task EnsureRaceConfigSeed()
        {
            var eventsInSeedRaw = File.ReadAllText($"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}/RaceConfigSeed/RaceConfigSeed.json");
            var domainEventsInSeed = _objectConverter.Deserialize<IEnumerable<IDomainEvent>>(eventsInSeedRaw);
            var eventsAllreadyAdded = (await _eventTypes.LoadEventsByTypeAsync(nameof(RaceCreated), 0)).Value.Count();
            var remainingEvents = domainEventsInSeed.Skip(eventsAllreadyAdded);
            await _eventTypes.AppendAsync(remainingEvents, eventsAllreadyAdded);
        }
    }
}