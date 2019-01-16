using System.Collections.Generic;
using AutoFixture;
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
            var skill = new Skill();
            skill.Apply(new SkillCreated(StringIdentity.Create("Block"), SkillType.General));

            var skillLevelUp = new Skill();
            skillLevelUp.Apply(new SkillCreated(StringIdentity.Create("egal"), SkillType.Strength));
            var domainResult = player.LevelUp(skillLevelUp);

            Assert.IsFalse(domainResult.IsOk);
        }
        
//        [Test]
//        public void LevelUp_NoSkillAvailable()
//        {
//            var fixture = new Fixture();
//            var playerConfig = fixture.Build<PlayerConfig>()
//                .With(c => c.SkillsOnDefault, new[] {SkillType.General})
//                .With(c => c.SkillsOnDouble, new[] {SkillType.Passing})
//                .Create();
//
//            var player = fixture.Build<Player>()
//                .With(p => p.PlayerConfig, playerConfig)
//                .Create();
//
//            var skillLevelUp = new Skill();
//            skillLevelUp.Apply(new SkillCreated(StringIdentity.Create("egal"), SkillType.Strength));
//            var domainResult = player.LevelUp(skillLevelUp);
//
//            Assert.IsFalse(domainResult.IsOk);
//        }
    }
}