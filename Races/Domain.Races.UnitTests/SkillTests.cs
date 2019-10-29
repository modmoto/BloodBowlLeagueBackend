using Domain.Races.Skills;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Races.UnitTests
{
    [TestClass]
    public class SkillTests
    {
        [TestMethod]
        public void SkillFromString()
        {
            var skill = Skill.Create("Dodge");
            Assert.AreEqual(Skill.Dodge, skill);
        }

        [TestMethod]
        public void UnknownSkill()
        {
            var skill = Skill.Create("WirdsNieGeben");
            Assert.AreEqual(Skill.NullSkill, skill);
        }
    }
}