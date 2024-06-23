using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darkest_RandomStart
{
    public static class HeroRankManager
    {
        private static int HeroCounter = 0;
        public static bool TankSelected = false;

        public static int GetNextHeroRank()
        {
            return ++HeroCounter;
        }
    }

}
