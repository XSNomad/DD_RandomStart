using Newtonsoft.Json;
using System.IO;

namespace Darkest_RandomStart
{
    public class Hero
    {
        private static readonly Random random = new();
        public static readonly string[] nonTankClasses =
        {
            "abomination",
            "antiquarian",
            "arbalest",
            "bounty_hunter",
            "grave_robber",
            "highwayman",
            "houndmaster",
            "jester",
            "plague_doctor"
        };
        public static readonly string[] tankClasses = { "crusader", "hellion", "leper", "man_at_arms" };
        public static readonly string[] healerClasses = { "vestal", "occultist" };
        public string secondHero; 
        [JsonProperty("roster.status")]
        public int RosterStatus { get; set; }

        [JsonProperty("roster.missing_duration")]
        public int RosterMissingDuration { get; set; }

        [JsonProperty("roster.story_variation")]
        public int RosterStoryVariation { get; set; }

        [JsonProperty("roster.missing_from")]
        public int RosterMissingFrom { get; set; }

        [JsonProperty("roster.building_name")]
        public string RosterBuildingName { get; set; }
        public Actor actor { get; set; }
        public string heroClass { get; set; }
        public int resolveXp { get; set; }
        public int stress { get; set; }
        public int weapon_rank { get; set; }
        public int armour_rank { get; set; }
        public int affliction_severity { get; set; }
        public string affliction_type_id { get; set; }
        public Dictionary<string, Quirk> quirks { get; set; }
        public SelectedSkills skills { get; set; }
        public Trinkets trinkets { get; set; }

        [JsonIgnore]
        public QuirkManager quirkManager = new();

        public Hero(string hero)
        {
            if (hero=="")
            {
                heroClass = InitializeRandomHeros();
            }
            else
            {
                heroClass = hero;
            }
            InitializeDefaults();
        }

        private void InitializeDefaults()
        {
            RosterStatus = 1;
            RosterMissingDuration = 0;
            RosterStoryVariation = 0;
            RosterMissingFrom = 0;
            RosterBuildingName = "";
            resolveXp = 0;
            stress = 10;
            weapon_rank = 0;
            armour_rank = 0;
            affliction_severity = 0;
            affliction_type_id = "";
            quirks = quirkManager.GetRandomQuirks();
            skills = new(heroClass);
            actor = new(heroClass);
            trinkets = new();
        }
        private static string InitializeRandomHeros()
        {
            if (!HeroRankManager.TankSelected)
            {
                HeroRankManager.TankSelected = true;
                return tankClasses[random.Next(tankClasses.Length)];
            }
            else
            {
                return nonTankClasses[random.Next(nonTankClasses.Length)];
            }
        }
    }
}
