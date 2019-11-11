using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Players.Events.Players;

namespace Domain.Players.UnitTests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void LevelUp_NoSkillAvailable()
        {
            var player = new Player();
            var skillLevelUp = Block();

            var domainResult = player.ChooseSkill(skillLevelUp);

            Assert.IsFalse(domainResult.IsOk);
        }

        [TestMethod]
        public void PlayerLevelsUp()
        {
            var player = new Player();
            var nominateForMostValuablePlayer = player.NominateForMostValuablePlayer();
            player.Apply(nominateForMostValuablePlayer.DomainEvents);
            var result = player.Block();
            player.Apply(result.DomainEvents);

            Assert.AreEqual(2, ((PlayerLeveledUp) result.DomainEvents.Last()).NewLevel);
            Assert.AreEqual(2, player.Level);
        }

        [TestMethod]
        public void PlayerLeveledUpTwice_LevelUpOnce()
        {
            var player = PlayerWith15SSP();
            player.NominateForMostValuablePlayer();
            player.RegisterLevelUpSkillPointRoll(FreeSkillPoint.PlusOneStrength);
            player.RegisterLevelUpSkillPointRoll(FreeSkillPoint.PlusOneStrength);

            var domainResult = player.ChooseSkill(Block());

            Assert.AreEqual(1, ((SkillChosen) domainResult.DomainEvents.Last()).NewFreeSkillPoints.Count());
        }
        
        [TestMethod]
        public void PlayerLeveledUpOnceChooseSkillPoints()
        {
            var player = LeveledUpdPlayer();
            player.RegisterLevelUpSkillPointRoll(FreeSkillPoint.PlusOneStrength);

            player.ChooseSkill(Block());

            Assert.AreEqual(0, player.FreeSkillPoints.Count());
            Assert.AreEqual(2, player.Level);
            Assert.AreEqual(Block().SkillId, player.CurrentSkills.Single().SkillId);
        }

        private Player LeveledUpdPlayer()
        {
            var defaultPlayer = DefaultPlayer();
            defaultPlayer.NominateForMostValuablePlayer();
            defaultPlayer.NominateForMostValuablePlayer();
            return defaultPlayer;
        }

        private static Player PlayerWith15SSP()
        {
            var player = DefaultPlayer();

            player.NominateForMostValuablePlayer();
            player.NominateForMostValuablePlayer();
            player.NominateForMostValuablePlayer();
            player.RegisterLevelUpSkillPointRoll(FreeSkillPoint.PlusOneStrength);
            return player;
        }

        private static Player DefaultPlayer()
        {
            var player = new Player();
            var playerCreated = PlayerCreated();
            player.Apply(playerCreated);
            return player;
        }

        private static SkillReadModel Block()
        {
            return new SkillReadModel
            {
                SkillId = "Block",
                SkillType = SkillType.General
            };
        }

        private static SkillReadModel PlusOneStrength()
        {
            return new SkillReadModel
            {
                SkillId = "PlusOneStrength",
                SkillType = SkillType.PlusOneStrength
            };
        }

        private static SkillReadModel Pass()
        {
            return new SkillReadModel
            {
                SkillId = "Pass",
                SkillType = SkillType.Passing
            };
        }

        [TestMethod]
        public void LevelUp_StrengthSkillAvailable()
        {
            var player = DefaultPlayer();
            player.Apply(PlayerChoseLevelUp(FreeSkillPoint.PlusOneStrength));
            var skillLevelUp = PlusOneStrength();

            var domainResult = player.ChooseSkill(skillLevelUp);

            Assert.IsTrue(domainResult.IsOk);
            Assert.AreEqual(1, domainResult.DomainEvents.Count());
        }

        [TestMethod]
        public void LevelUp_StrengtSkillAvailable_SkillOfLowerPower()
        {
            var player = DefaultPlayer();
            player.Apply(PlayerChoseLevelUp(FreeSkillPoint.PlusOneStrength));
            var skillLevelUp = Pass();

            var domainResult = player.ChooseSkill(skillLevelUp);

            Assert.IsTrue(domainResult.IsOk);
            Assert.AreEqual(1, domainResult.DomainEvents.Count());
        }

        [TestMethod]
        public void LevelUp_StrengthWanted_ButOnlyArmorPossible()
        {
            var player = DefaultPlayer();
            player.Apply(PlayerChoseLevelUp(FreeSkillPoint.PlusOneArmorOrMovement));
            var skillLevelUp = PlusOneStrength();

            var domainResult = player.ChooseSkill(skillLevelUp);

            Assert.IsFalse(domainResult.IsOk);
        }

        [TestMethod]
        public void LevelUp_DoubleReplacedWithGeneral()
        {
            var player = new Player();
            player.Apply(PlayerCreated());
            player.Apply(PlayerChoseLevelUp(FreeSkillPoint.Double));
            var skillLevelUp = Block();

            var domainResult = player.ChooseSkill(skillLevelUp);

            Assert.IsTrue(domainResult.IsOk);
        }

        [TestMethod]
        public void LevelUp_DoubleUSedForDouble()
        {
            var player = new Player();
            player.Apply(PlayerCreated());
            player.Apply(PlayerChoseLevelUp(FreeSkillPoint.Double));
            var skillLevelUp = Pass();

            var domainResult = player.ChooseSkill(skillLevelUp);

            Assert.IsTrue(domainResult.IsOk);
        }

        [TestMethod]
        public void LevelUp_DoubleTooHigh()
        {
            var player = new Player();
            player.Apply(PlayerCreated());
            player.Apply(PlayerChoseLevelUp());
            var skillLevelUp = Pass();

            var domainResult = player.ChooseSkill(skillLevelUp);

            Assert.IsFalse(domainResult.IsOk);
        }

        [TestMethod]
        public void LevelUp_PickSkillTwice()
        {
            var player = new Player();
            player.Apply(PlayerCreated());
            player.Apply(PlayerChoseLevelUp());
            player.Apply(PlayerChoseLevelUp());
            player.Apply(SkillPicked(Block()));

            var skillLevelUp = Block();

            var domainResult2 = player.ChooseSkill(skillLevelUp);

            Assert.IsFalse(domainResult2.IsOk);
        }

        private static PlayerLevelUpPossibilitiesChosen PlayerChoseLevelUp(FreeSkillPoint freeSkillPoint = default(FreeSkillPoint))
        {
            return new PlayerLevelUpPossibilitiesChosen(Guid.NewGuid(), freeSkillPoint);
        }

        private static SkillChosen SkillPicked(SkillReadModel skill)
        {
            return new SkillChosen(Guid.NewGuid(), skill, new List<FreeSkillPoint>());
        }

        private static PlayerConfig PlayerConfig(
            IEnumerable<SkillType> defaultSkills = null,
            IEnumerable<SkillType> doubleSkills = null)
        {
            var playerConfig = new PlayerConfig(
                "whatever",
                new PlayerStats(8,3,3,8),
                new List<SkillReadModel>(),
                defaultSkills ?? new[] {SkillType.General},
                doubleSkills ?? new[] {SkillType.Passing});
            return playerConfig;
        }

        private static PlayerCreated PlayerCreated(
            IEnumerable<SkillType> defaultSkills = null,
            IEnumerable<SkillType> doubleSkills = null)
        {
            var playerConfig = PlayerConfig(defaultSkills, doubleSkills);
            return new PlayerCreated(Guid.NewGuid(), Guid.NewGuid(), playerConfig);
        }
    }
}