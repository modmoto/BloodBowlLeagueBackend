using System.Collections.Generic;
using System.Linq;
using Domain.Players.DomainErrors;
using Domain.Players.Events.Players;
using Microwave.Domain;

namespace Domain.Players
{
    public class Player : Entity, IApply<PlayerCreated>, IApply<PlayerLeveledUp>, IApply<SkillPicked>
    {
        public GuidIdentity EntityId { get; private set; }
        public StringIdentity PlayerTypeId { get; private set; }
        public PlayerConfig PlayerConfig { get; private set; }
        public IEnumerable<FreeSkillPoint> FreeSkillPoints { get; private set; } = new List<FreeSkillPoint>();
        public IEnumerable<StringIdentity> CurrentSkills { get; private set; } = new List<StringIdentity>();

        public static DomainResult Create(
            GuidIdentity playerId,
            StringIdentity playerTypeId,
            PlayerConfig playerConfig)
        {
            var playerCreated = new PlayerCreated(playerId, playerTypeId, playerConfig);
            return DomainResult.Ok(playerCreated);
        }

        public DomainResult LevelUp(Skill newSkill)
        {
            if (!FreeSkillPoints.Any()) return DomainResult.Error(new NoLevelUpsAvailable());
            if (CurrentSkills.Any(s => s == newSkill.SkillId)) return DomainResult.Error(new CanNotPickSkillTwice(CurrentSkills));

            foreach (var freeSkillType in FreeSkillPoints)
            {
                var isPossible = false;
                switch (freeSkillType)
                {
                    case FreeSkillPoint.Normal:
                    {
                        if (PlayerConfig.SkillsOnDefault.Contains(newSkill.SkillType)) isPossible = true;
                        break;
                    }
                    case FreeSkillPoint.Double:
                    {
                        if (PlayerConfig.SkillsOnDefault.Contains(newSkill.SkillType)) isPossible = true;
                        if (PlayerConfig.SkillsOnDouble.Contains(newSkill.SkillType)) isPossible = true;
                        break;
                    }
                    case FreeSkillPoint.PlusOneArmorOrMovement:
                    {
                        if (PlayerConfig.SkillsOnDefault.Contains(newSkill.SkillType)) isPossible = true;
                        if (PlayerConfig.SkillsOnDouble.Contains(newSkill.SkillType)) isPossible = true;
                        if (newSkill.SkillType == SkillType.PlusOneArmorOrMovement) isPossible = true;
                        break;
                    }
                    case FreeSkillPoint.PlusOneAgility:
                    {
                        if (PlayerConfig.SkillsOnDefault.Contains(newSkill.SkillType)) isPossible = true;
                        if (PlayerConfig.SkillsOnDouble.Contains(newSkill.SkillType)) isPossible = true;
                        if (newSkill.SkillType == SkillType.PlusOneArmorOrMovement) isPossible = true;
                        if (newSkill.SkillType == SkillType.PlusOneAgility) isPossible = true;
                        break;
                    }
                    case FreeSkillPoint.PlusOneStrength:
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

        public void Apply(SkillPicked domainEvent)
        {
            CurrentSkills = CurrentSkills.Append(domainEvent.NewSkill);
            FreeSkillPoints = domainEvent.RemainingLevelUps;
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
    }
}