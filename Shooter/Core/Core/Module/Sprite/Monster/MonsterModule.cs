using Core.Module.Sprite.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Module.Sprite.Monster
{
    public class MonsterModule:SpriteBase
    {
        public MonsterTypeOption MonsterType { get; set; }
        public int MonsterX { get; set; }
    }
}
