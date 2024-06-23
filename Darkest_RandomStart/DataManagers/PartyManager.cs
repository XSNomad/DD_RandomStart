using Darkest_RandomStart.Darkest_RandomStart;
using Newtonsoft.Json;

namespace Darkest_RandomStart
{
    public class PartyData
    {
        [JsonProperty("party_names")]
        public List<Party> PartyNames { get; set; }
    }

    public class Party
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("required_hero_class")]
        public List<string> RequiredHeroClasses { get; set; }
    }

    public class PartyManager
    {
        public static void GetHerosFromParty(out List<string> firstTwoHeroes, out List<string> lastTwoHeroes)
        {
            firstTwoHeroes = new List<string>();
            lastTwoHeroes = new List<string>();

            try
            {
                // Read JSON file using FileFunctions
                string fileName = "party_name_library.json";
                string directory = Path.Combine("..", "..", "shared", "party_name");
                PartyData partyData = FileFunctions.ReadJsonFile<PartyData>(fileName, directory);

                if (partyData == null || partyData.PartyNames == null || partyData.PartyNames.Count == 0)
                {
                    throw new Exception("No party data found in the JSON file.");
                }

                // Randomly select a party
                Random random = new();
                Party selectedParty = partyData.PartyNames[random.Next(partyData.PartyNames.Count)];
                lastTwoHeroes = selectedParty.RequiredHeroClasses.Take(2).ToList();
                firstTwoHeroes = selectedParty.RequiredHeroClasses.Skip(2).Take(2).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetHerosFromParty: {ex.Message}");
                // Optionally handle the exception or return an error state
            }
        }
    }
}
