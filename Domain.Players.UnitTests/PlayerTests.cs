using System.Collections.Generic;
using System.Linq;
using Domain.Players.Events;
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
            var playerConfig = new PlayerConfig();
            var playerTypeId = StringIdentity.Create("whatever");
            playerConfig.Apply(new PlayerConfigCreated(
                playerTypeId,
                new List<StringIdentity>(),
                new []{ SkillType.General },
                new []{ SkillType.Passing }));

            player.Apply(new PlayerCreated(GuidIdentity.Create(), playerTypeId, playerConfig));

            var skillLevelUp = new Skill();
            skillLevelUp.Apply(new SkillCreated(StringIdentity.Create("egal"), SkillType.Strength));
            var domainResult = player.LevelUp(skillLevelUp);

            Assert.IsFalse(domainResult.IsOk);
        }

        [Test]
        public void LevelUp_StrengtSkillAvailable()
        {
            var player = new Player();
            var playerConfig = new PlayerConfig();
            var playerTypeId = StringIdentity.Create("whatever");
            playerConfig.Apply(new PlayerConfigCreated(
                playerTypeId,
                new List<StringIdentity>(),
                new []{ SkillType.General },
                new []{ SkillType.Passing }));

            player.Apply(new PlayerCreated(GuidIdentity.Create(), playerTypeId, playerConfig));
            player.Apply(new PlayerLeveledUp(GuidIdentity.Create(), new []{ LevelUpType.PlusOneStrength }));

            var skillLevelUp = new Skill();
            skillLevelUp.Apply(new SkillCreated(StringIdentity.Create("egal"), SkillType.PlusOneStrength));
            var domainResult = player.LevelUp(skillLevelUp);

            Assert.IsTrue(domainResult.IsOk);
            Assert.AreEqual(1, domainResult.DomainEvents.Count());
        }

        [Test]
        public void LevelUp_StrengtSkillAvailable_SkillOfLowerPower()
        {
            var player = new Player();
            var playerConfig = new PlayerConfig();
            var playerTypeId = StringIdentity.Create("whatever");
            playerConfig.Apply(new PlayerConfigCreated(
                playerTypeId,
                new List<StringIdentity>(),
                new []{ SkillType.General },
                new []{ SkillType.Passing }));

            player.Apply(new PlayerCreated(GuidIdentity.Create(), playerTypeId, playerConfig));
            player.Apply(new PlayerLeveledUp(GuidIdentity.Create(), new []{ LevelUpType.PlusOneStrength }));

            var skillLevelUp = new Skill();
            skillLevelUp.Apply(new SkillCreated(StringIdentity.Create("egal"), SkillType.PlusOneArmorOrMovement));
            var domainResult = player.LevelUp(skillLevelUp);

            Assert.IsTrue(domainResult.IsOk);
            Assert.AreEqual(1, domainResult.DomainEvents.Count());
        }

        [Test]
        public void LevelUp_StrengthWanted_ButOnlyArmorPossible()
        {
            var player = new Player();
            var playerConfig = new PlayerConfig();
            var playerTypeId = StringIdentity.Create("whatever");
            playerConfig.Apply(new PlayerConfigCreated(
                playerTypeId,
                new List<StringIdentity>(),
                new []{ SkillType.General },
                new []{ SkillType.Passing }));

            player.Apply(new PlayerCreated(GuidIdentity.Create(), playerTypeId, playerConfig));
            player.Apply(new PlayerLeveledUp(GuidIdentity.Create(), new []{ LevelUpType.PlusOneArmorOrMovement }));

            var skillLevelUp = new Skill();
            skillLevelUp.Apply(new SkillCreated(StringIdentity.Create("egal"), SkillType.PlusOneStrength));
            var domainResult = player.LevelUp(skillLevelUp);

            Assert.IsFalse(domainResult.IsOk);
        }

        [Test]
        public void LevelUp_DoubleReplacedWithGeneral()
        {
            var player = new Player();
            var playerConfig = new PlayerConfig();
            var playerTypeId = StringIdentity.Create("whatever");
            playerConfig.Apply(new PlayerConfigCreated(
                playerTypeId,
                new List<StringIdentity>(),
                new []{ SkillType.General },
                new []{ SkillType.Passing }));

            player.Apply(new PlayerCreated(GuidIdentity.Create(), playerTypeId, playerConfig));
            player.Apply(new PlayerLeveledUp(GuidIdentity.Create(), new []{ LevelUpType.Double }));

            var skillLevelUp = new Skill();
            skillLevelUp.Apply(new SkillCreated(StringIdentity.Create("egal"), SkillType.General));
            var domainResult = player.LevelUp(skillLevelUp);

            Assert.IsTrue(domainResult.IsOk);
        }

        [Test]
        public void LevelUp_DoubleUSedForDouble()
        {
            var player = new Player();
            var playerConfig = new PlayerConfig();
            var playerTypeId = StringIdentity.Create("whatever");
            playerConfig.Apply(new PlayerConfigCreated(
                playerTypeId,
                new List<StringIdentity>(),
                new []{ SkillType.General },
                new []{ SkillType.Passing }));

            player.Apply(new PlayerCreated(GuidIdentity.Create(), playerTypeId, playerConfig));
            player.Apply(new PlayerLeveledUp(GuidIdentity.Create(), new []{ LevelUpType.Double }));

            var skillLevelUp = new Skill();
            skillLevelUp.Apply(new SkillCreated(StringIdentity.Create("egal"), SkillType.Passing));
            var domainResult = player.LevelUp(skillLevelUp);

            Assert.IsTrue(domainResult.IsOk);
        }

        [Test]
        public void LevelUp_DoubleTooHigh()
        {
            var player = new Player();
            var playerConfig = new PlayerConfig();
            var playerTypeId = StringIdentity.Create("whatever");
            playerConfig.Apply(new PlayerConfigCreated(
                playerTypeId,
                new List<StringIdentity>(),
                new []{ SkillType.General },
                new []{ SkillType.Passing }));

            player.Apply(new PlayerCreated(GuidIdentity.Create(), playerTypeId, playerConfig));
            player.Apply(new PlayerLeveledUp(GuidIdentity.Create(), new []{ LevelUpType.Normal }));

            var skillLevelUp = new Skill();
            skillLevelUp.Apply(new SkillCreated(StringIdentity.Create("egal"), SkillType.Passing));
            var domainResult = player.LevelUp(skillLevelUp);

            Assert.IsFalse(domainResult.IsOk);
        }
    }
}