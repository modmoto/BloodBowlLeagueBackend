using System.Collections.Generic;
using Domain.Players.Events;
using Microwave.Domain;

namespace Domain.Players
{
    public class PlayerConfig : Entity
    {
        public void Apply(PlayerConfigCreated configCreated)
        {
            CurrentSkills = configCreated.StartingSkills;
            SkillsOnDefault = configCreated.SkillsOnDefault;
            SkillsOnDouble = configCreated.SkillsOnDouble;

        }

        public IEnumerable<StringIdentity> CurrentSkills { get; set; }

        public IEnumerable<SkillType> SkillsOnDefault { get; set; }

        public IEnumerable<SkillType> SkillsOnDouble { get; set; }
    }

    public enum SkillType
    {
        General, Agility, Strength, Passing, Mutation, Extraordinary
    }
}