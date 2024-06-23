
namespace Darkest_RandomStart
{
    public class HeroDataManager
    {
        private Dictionary<string, List<string>> _combatSkills;
        private readonly string _heroesDirectory;

        public HeroDataManager()
        {
            _heroesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "heroes");
            LoadCombatSkills();
        }

        private void LoadCombatSkills()
        {
            _combatSkills = new Dictionary<string, List<string>>();

            if (Directory.Exists(_heroesDirectory))
            {
                foreach (var filePath in Directory.GetFiles(_heroesDirectory, "*info.darkest", SearchOption.AllDirectories))
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
                                    string[] parts = line.Split(' ');
                                    if (parts.Length >= 3)
                                    {
                                        string combatSkillId = parts[2].Trim('"', ' ', '\t', '\r', '\n');
                                        return combatSkillId;
                                    }
                                    return null;
                                })
                                .Where(skillId => skillId != null)
                                .ToList();

                            _combatSkills[key] = combatSkills;
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
                Console.WriteLine($"Heroes directory not found: {_heroesDirectory}");
            }
        }

        public List<string> GetCombatSkills(string heroClass)
        {
            return _combatSkills.TryGetValue(heroClass, out var skills) ? skills : new List<string>();
        }
        public List<string> GetCampingSkills(string heroClass)
        {
            return _combatSkills.TryGetValue(heroClass, out var skills) ? skills : new List<string>();
        }
    }
}
