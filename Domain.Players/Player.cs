using System.Collections.Generic;
using System.Linq;
using Domain.Players.DomainErrors;
using Domain.Players.Events;
using Microwave.Domain;

namespace Domain.Players
{
    public class Player : Entity
    {
        public GuidIdentity EntityId { get; private set; }
        public StringIdentity PlayerTypeId { get; private set; }
        public PlayerConfig PlayerConfig { get; private set; }
        public IEnumerable<LevelUpType> FreeSkillPoints { get; private set; } = new List<LevelUpType>();
        public IEnumerable<StringIdentity> CurrentSkills { get; private set; } = new List<StringIdentity>();

        public static DomainResult Create(
            GuidIdentity playerId,
            StringIdentity playerTypeId,
            PlayerConfig playerConfig)
        {
            var playerCreated = new PlayerCreated(playerId, playerTypeId, playerConfig);
            return DomainResult.Ok(playerCreated);
        }

        public void Apply(PlayerCreated playerCreated)
        {
            EntityId = (GuidIdentity) playerCreated.EntityId;
            PlayerTypeId = playerCreated.PlayerTypeId;
            CurrentSkills = playerCreated.PlayerConfig.StartingSkills;
            PlayerConfig = playerCreated.PlayerConfig;
        }

        public void Apply(PlayerLeveledUp leveledUp)
        {
            FreeSkillPoints = leveledUp.FreeSkillPoints;
        }

        public DomainResult LevelUp(Skill newSkill)
        {
            if (!FreeSkillPoints.Any()) return DomainResult.Error(new NoLevelUpsAvailable());

            foreach (var freeSkillType in FreeSkillPoints)
            {
                var isPossible = false;
                switch (freeSkillType)
                {
                    case LevelUpType.Normal:
                    {
                        if (PlayerConfig.SkillsOnDefault.Contains(newSkill.SkillType)) isPossible = true;
                        break;
                    }
                    case LevelUpType.Double:
                    {
                        if (PlayerConfig.SkillsOnDefault.Contains(newSkill.SkillType)) isPossible = true;
                        if (PlayerConfig.SkillsOnDouble.Contains(newSkill.SkillType)) isPossible = true;
                        break;
                    }
                    case LevelUpType.PlusOneArmorOrMovement:
                    {
                        if (PlayerConfig.SkillsOnDefault.Contains(newSkill.SkillType)) isPossible = true;
                        if (PlayerConfig.SkillsOnDouble.Contains(newSkill.SkillType)) isPossible = true;
                        if (newSkill.SkillType == SkillType.PlusOneArmorOrMovement) isPossible = true;
                        break;
                    }
                    case LevelUpType.PlusOneAgility:
                    {
                        if (PlayerConfig.SkillsOnDefault.Contains(newSkill.SkillType)) isPossible = true;
                        if (PlayerConfig.SkillsOnDouble.Contains(newSkill.SkillType)) isPossible = true;
                        if (newSkill.SkillType == SkillType.PlusOneArmorOrMovement) isPossible = true;
                        if (newSkill.SkillType == SkillType.PlusOneAgility) isPossible = true;
                        break;
                    }
                    case LevelUpType.PlusOneStrength:
                        isPossible = true;
                        break;
                }


                if (isPossible)
                {
                    var skillTypes = FreeSkillPoints.ToList();
                    skillTypes.Remove(freeSkillType);
                    return DomainResult.Ok(new SkillPicked(EntityId, newSkill.SkillId, skillTypes));
                }
            }

            return DomainResult.Error(new SkillNotPickable(FreeSkillPoints));
        }
    }

    public enum LevelUpType
    {
        Normal, Double, PlusOneArmorOrMovement, PlusOneAgility, PlusOneStrength
    }
}