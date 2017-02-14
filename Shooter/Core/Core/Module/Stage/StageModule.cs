using Core.Module.Sprite.Monster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Module.Stage
{
    public class StageModule
    {
        public int stageLevel { get; set; } = 1;
        public List<MonsterModule> monsters { get; set; } = new List<MonsterModule>();

    }
}
