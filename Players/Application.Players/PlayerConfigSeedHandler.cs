using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Players;
using Domain.Players.Events.PlayerConfigs;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;
using Microwave.Domain.Results;
using Microwave.EventStores;

namespace Application.Players
{
    public class PlayerConfigSeedHandler
    {
        private readonly IEventRepository _eventRepository;

        public PlayerConfigSeedHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task EnsurePlayerConfigSeed()
        {
            var result = await _eventRepository.LoadEventsByTypeAsync(nameof(PlayerConfigCreated));
            var eventsAllreadyAdded = 0;
            if (result.Is<Ok>()) eventsAllreadyAdded = result.Value.Count();
            var remainingEvents = DomainEventsInSeed.Skip(eventsAllreadyAdded);
            foreach (var domainEvent in remainingEvents)
            {
                await _eventRepository.AppendAsync(new []{ domainEvent }, eventsAllreadyAdded);
            }
        }

        private static IEnumerable<IDomainEvent> DomainEventsInSeed => new List<IDomainEvent>
        {
            new PlayerConfigCreated(StringIdentity.Create("DE_LineMan"),
                new List<StringIdentity>(),
                new List<SkillType>
                {
                    SkillType.General
                },
                new List<SkillType>
                {
                    SkillType.General
                }
            ),
            new PlayerConfigCreated(StringIdentity.Create("DE_Blitzer"),
                new List<StringIdentity>
                {
                    Skills.Block
                },
                new List<SkillType>
                {
                    SkillType.General
                },
                new List<SkillType>
                {
                    SkillType.General
                }
            ),
            new PlayerConfigCreated(StringIdentity.Create("DE_WitchElve"),
                new List<StringIdentity>
                {
                    Skills.Dodge
                },
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

    internal static class Skills
    {
        public static StringIdentity Block => StringIdentity.Create("Block");
        public static StringIdentity Dodge => StringIdentity.Create("Dodge");
    }
}