using Newtonsoft.Json;

namespace Darkest_RandomStart
{
    public class Skill
    {
        public string Id { get; set; }
        [JsonProperty("hero_classes")]
        public List<string> HeroClasses { get; set; }
    }
}
