using System.Collections.Generic;
using Teams.ReadHost.Players;
using Teams.ReadHost.Races;

namespace Teams.ReadHost.Teams
{
    public class AllowedPlayer
    {
        public string PlayerTypeId { get; set; }
        public int MaximumPlayers { get; set; }
        public GoldCoins Cost { get; set; }
        public PlayerStats PlayerStats { get; set; }
        public IEnumerable<Skill> StartingSkills { get; set; }
        public IEnumerable<SkillType> SkillsOnDefault { get; set; }
        public IEnumerable<SkillType> SkillsOnDouble { get; set; }
    }

    public class Skill
    {
        public string SkillId { get; set; }
        public SkillType SkillType { get; set; }
    }
}