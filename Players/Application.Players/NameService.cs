using Domain.Players;
using RandomNameGeneratorLibrary;

namespace Application.Players
{
    public class NameService
    {
        public string CreateNameFor(RaceReadModel race)
        {
            var personGenerator = new PersonNameGenerator();
            var name = personGenerator.GenerateRandomFirstAndLastName();
            return name;
        }
    }
}