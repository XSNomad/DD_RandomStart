namespace Darkest_RandomStart
{

    public class Data
    {
        public int nextGuid { get; set; }
        public Dictionary<string, Hero> heroes { get; set; }
        public Dictionary<string, object> last_party { get; set; }
        public Data()
        {
            nextGuid = 5;
            heroes = new Dictionary<string, Hero>();
            last_party = new Dictionary<string, object>()
            {
                { "last_party_guids", new List<int> {-1, -1, 2, 1} }
            };
        }
    }
}
