using Core.Module.Sprite.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Module.Sprite.Players
{
    public class PlayerModule:SpriteBase
    {
        public PlayerTypeOption PlayerType { get; set; }
        public int TotalScore { get; set; }
    }
}
