using Core.Module.Sprite.Monster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Module.Stage
{
    public static class StageExtend
    {
        public static int refreshSeed = 10;
        public static void AddMonster(this StageModule stage,MonsterModule monster)
        {
            stage.monsters.Add(monster);
        }
        public static int GetRefreshTime(this int level)
        {
            var refreshRate = 11 - level;
            if (refreshSeed <= 0)
            {
                return 1;
            }
            return (refreshSeed * refreshRate)/10;
        }
    }
}
