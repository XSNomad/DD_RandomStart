namespace Darkest_RandomStart
{
    public class Trinkets
    {
        public string system_config_type { get; set; }
        public Dictionary<string, Trinkets> items { get; set; }

        public Trinkets()
        {
            system_config_type = "hero_equipped_trinkets";
            items = new Dictionary<string, Trinkets>();
        }
    }
}
