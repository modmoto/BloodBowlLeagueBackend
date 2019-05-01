using System.Collections.Generic;
using System.Linq;
using Domain.Players.DomainErrors;
using Domain.Players.Events.Players;
using Microwave.Domain;

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
        public IEnumerable<StringIdentity> CurrentSkills { get; private set; } = new List<StringIdentity>();
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
                    return DomainResult.Ok(new SkillChosen(PlayerId, newSkill.SkillId, skillTypes));
                }
            }

            return DomainResult.Error(new SkillNotPickable(FreeSkillPoints));
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