using System;

namespace Domain.Players
{
    public enum FreeSkillPoint
    {
        Normal, Double, PlusOneArmorOrMovement, PlusOneAgility, PlusOneStrength
    }

    public class FreeSkillPointFactory
    {
        public FreeSkillPoint Create()
        {
            var random = new Random();
            var firstDice = random.Next(1, 6);
            var secondDice = random.Next(1, 6);
            if (secondDice + firstDice == 12) return FreeSkillPoint.PlusOneStrength;
            if (secondDice + firstDice == 11) return FreeSkillPoint.PlusOneAgility;
            if (secondDice + firstDice == 10) return FreeSkillPoint.PlusOneArmorOrMovement;
            if (secondDice == firstDice) return FreeSkillPoint.Double;
            return FreeSkillPoint.Normal;
        }
    }
}