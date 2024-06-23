using Darkest_RandomStart.Darkest_RandomStart;
using Darkest_RandomStart.SC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Darkest_RandomStart
{
    class Program
    {
        static void Main(string[] args)
        {
            bool party = args.Length > 0 && args[0].Equals("-party", StringComparison.OrdinalIgnoreCase);

            GenerateHeroes(party);
        }
        static void GenerateHeroes(bool party)
        {
            RootObject root = new();
            List<string> firstTwoHeroes = new();
            List<string> lastTwoHeroes = new();

            if (party)
            {
                PartyManager.GetHerosFromParty(out firstTwoHeroes, out lastTwoHeroes);
            }

            PopulateHeroData(root, party, firstTwoHeroes);

            string json = JsonConvert.SerializeObject(root, Formatting.Indented);
            string filePath = @"persist.roster.json";
            FileFunctions.SaveJsonToFile(json, filePath, "scripts/starting_save");
            Console.WriteLine("JSON file has been generated and saved to: " + filePath);

            StageCoach(lastTwoHeroes);
        }
        static void PopulateHeroData(RootObject root, bool party, List<string> firstTwoHeroes)
        {
            root.data.heroes = new Dictionary<string, Hero>();

            if (party && firstTwoHeroes.Count > 0)
            {
                for (int i = 0; i < firstTwoHeroes.Count && i < 2; i++)
                {
                    root.data.heroes[(i + 1).ToString()] = new Hero(firstTwoHeroes[i]);
                }
            }
            else
            {
                root.data.heroes["1"] = new Hero("");
                root.data.heroes["2"] = new Hero("");
            }
        }
        static void StageCoach(List<string> heroes)
        {
            try
            {
                string fileName = "stage_coach.building.json";
                string readDirectory = Path.Combine("..", "..", "campaign", "town", "buildings", "stage_coach");
                string writeDirectory = Path.Combine("campaign", "town", "buildings", "stage_coach");

                SC.RootObject rootObject = FileFunctions.ReadJsonFile<SC.RootObject>(fileName, readDirectory);
                List<string> combinedClasses = heroes.Count > 0 ? heroes : RandomizeHeroClasses();

                if (rootObject.Data.Stores.Count > 0)
                {
                    rootObject.Data.Stores[0].Data.FirstHeroClasses = combinedClasses;
                }
                else
                {
                    throw new Exception("No stores found in the JSON data.");
                }

                string modifiedJson = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
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
