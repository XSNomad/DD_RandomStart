using System;
using System.Xml.Linq;

namespace Darkest_RandomStart
{
    public class Actor
    {
        public string name_id { get; set; }
        public int current_hp { get; set; }
        public int stunned { get; set; }
        public int ranks { get; set; }
        public bool combat_ready { get; set; }
        public string damage_source { get; set; }
        public int damage_source_type { get; set; }
        public int damage_type { get; set; }
        public Dictionary<string, object> buff_group { get; set; }
        private static readonly Random random = new();

        private static readonly Dictionary<string, int> HeroSetup =
            new()
            {
                { "man_at_arms", 31 },
                { "crusader", 33 },
                { "highwayman", 23 },
                { "plague_doctor", 22 },
                { "vestal", 24 },
                { "hellion", 26 },
                { "grave_robber", 20 },
                { "occultist", 19 },
                { "leper", 35 },
                { "bounty_hunter", 25 },
                { "jester", 19 },
                { "houndmaster", 21 },
                { "abomination", 26 },
                { "antiquarian", 17 },
                { "arbalest", 27 }
            };

        public Actor(string heroClass)
        {
            name_id = GetActorName();
            current_hp = HeroSetup[heroClass];
            stunned = 0;
            ranks = HeroRankManager.GetNextHeroRank();
            combat_ready = false;
            damage_source = "";
            damage_source_type = 0;
            damage_type = 0;
            buff_group = new Dictionary<string, object>();
        }
        public static string GetActorName()
        {
            // Load and parse the XML file
            string xmlFilePath = GetXmlFilePath();
            XDocument doc = XDocument.Load(xmlFilePath);

            // Extract hero names under <language id="english">
            var heroNames = doc.Root
                                .Elements("language")
                                .FirstOrDefault(l => (string)l.Attribute("id") == "english")
                                ?.Elements("entry")
                                .Select(e => e.Attribute("id")?.Value)
                                .ToList();

            if (heroNames == null || heroNames.Count == 0)
            {
                Console.WriteLine("No hero names found in the XML.");
                return "[ERROR]";
            }
            return GetRandomElement(heroNames);
        }
        private static string GetXmlFilePath()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string xmlFilePath = Path.Combine(currentDirectory, "..", "..", "localization", "names.string_table.xml");
            return xmlFilePath;
        }
        private static T GetRandomElement<T>(List<T> list)
        {
            return list[random.Next(list.Count)];
        }
    }
}
