using Darkest_RandomStart.Darkest_RandomStart;
using Darkest_RandomStart.SC;
using Newtonsoft.Json;

namespace Darkest_RandomStart
{
    class Program
    {
        public static bool TankSelected = false;
        public static int heroCounter = 0;
        
        static void Main(string[] args)
        {
            // Generate sample hero data and save to JSON
            GenerateSampleHeroes();

            // Perform StageCoach logic
            StageCoach();
        }

        static void GenerateSampleHeroes()
        {
            // Create a new RootObject instance and populate it with sample data
            RootObject root = new();
            // Adding sample hero data (similar structure to the provided JSON)
            root.data.heroes = new Dictionary<string, Hero>
            {
                ["1"] = new Hero(),
                ["2"] = new Hero()
            };

            // Serialize the root object to JSON
            string json = JsonConvert.SerializeObject(root, Formatting.Indented);

            // Save JSON to a file using FileFunctions
            string filePath = @"persist.roster.json";
            FileFunctions.SaveJsonToFile(json, filePath, "scripts/starting_save");
            Console.WriteLine("JSON file has been generated and saved to: " + filePath);
        }

        public static int GetNextHeroRank()
        {
            return ++heroCounter;
        }

        static void StageCoach()
        {
            try
            {
                // Read JSON file using FileFunctions
                string fileName = "stage_coach.building.json";
                string readDirectory = Path.Combine("..", "..", "campaign", "town", "buildings", "stage_coach");
                string writeDirectory = Path.Combine("campaign", "town", "buildings", "stage_coach");
                SC.RootObject rootObject = FileFunctions.ReadJsonFile<SC.RootObject>(fileName, readDirectory);

                // Randomize first_hero_classes
                List<string> combinedClasses = RandomizeHeroClasses();

                // Update the first_hero_classes in the first store's data
                if (rootObject.Data.Stores.Count > 0)
                {
                    rootObject.Data.Stores[0].Data.FirstHeroClasses = combinedClasses;
                }
                else
                {
                    throw new Exception("No stores found in the JSON data.");
                }

                // Serialize RootObject back to JSON
                string modifiedJson = JsonConvert.SerializeObject(rootObject, Formatting.Indented);

                // Save modified JSON to a file using FileFunctions
                FileFunctions.SaveJsonToFile(modifiedJson, fileName, writeDirectory);

                Console.WriteLine("JSON file has been updated and saved to the new location.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in StageCoach: {ex.Message}");
            }
        }

        static List<string> RandomizeHeroClasses()
        {
            List<string> heroClasses = Hero.nonTankClasses.Where(c => c != null).ToList();
            List<string> healerClasses = Hero.healerClasses.Where(c => c != null).ToList();

            Random random = new();
            List<string> combinedClasses = new();

            // Select one hero class and one healer class randomly
            if (heroClasses.Count > 0)
            {
                combinedClasses.Add(heroClasses[random.Next(heroClasses.Count)]);
            }

            if (healerClasses.Count > 0)
            {
                combinedClasses.Add(healerClasses[random.Next(healerClasses.Count)]);
            }

            return combinedClasses;
        }
    }
}
