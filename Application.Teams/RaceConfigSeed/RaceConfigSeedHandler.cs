using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Domain.Teams.DomainEvents;
using Microwave.Application;
using Microwave.Domain;

namespace Application.Teams.RaceConfigSeed
{
    public class RaceConfigSeedHandler
    {
        private readonly ITypeProjectionRepository _eventTypes;
        private readonly IObjectConverter _objectConverter;

        public RaceConfigSeedHandler(ITypeProjectionRepository eventTypes, IObjectConverter objectConverter)
        {
            _eventTypes = eventTypes;
            _objectConverter = objectConverter;
        }

        public async Task EnsureRaceConfigSeed()
        {
            var eventsInSeedRaw = File.ReadAllText($"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}/RaceConfigSeed/RaceConfigSeed.json");
            var domainEventsInSeed = _objectConverter.Deserialize<IEnumerable<IDomainEvent>>(eventsInSeedRaw);
            var eventsAllreadyAdded = (await _eventTypes.LoadEventsByTypeAsync(nameof(RaceCreated))).Value.Count();
            var remainingEvents = domainEventsInSeed.Skip(eventsAllreadyAdded);
            foreach (var remainingEvent in remainingEvents)
            {
                await _eventTypes.AppendToTypeStream(remainingEvent);
            }
        }
    }
}