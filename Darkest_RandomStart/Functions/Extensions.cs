namespace Darkest_RandomStart
{
    public static class Extensions
    {
        private static Random random = new();
        // Extension method to shuffle a list using Fisher-Yates algorithm
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                (list[n], list[k]) = (list[k], list[n]);
            }
        }
    }
}
