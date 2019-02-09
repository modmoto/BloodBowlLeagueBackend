using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Players.Events.PlayerConfigs;
using Domain.Players.Events.Players;
using Domain.Players.Events.Skills;
using Microwave.Domain;

namespace Domain.Players.UnitTests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void LevelUp_NoSkillAvailable()
        {
            var player = new Player();
            var skillLevelUp = new Skill();
            skillLevelUp.Apply(SkillCreated(SkillType.Agility));

            var domainResult = player.ChooseSkill(skillLevelUp);

            Assert.IsFalse(domainResult.IsOk);
        }

        [TestMethod]
        public void LevelUp_StrengtSkillAvailable()
        {
            var player = new Player();
            player.Apply(PlayerLeveledUp(new [] { FreeSkillPoint.PlusOneStrength }));
            var skillLevelUp = new Skill();
            skillLevelUp.Apply(SkillCreated(SkillType.PlusOneStrength));

            var domainResult = player.ChooseSkill(skillLevelUp);

            Assert.IsTrue(domainResult.IsOk);
            Assert.AreEqual(1, domainResult.DomainEvents.Count());
        }

        [TestMethod]
        public void LevelUp_StrengtSkillAvailable_SkillOfLowerPower()
        {
            var player = new Player();
            player.Apply(PlayerLeveledUp(new []{ FreeSkillPoint.PlusOneStrength }));
            var skillLevelUp = new Skill();
            skillLevelUp.Apply(SkillCreated(SkillType.Passing));

            var domainResult = player.ChooseSkill(skillLevelUp);

            Assert.IsTrue(domainResult.IsOk);
            Assert.AreEqual(1, domainResult.DomainEvents.Count());
        }

        [TestMethod]
        public void LevelUp_StrengthWanted_ButOnlyArmorPossible()
        {
            var player = new Player();
            player.Apply(PlayerCreated());
            player.Apply(PlayerLeveledUp(new []{ FreeSkillPoint.PlusOneArmorOrMovement }));
            var skillLevelUp = new Skill();
            skillLevelUp.Apply(SkillCreated(SkillType.PlusOneStrength));

            var domainResult = player.ChooseSkill(skillLevelUp);

            Assert.IsFalse(domainResult.IsOk);
        }

        [TestMethod]
        public void LevelUp_DoubleReplacedWithGeneral()
        {
            var player = new Player();
            player.Apply(PlayerCreated());
            player.Apply(PlayerLeveledUp(new []{ FreeSkillPoint.Double }));
            var skillLevelUp = new Skill();
            skillLevelUp.Apply(SkillCreated(SkillType.General));

            var domainResult = player.ChooseSkill(skillLevelUp);

            Assert.IsTrue(domainResult.IsOk);
        }

        [TestMethod]
        public void LevelUp_DoubleUSedForDouble()
        {
            var player = new Player();
            player.Apply(PlayerCreated());
            player.Apply(PlayerLeveledUp(new []{ FreeSkillPoint.Double }));
            var skillLevelUp = new Skill();
            skillLevelUp.Apply(SkillCreated(SkillType.Passing));

            var domainResult = player.ChooseSkill(skillLevelUp);

            Assert.IsTrue(domainResult.IsOk);
        }

        [TestMethod]
        public void LevelUp_DoubleTooHigh()
        {
            var player = new Player();
            player.Apply(PlayerCreated());
            player.Apply(PlayerLeveledUp(new []{ FreeSkillPoint.Normal }));
            var skillLevelUp = new Skill();
            skillLevelUp.Apply(SkillCreated(SkillType.Passing));

            var domainResult = player.ChooseSkill(skillLevelUp);

            Assert.IsFalse(domainResult.IsOk);
        }

        [TestMethod]
        public void LevelUp_PickSkillTwice()
        {
            var player = new Player();
            player.Apply(PlayerCreated());
            player.Apply(PlayerLeveledUp(new []{ FreeSkillPoint.Normal }));
            player.Apply(PlayerLeveledUp(new []{ FreeSkillPoint.Normal }));
            player.Apply(SkillPicked(StringIdentity.Create("SameSkill")));

            var skillLevelUp = new Skill();
            skillLevelUp.Apply(SkillCreated(SkillType.General, StringIdentity.Create("SameSkill")));

            var domainResult2 = player.ChooseSkill(skillLevelUp);

            Assert.IsFalse(domainResult2.IsOk);
        }


        private static SkillCreated SkillCreated(SkillType skillType, StringIdentity skillId = null)
        {
            return new SkillCreated(skillId ?? StringIdentity.Create("egal"), skillType);
        }

        private static PlayerLeveledUp PlayerLeveledUp(IEnumerable<FreeSkillPoint> freeSkillPoints = null)
        {
            return new PlayerLeveledUp(GuidIdentity.Create(), freeSkillPoints ?? new []{ FreeSkillPoint.PlusOneStrength }, 2);
        }

        private static SkillChosen SkillPicked(StringIdentity skillId = null)
        {
            return new SkillChosen(GuidIdentity.Create(), skillId ?? StringIdentity.Create("Whatever"),
                new List<FreeSkillPoint>());
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