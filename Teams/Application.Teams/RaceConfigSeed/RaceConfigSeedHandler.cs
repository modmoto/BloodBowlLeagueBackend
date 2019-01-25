﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Teams;
using Domain.Teams.DomainEvents;
using Microwave.Application.Results;
using Microwave.Domain;
using Microwave.EventStores.Ports;

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
            var result = await _eventTypes.LoadEventsByTypeAsync(nameof(RaceCreated));
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
            new RaceCreated(StringIdentity.Create("DarkElves"), new List<AllowedPlayer>
            {
                new AllowedPlayer(StringIdentity.Create("DE_LineMan"), 16, new GoldCoins(70000), "Lineman"),
                new AllowedPlayer(StringIdentity.Create("DE_Assassine"), 2, new GoldCoins(90000), "Assasine"),
                new AllowedPlayer(StringIdentity.Create("DE_Blitzer"), 4, new GoldCoins(100000), "Blitzer"),
                new AllowedPlayer(StringIdentity.Create("DE_WitchElve"), 2, new GoldCoins(110000), "Witch Elve")
            }, "Dark Elves"),

            new RaceCreated(StringIdentity.Create("Humans"), new List<AllowedPlayer>
            {
                new AllowedPlayer(StringIdentity.Create("HU_LineMan"), 16, new GoldCoins(50000), "Lineman"),
                new AllowedPlayer(StringIdentity.Create("HU_Blitzer"), 4, new GoldCoins(90000), "Blitzer"),
                new AllowedPlayer(StringIdentity.Create("HU_Catcher"), 4, new GoldCoins(70000), "Catcher"),
                new AllowedPlayer(StringIdentity.Create("HU_Thrower"), 2, new GoldCoins(70000), "Thrower"),
                new AllowedPlayer(StringIdentity.Create("HU_Ogre"), 1, new GoldCoins(70000), "Ogre")
            }, "Humans")

        };
    }
}