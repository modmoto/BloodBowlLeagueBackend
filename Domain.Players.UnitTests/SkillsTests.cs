using Microwave.Domain;
using NUnit.Framework;

namespace Domain.Players.UnitTests
{
    [TestFixture]
    public class SkillsTests
    {
        [Test]
        public void BiggerOrEqual()
        {
            var skill = new Skill();
            skill.Apply(new SkillCreated(StringIdentity.Create("Block"), SkillType.General));

            Assert.IsFalse(skill.IsBiggerOrEqual(SkillType.PlusOneStrength));
        }
    }
}