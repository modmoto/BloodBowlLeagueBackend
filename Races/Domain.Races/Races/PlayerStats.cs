namespace Domain.Races.Races
{
    public class PlayerStats
    {
        public int Movement { get; }
        public int Strength { get; }
        public int Agility { get; }
        public int Armor { get; }

        public PlayerStats(int movement, int strength, int agility, int armor)
        {
            Movement = movement;
            Strength = strength;
            Agility = agility;
            Armor = armor;
        }
    }
}