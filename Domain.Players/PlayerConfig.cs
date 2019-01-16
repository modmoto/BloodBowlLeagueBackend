using System.Collections.Generic;
using Domain.Players.Events;
using Domain.Players.Events.PlayerConfigs;
using Microwave.Domain;

namespace Domain.Players
{
    public class PlayerConfig : Entity
    {
        public void Apply(PlayerConfigCreated configCreated)
        {
            StartingSkills = configCreated.StartingSkills;
            SkillsOnDefault = configCreated.SkillsOnDefault;
            SkillsOnDouble = configCreated.SkillsOnDouble;

        }

        public IEnumerable<StringIdentity> StartingSkills { get; private set; }

        public IEnumerable<SkillType> SkillsOnDefault { get; private set; }

        public IEnumerable<SkillType> SkillsOnDouble { get; private set; }
    }

    public enum SkillType
    {
        General, Agility, Strength, Passing, Mutation, Extraordinary,
        PlusOneAgility,
        PlusOneArmorOrMovement,
        PlusOneStrength
    }
}