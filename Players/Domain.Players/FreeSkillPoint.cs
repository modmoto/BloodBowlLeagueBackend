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
            switch (secondDice + firstDice)
            {
                case 12:
                    return FreeSkillPoint.PlusOneStrength;
                case 11:
                    return FreeSkillPoint.PlusOneAgility;
                case 10:
                    return FreeSkillPoint.PlusOneArmorOrMovement;
            }

            return secondDice == firstDice ? FreeSkillPoint.Double : FreeSkillPoint.Normal;
        }
    }
}