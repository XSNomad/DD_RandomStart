namespace Darkest_RandomStart
{
    public class RootObject
    {
        public int version { get; set; }
        public Data data { get; set; }

        public RootObject()
        {
            version = 513;
            data = new Data();
        }
    }
}
