using System;
using Microwave.Domain;

namespace Application.Players.Skills
{
    public abstract class Skill
    {
        public Skill(SkillType type, string description)
        {
            Type = type;
            Description = description;
        }

        public SkillType Type { get; }
        public string Description { get; }

        public static bool operator== (Skill id1, Skill id2)
        {
            return id1?.GetType() == id2?.GetType();
        }

        public static bool operator!= (Skill id1, Skill id2)
        {
            return id1?.Equals(id2) == false;
        }

        public override bool Equals(Object obj)
        {
            var identity = obj as Skill;
            return Equals(identity);
        }
    }
}