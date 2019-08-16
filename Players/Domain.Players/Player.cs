using System.Collections.Generic;
using System.Linq;
using Domain.Players.DomainErrors;
using Domain.Players.Events.Players;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;
using Microwave.Domain.Validation;

namespace Domain.Players
{
    public class Player : Entity,
        IApply<PlayerCreated>,
        IApply<PlayerLeveledUp>,
        IApply<SkillChosen>,
        IApply<PlayerPassed>,
        IApply<PlayerWasNominatedMostValuablePlayer>,
        IApply<PlayerMadeTouchdown>,
        IApply<PlayerMadeCasualty>
    {
        public GuidIdentity PlayerId { get; private set; }
        public PlayerConfig PlayerConfig { get; private set; }
        public IEnumerable<FreeSkillPoint> FreeSkillPoints { get; private set; } = new List<FreeSkillPoint>();
        public IEnumerable<SkillReadModel> CurrentSkills { get; private set; } = new List<SkillReadModel>();
        public long StarPlayerPoints { get; private set; }

        public int Level { get; private set; } = 1;

        private readonly IEnumerable<int> _levelUpPoints = new[] { 6, 16, 31, 51, 76, 176 };

        public static DomainResult Create(
            GuidIdentity playerId,
            GuidIdentity teamId,
            AllowedPlayer allowedPlayer,
            string name)
        {
            var playerConfig = new PlayerConfig(
                allowedPlayer.PlayerTypeId,
                allowedPlayer.StartingSkills,
                allowedPlayer.SkillsOnDefault,
                allowedPlayer.SkillsOnDouble);
            var playerCreated = new PlayerCreated(playerId, teamId, playerConfig, name);
            return DomainResult.Ok(playerCreated);
        }

        public DomainResult ChooseSkill(SkillReadModel newSkill)
        {
            if (!FreeSkillPoints.Any()) return DomainResult.Error(new NoLevelUpsAvailable());
            if (CurrentSkills.Any(s => s.Equals(newSkill))) return DomainResult.Error(
                new CanNotPickSkillTwice(CurrentSkills.Select(s => s.SkillId)));

            foreach (var freeSkillPoint in FreeSkillPoints)
            {
                if (!HasPlayerFreeSkillForChosenSkill(newSkill, freeSkillPoint))
                {
                    return DomainResult.Error(new SkillNotPickable(freeSkillPoint));
                }
            }

            var newFreeSkills = GetMinimalSkillToRemove(FreeSkillPoints, newSkill);

            return DomainResult.Ok(new SkillChosen(PlayerId, newSkill, newFreeSkills));
        }

        private IEnumerable<FreeSkillPoint> GetMinimalSkillToRemove(
            IEnumerable<FreeSkillPoint> freeSkillPoints,
            SkillReadModel newSkill)
        {
            var skillPoints = freeSkillPoints.ToList();
            if (PlayerConfig.SkillsOnDefault.Contains(newSkill.SkillType))
            {
                return RemoveBiggerThan(skillPoints, FreeSkillPoint.Normal);
            }

            if (PlayerConfig.SkillsOnDouble.Contains(newSkill.SkillType))
            {
                return RemoveBiggerThan(skillPoints, FreeSkillPoint.Double);
            }

            switch (newSkill.SkillType)
            {
                case SkillType.PlusOneArmorOrMovement:
                    return RemoveBiggerThan(skillPoints, FreeSkillPoint.PlusOneArmorOrMovement);
                case SkillType.PlusOneAgility:
                    return RemoveBiggerThan(skillPoints, FreeSkillPoint.PlusOneAgility);
                default:
                    return RemoveBiggerThan(skillPoints, FreeSkillPoint.PlusOneStrength);
            }
        }

        private IEnumerable<FreeSkillPoint> RemoveBiggerThan(IEnumerable<FreeSkillPoint> skillPoints, FreeSkillPoint normal)
        {
            var freeSkillPoints = skillPoints.ToList();
            var smallest = freeSkillPoints.Where(s => s >= normal).Min();
            freeSkillPoints.Remove(smallest);
            return freeSkillPoints;
        }

        private bool HasPlayerFreeSkillForChosenSkill(
            SkillReadModel newSkill,
            FreeSkillPoint freeSkillType)
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

        private bool PlayerCanPickNormalSkill(SkillReadModel newSkill)
        {
            return PlayerConfig.SkillsOnDefault.Contains(newSkill.SkillType);
        }

        private bool PlayerCanPickAgility(SkillReadModel newSkill)
        {
            if (PlayerConfig.SkillsOnDefault.Contains(newSkill.SkillType)) return true;
            if (PlayerConfig.SkillsOnDouble.Contains(newSkill.SkillType)) return true;
            if (newSkill.SkillType == SkillType.PlusOneArmorOrMovement) return true;
            if (newSkill.SkillType == SkillType.PlusOneAgility) return true;
            return false;
        }

        private bool PlayerCanPickArmorOrMovement(SkillReadModel newSkill)
        {
            if (PlayerConfig.SkillsOnDefault.Contains(newSkill.SkillType)) return true;
            if (PlayerConfig.SkillsOnDouble.Contains(newSkill.SkillType)) return true;
            if (newSkill.SkillType == SkillType.PlusOneArmorOrMovement) return true;
            return false;
        }

        private bool PlayerCanPickDoubleSkill(SkillReadModel newSkill)
        {
            if (PlayerConfig.SkillsOnDefault.Contains(newSkill.SkillType)) return true;
            if (PlayerConfig.SkillsOnDouble.Contains(newSkill.SkillType)) return true;
            return false;
        }

        public DomainResult Pass()
        {
            var newStarPlayerPoints = StarPlayerPoints + 1;
            var domainEvents = CreateLevelUpEvents(new PlayerPassed(PlayerId, newStarPlayerPoints), newStarPlayerPoints);
            return DomainResult.Ok(domainEvents);
        }

        public DomainResult Block()
        {
            var newStarPlayerPoints = StarPlayerPoints + 2;
            var domainEvents = CreateLevelUpEvents(new PlayerMadeCasualty(PlayerId, newStarPlayerPoints), newStarPlayerPoints);
            return DomainResult.Ok(domainEvents);
        }

        public DomainResult Move()
        {
            var newStarPlayerPoints = StarPlayerPoints + 3;
            var domainEvents = CreateLevelUpEvents(new PlayerMadeTouchdown(PlayerId, newStarPlayerPoints), newStarPlayerPoints);
            return DomainResult.Ok(domainEvents);
        }

        public DomainResult NominateForMostValuablePlayer()
        {
            var newStarPlayerPoints = StarPlayerPoints + 5;
            var domainEvents = CreateLevelUpEvents(new PlayerWasNominatedMostValuablePlayer(PlayerId, newStarPlayerPoints), newStarPlayerPoints);
            return DomainResult.Ok(domainEvents);
        }

        private IEnumerable<IDomainEvent> CreateLevelUpEvents(IDomainEvent defaultEvent, long newPoints)
        {
            var domainEvents = new List<IDomainEvent>();
            domainEvents.Add(defaultEvent);

            if (NextLevelIsDue(newPoints))
            {
                var freeSkillPointFactory = new FreeSkillPointFactory();
                var freeSkillPoint = freeSkillPointFactory.Create();
                var freeSkillPoints = new List<FreeSkillPoint>();
                freeSkillPoints.AddRange(FreeSkillPoints);
                freeSkillPoints.Add(freeSkillPoint);
                domainEvents.Add(new PlayerLeveledUp(PlayerId, freeSkillPoints, Level + 1));
            }

            return domainEvents;
        }
        private bool NextLevelIsDue(long starPlayerPoints)
        {
            var level = _levelUpPoints.Count(upPoint => starPlayerPoints >= upPoint);
            return level + 1 > Level;
        }

        public void Apply(PlayerPassed domainEvent)
        {
            StarPlayerPoints = domainEvent.NewStarPlayerPoints;
        }

        public void Apply(PlayerMadeCasualty domainEvent)
        {
            StarPlayerPoints = domainEvent.NewStarPlayerPoints;
        }

        public void Apply(PlayerMadeTouchdown domainEvent)
        {
            StarPlayerPoints = domainEvent.NewStarPlayerPoints;
        }

        public void Apply(PlayerWasNominatedMostValuablePlayer domainEvent)
        {
            StarPlayerPoints = domainEvent.NewStarPlayerPoints;
        }

        public void Apply(SkillChosen domainEvent)
        {
            CurrentSkills = CurrentSkills.Append(domainEvent.NewSkill);
            FreeSkillPoints = domainEvent.NewFreeSkillPoints;
        }

        public void Apply(PlayerCreated playerCreated)
        {
            PlayerId = playerCreated.PlayerId;
            CurrentSkills = playerCreated.PlayerConfig.StartingSkills;
            PlayerConfig = playerCreated.PlayerConfig;
        }

        public void Apply(PlayerLeveledUp leveledUp)
        {
            FreeSkillPoints = leveledUp.NewFreeSkillPoints;
            Level = leveledUp.NewLevel;
        }
    }
}