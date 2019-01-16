using System.Collections.Generic;
using System.Linq;
using Domain.Players.Events;
using Domain.Players.Events.PlayerConfigs;
using Domain.Players.Events.Players;
using Domain.Players.Events.Skills;
using Microwave.Domain;
using NUnit.Framework;

namespace Domain.Players.UnitTests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void LevelUp_NoSkillAvailable()
        {
            var player = new Player();
            var skillLevelUp = new Skill();
            skillLevelUp.Apply(SkillCreated(SkillType.Agility));

            var domainResult = player.LevelUp(skillLevelUp);

            Assert.IsFalse(domainResult.IsOk);
        }

        [Test]
        public void LevelUp_StrengtSkillAvailable()
        {
            var player = new Player();
            player.Apply(PlayerLeveledUp(new [] { FreeSkillPoint.PlusOneStrength }));
            var skillLevelUp = new Skill();
            skillLevelUp.Apply(SkillCreated(SkillType.PlusOneStrength));

            var domainResult = player.LevelUp(skillLevelUp);

            Assert.IsTrue(domainResult.IsOk);
            Assert.AreEqual(1, domainResult.DomainEvents.Count());
        }

        [Test]
        public void LevelUp_StrengtSkillAvailable_SkillOfLowerPower()
        {
            var player = new Player();
            player.Apply(PlayerLeveledUp(new []{ FreeSkillPoint.PlusOneStrength }));
            var skillLevelUp = new Skill();
            skillLevelUp.Apply(SkillCreated(SkillType.Passing));

            var domainResult = player.LevelUp(skillLevelUp);

            Assert.IsTrue(domainResult.IsOk);
            Assert.AreEqual(1, domainResult.DomainEvents.Count());
        }

        [Test]
        public void LevelUp_StrengthWanted_ButOnlyArmorPossible()
        {
            var player = new Player();
            player.Apply(PlayerCreated());
            player.Apply(PlayerLeveledUp(new []{ FreeSkillPoint.PlusOneArmorOrMovement }));
            var skillLevelUp = new Skill();
            skillLevelUp.Apply(SkillCreated(SkillType.PlusOneStrength));

            var domainResult = player.LevelUp(skillLevelUp);

            Assert.IsFalse(domainResult.IsOk);
        }

        [Test]
        public void LevelUp_DoubleReplacedWithGeneral()
        {
            var player = new Player();
            player.Apply(PlayerCreated());
            player.Apply(PlayerLeveledUp(new []{ FreeSkillPoint.Double }));
            var skillLevelUp = new Skill();
            skillLevelUp.Apply(SkillCreated(SkillType.General));

            var domainResult = player.LevelUp(skillLevelUp);

            Assert.IsTrue(domainResult.IsOk);
        }

        [Test]
        public void LevelUp_DoubleUSedForDouble()
        {
            var player = new Player();
            player.Apply(PlayerCreated());
            player.Apply(PlayerLeveledUp(new []{ FreeSkillPoint.Double }));
            var skillLevelUp = new Skill();
            skillLevelUp.Apply(SkillCreated(SkillType.Passing));

            var domainResult = player.LevelUp(skillLevelUp);

            Assert.IsTrue(domainResult.IsOk);
        }

        [Test]
        public void LevelUp_DoubleTooHigh()
        {
            var player = new Player();
            player.Apply(PlayerCreated());
            player.Apply(PlayerLeveledUp(new []{ FreeSkillPoint.Normal }));
            var skillLevelUp = new Skill();
            skillLevelUp.Apply(SkillCreated(SkillType.Passing));

            var domainResult = player.LevelUp(skillLevelUp);

            Assert.IsFalse(domainResult.IsOk);
        }


        private static SkillCreated SkillCreated(SkillType skillType)
        {
            return new SkillCreated(StringIdentity.Create("egal"), skillType);
        }

        private static PlayerLeveledUp PlayerLeveledUp(IEnumerable<FreeSkillPoint> freeSkillPoints = null)
        {
            return new PlayerLeveledUp(GuidIdentity.Create(), freeSkillPoints ?? new []{ FreeSkillPoint.PlusOneStrength });
        }

        private static PlayerConfig PlayerConfig(
            IEnumerable<SkillType> defaultSkills = null,
            IEnumerable<SkillType> doubleSkills = null)
        {
            var playerConfig = new PlayerConfig();
            var playerTypeId = StringIdentity.Create("whatever");
            playerConfig.Apply(new PlayerConfigCreated(
                playerTypeId,
                new List<StringIdentity>(),
                defaultSkills ?? new[] {SkillType.General},
                doubleSkills ?? new[] {SkillType.Passing}));
            return playerConfig;
        }

        private static PlayerCreated PlayerCreated(
            IEnumerable<SkillType> defaultSkills = null,
            IEnumerable<SkillType> doubleSkills = null)
        {
            var playerConfig = PlayerConfig(defaultSkills, doubleSkills);
            return new PlayerCreated(GuidIdentity.Create(), StringIdentity.Create("whatever"), playerConfig);
        }
    }
}