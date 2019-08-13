using Domain.Races.Skills;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microwave.Domain.Identities;

namespace Domain.Races.UnitTests
{
    [TestClass]
    public class SkillTests
    {
        [TestMethod]
        public void SkillFromString()
        {
            var skill = Skill.Create(StringIdentity.Create("Dodge"));
            Assert.AreEqual(Skill.Create(Skill.Dodge), skill);
        }

        [TestMethod]
        public void UnknownSkill()
        {
            var skill = Skill.Create(StringIdentity.Create("WirdsNieGeben"));
            Assert.AreEqual(Skill.Create(Skill.NullSkill), skill);
        }
    }
}