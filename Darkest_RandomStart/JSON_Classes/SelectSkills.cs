using Darkest_RandomStart.Darkest_RandomStart;
using Newtonsoft.Json;

namespace Darkest_RandomStart
{
    public class RootObjectSkills
    {
        public List<Skill> Skills { get; set; }
    }
    public class Skill
    {
        public string Id { get; set; }
        [JsonProperty("hero_classes")]
        public List<string> HeroClasses { get; set; }
    }

    public class SelectedSkills
    {
        [JsonProperty("selected_combat_skills")]
        public Dictionary<string, int> SelectedCombatSkills { get; private set; }
        [JsonProperty("selected_camping_skills")]
        public Dictionary<string, int> SelectedCampingSkills { get; private set; }

        private Dictionary<string, List<string>> CombatSkills { get; set; }
        private Dictionary<string, List<string>> CampingSkills { get; set; }

        public SelectedSkills(string heroClass)
        {
            SelectedCombatSkills = SelectRandomSkills(GetCombatSkills(heroClass));
            SelectedCampingSkills = SelectRandomSkills(GetCampingSkills(heroClass));
        }

        private List<string> GetCombatSkills(string heroClass)
        {
            CombatSkills ??= GetHeroSkills();
            return CombatSkills.TryGetValue(heroClass, out var skills) ? skills : new List<string>();
        }

        private List<string> GetCampingSkills(string heroClass)
        {
            CampingSkills ??= GetHeroCampingSkills();
            return CampingSkills.TryGetValue(heroClass, out var skills) ? skills : new List<string>();
        }

        private static Dictionary<string, int> SelectRandomSkills(List<string> skills)
        {
            var selectedSkills = new Dictionary<string, int>();
            skills.Shuffle();

            foreach (var skill in skills.Take(4))
            {
                selectedSkills[skill] = 0; // Initialize skill rank to 0
            }

            return selectedSkills;
        }

        private static Dictionary<string, List<string>> GetHeroSkills()
        {
            var combatSkillsDict = new Dictionary<string, List<string>>();
            string heroesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "heroes");

            if (Directory.Exists(heroesDirectory))
            {
                foreach (var filePath in Directory.GetFiles(heroesDirectory, "*info.darkest", SearchOption.AllDirectories))
                {
                    try
                    {
                        string fileName = Path.GetFileNameWithoutExtension(filePath);
                        int indexOfFirstPeriod = fileName.IndexOf('.');

                        if (indexOfFirstPeriod != -1)
                        {
                            string key = fileName[..indexOfFirstPeriod];
                            var lines = File.ReadAllLines(filePath);
                            var combatSkills = lines
                                .Where(line => line.Trim().StartsWith("combat_skill:") && line.Contains(".level 0"))
                                .Select(line =>
                                {
                                    // Split by space and select the third part
                                    string[] parts = line.Split(' ');
                                    if (parts.Length >= 3)
                                    {
                                        // Trim any special characters from the string
                                        string combatSkillId = parts[2].Trim('"', ' ', '\t', '\r', '\n'); // Trim quotes, spaces, tabs, newlines, etc.
                                        return combatSkillId;
                                    }
                                    return null; // Handle cases where splitting doesn't yield enough parts
                                })
                                .Where(skillId => skillId != null) // Filter out null entries
                                .ToList();

                            combatSkillsDict[key] = combatSkills;
                        }
                        else
                        {
                            Console.WriteLine($"Invalid file name format: {fileName}. Skipping file.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing file {filePath}: {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Heroes directory not found: {heroesDirectory}");
            }
            return combatSkillsDict;
        }

        public static Dictionary<string, List<string>> GetHeroCampingSkills()
        {
            string fileName = "default.camping_skills.json";
            string directory = "../../raid/camping/";
            try
            {
                var rootObject = FileFunctions.ReadJsonFile<RootObjectSkills>(fileName, directory);
                var campingSkills = new Dictionary<string, List<string>>();
                foreach (var skill in rootObject.Skills)
                {
                    foreach (var heroClass in skill.HeroClasses)
                    {
                        if (!campingSkills.ContainsKey(heroClass))
                        {
                            campingSkills[heroClass] = new List<string>();
                        }
                        campingSkills[heroClass].Add(skill.Id);
                    }
                }
                return campingSkills;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
                return new Dictionary<string, List<string>>(); // Handle error gracefully
            }
        }

    }
}
