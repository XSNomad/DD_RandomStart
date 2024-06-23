using Darkest_RandomStart.Darkest_RandomStart;
using Newtonsoft.Json;

namespace Darkest_RandomStart
{
    public class SelectedSkills
    {
        [JsonProperty("selected_combat_skills")]
        public Dictionary<string, int> SelectedCombatSkills { get; private set; }

        [JsonProperty("selected_camping_skills")]
        public Dictionary<string, int> SelectedCampingSkills { get; private set; }

        private readonly HeroDataManager _heroDataHandler;

        public SelectedSkills(string heroClass)
        {
            _heroDataHandler = new HeroDataManager();

            if (heroClass == "abomination")
            {
                SelectedCombatSkills = InitializeSkills(_heroDataHandler.GetCombatSkills(heroClass));
            }
            else
            {
                SelectedCombatSkills = InitializeSkills(SelectRandomSkills(_heroDataHandler.GetCombatSkills(heroClass)));
            }

            SelectedCampingSkills = InitializeSkills(SelectRandomSkills(_heroDataHandler.GetCampingSkills(heroClass)));
        }

        private static Dictionary<string, int> InitializeSkills(IEnumerable<string> skills)
        {
            var selectedSkills = new Dictionary<string, int>();
            foreach (var skill in skills)
            {
                selectedSkills[skill] = 0; // Initialize skill rank to 0
            }
            return selectedSkills;
        }

        private static IEnumerable<string> SelectRandomSkills(IEnumerable<string> skills)
        {
            var skillList = skills.ToList();
            skillList.Shuffle();
            return skillList.Take(4); // Shuffle and take 4 random skills
        }
    }
}
