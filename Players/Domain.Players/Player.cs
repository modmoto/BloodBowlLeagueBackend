using System.Collections.Generic;
using System.Linq;
using Domain.Players.DomainErrors;
using Domain.Players.Events.Players;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;
using Microwave.Domain.Validation;

namespace Domain.Players
{
    public class Player : Entity, IApply<PlayerCreated>,
        IApply<PlayerLeveledUp>,
        IApply<SkillChosen>,
        IApply<PlayerPassed>,
        IApply<PlayerWasNominatedMostValuablePlayer>,
        IApply<PlayerMadeTouchdown>,
        IApply<PlayerMadeCasualty>
    {
        public GuidIdentity PlayerId { get; private set; }
        public StringIdentity PlayerTypeId { get; private set; }
        public PlayerConfig PlayerConfig { get; private set; }
        public IEnumerable<FreeSkillPoint> FreeSkillPoints { get; private set; } = new List<FreeSkillPoint>();
        public IEnumerable<Skill> CurrentSkills { get; private set; } = new List<Skill>();
        public long StarPlayerPoints { get; private set; }

        public static DomainResult Create(
            GuidIdentity playerId,
            GuidIdentity teamId,
            StringIdentity playerTypeId,
            PlayerConfig playerConfig)
        {
            var playerCreated = new PlayerCreated(playerId, teamId, playerTypeId, playerConfig);
            return DomainResult.Ok(playerCreated);
        }

        public DomainResult ChooseSkill(Skill newSkill)
        {
            if (!FreeSkillPoints.Any()) return DomainResult.Error(new NoLevelUpsAvailable());
            if (CurrentSkills.Any(s => s.Equals(newSkill))) return DomainResult.Error(
                new CanNotPickSkillTwice(CurrentSkills.Select(s => s.SkillId)));

            foreach (var freeSkillType in FreeSkillPoints)
            {
                if (!HasPlayerFreeSkillForChosenSkill(newSkill, freeSkillType)) continue;

                var skillTypes = FreeSkillPoints.ToList();
                skillTypes.Remove(freeSkillType);
                return DomainResult.Ok(new SkillChosen(PlayerId, newSkill, skillTypes));
            }

            return DomainResult.Error(new SkillNotPickable(FreeSkillPoints));
        }

        private bool HasPlayerFreeSkillForChosenSkill(Skill newSkill, FreeSkillPoint freeSkillType)
        {
            switch (freeSkillType)
            {
                case FreeSkillPoint.Normal:
                    return PlayerCanPickNormalSkill(newSkill);
                case FreeSkillPoint.Double:
                    return PlayerCanPickDoubleSkill(newSkill);
                case FreeSkillPoint.PlusOneArmorOrMovement:
                    return PlayerCanPickArmorOrMovement(newSkill);
                case FreeSkillPoint.PlusOneAgility:
                    return PlayerCanPickAgility(newSkill);
                case FreeSkillPoint.PlusOneStrength:
                    return true;
                default:
                    return false;
            }
        }

        private bool PlayerCanPickNormalSkill(Skill newSkill)
        {
            return PlayerConfig.SkillsOnDefault.Contains(newSkill.SkillType);
        }

        private bool PlayerCanPickAgility(Skill newSkill)
        {
            if (PlayerConfig.SkillsOnDefault.Contains(newSkill.SkillType)) return true;
            if (PlayerConfig.SkillsOnDouble.Contains(newSkill.SkillType)) return true;
            if (newSkill.SkillType == SkillType.PlusOneArmorOrMovement) return true;
            if (newSkill.SkillType == SkillType.PlusOneAgility) return true;
            return false;
        }

        private bool PlayerCanPickArmorOrMovement(Skill newSkill)
        {
            if (PlayerConfig.SkillsOnDefault.Contains(newSkill.SkillType)) return true;
            if (PlayerConfig.SkillsOnDouble.Contains(newSkill.SkillType)) return true;
            if (newSkill.SkillType == SkillType.PlusOneArmorOrMovement) return true;
            return false;
        }

        private bool PlayerCanPickDoubleSkill(Skill newSkill)
        {
            if (PlayerConfig.SkillsOnDefault.Contains(newSkill.SkillType)) return true;
            if (PlayerConfig.SkillsOnDouble.Contains(newSkill.SkillType)) return true;
            return false;
        }

        public DomainResult Pass()
        {
            return DomainResult.Ok(new PlayerPassed(PlayerId, StarPlayerPoints + 1));
        }

        public DomainResult Block()
        {
            return DomainResult.Ok(new PlayerMadeCasualty(PlayerId, StarPlayerPoints + 2));
        }

        public DomainResult Move()
        {
            return DomainResult.Ok(new PlayerMadeTouchdown(PlayerId, StarPlayerPoints + 3));
        }

        public DomainResult NominateForMostValuablePlayer()
        {
            return DomainResult.Ok(new PlayerWasNominatedMostValuablePlayer(PlayerId, StarPlayerPoints + 5));
        }

        public void Apply(PlayerPassed domainEvent)
        {
            StarPlayerPoints = domainEvent.NewStarPlayerPoints;
        }

        public void Apply(PlayerMadeCasualty domainEvent)
        {
            StarPlayerPoints += domainEvent.NewStarPlayerPoints;
        }

        public void Apply(PlayerMadeTouchdown domainEvent)
        {
            StarPlayerPoints += domainEvent.NewStarPlayerPoints;
        }

        public void Apply(PlayerWasNominatedMostValuablePlayer domainEvent)
        {
            StarPlayerPoints += domainEvent.NewStarPlayerPoints;
        }

        public void Apply(SkillChosen domainEvent)
        {
            CurrentSkills = CurrentSkills.Append(domainEvent.NewSkill);
            FreeSkillPoints = domainEvent.RemainingLevelUps;
        }

        public void Apply(PlayerCreated playerCreated)
        {
            PlayerId = playerCreated.PlayerId;
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