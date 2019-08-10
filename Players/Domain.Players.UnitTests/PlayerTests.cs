using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Players.Events.PlayerConfigs;
using Domain.Players.Events.Players;
using Microwave.Domain.Identities;

namespace Domain.Players.UnitTests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void LevelUp_NoSkillAvailable()
        {
            var player = new Player();
            var skillLevelUp = Skill.Block;

            var domainResult = player.ChooseSkill(skillLevelUp);

            Assert.IsFalse(domainResult.IsOk);
        }

        [TestMethod]
        public void LevelUp_StrengthSkillAvailable()
        {
            var player = new Player();
            player.Apply(PlayerLeveledUp(new [] { FreeSkillPoint.PlusOneStrength }));
            var skillLevelUp = Skill.PlusOneStrength;

            var domainResult = player.ChooseSkill(skillLevelUp);

            Assert.IsTrue(domainResult.IsOk);
            Assert.AreEqual(1, domainResult.DomainEvents.Count());
        }

        [TestMethod]
        public void LevelUp_StrengtSkillAvailable_SkillOfLowerPower()
        {
            var player = new Player();
            player.Apply(PlayerLeveledUp(new []{ FreeSkillPoint.PlusOneStrength }));
            var skillLevelUp = Skill.Pass;

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
            var skillLevelUp = Skill.PlusOneStrength;

            var domainResult = player.ChooseSkill(skillLevelUp);

            Assert.IsFalse(domainResult.IsOk);
        }

        [TestMethod]
        public void LevelUp_DoubleReplacedWithGeneral()
        {
            var player = new Player();
            player.Apply(PlayerCreated());
            player.Apply(PlayerLeveledUp(new []{ FreeSkillPoint.Double }));
            var skillLevelUp = Skill.Block;

            var domainResult = player.ChooseSkill(skillLevelUp);

            Assert.IsTrue(domainResult.IsOk);
        }

        [TestMethod]
        public void LevelUp_DoubleUSedForDouble()
        {
            var player = new Player();
            player.Apply(PlayerCreated());
            player.Apply(PlayerLeveledUp(new []{ FreeSkillPoint.Double }));
            var skillLevelUp = Skill.Pass;

            var domainResult = player.ChooseSkill(skillLevelUp);

            Assert.IsTrue(domainResult.IsOk);
        }

        [TestMethod]
        public void LevelUp_DoubleTooHigh()
        {
            var player = new Player();
            player.Apply(PlayerCreated());
            player.Apply(PlayerLeveledUp(new []{ FreeSkillPoint.Normal }));
            var skillLevelUp = Skill.Pass;

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
            player.Apply(SkillPicked(Skill.Block));

            var skillLevelUp = Skill.Block;

            var domainResult2 = player.ChooseSkill(skillLevelUp);

            Assert.IsFalse(domainResult2.IsOk);
        }

        private static PlayerLeveledUp PlayerLeveledUp(IEnumerable<FreeSkillPoint> freeSkillPoints = null)
        {
            return new PlayerLeveledUp(GuidIdentity.Create(), freeSkillPoints ?? new []{ FreeSkillPoint.PlusOneStrength }, 2);
        }

        private static SkillChosen SkillPicked(Skill skill)
        {
            return new SkillChosen(GuidIdentity.Create(), skill, new List<FreeSkillPoint>());
        }

        private static PlayerConfig PlayerConfig(
            IEnumerable<SkillType> defaultSkills = null,
            IEnumerable<SkillType> doubleSkills = null)
        {
            var playerConfig = new PlayerConfig();
            var playerTypeId = StringIdentity.Create("whatever");
            playerConfig.Apply(new PlayerConfigCreated(
                playerTypeId,
                new List<Skill>(),
                defaultSkills ?? new[] {SkillType.General},
                doubleSkills ?? new[] {SkillType.Passing}));
            return playerConfig;
        }

        private static PlayerCreated PlayerCreated(
            IEnumerable<SkillType> defaultSkills = null,
            IEnumerable<SkillType> doubleSkills = null)
        {
            var playerConfig = PlayerConfig(defaultSkills, doubleSkills);
            return new PlayerCreated(GuidIdentity.Create(), GuidIdentity.Create(), StringIdentity.Create("whatever"), playerConfig);
        }
    }
}