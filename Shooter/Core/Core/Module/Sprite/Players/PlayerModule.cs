using Core.Module.Sprite.Monster;
using Core.Module.Sprite.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Module.Sprite.Players
{
    public class PlayerModule:SpriteBase
    {
        public PlayerStatusOption PlayerStatus { get; set; }
        public PlayerTypeOption PlayerType { get; set; }
        public string RoomId { get; set; }
        public int TotalScore { get; set; }
        public int PlayerX { get; set; }
        public int PlayerY { get; set; }

        public List<MonsterModule> newMonsterPool { get; set; } = new List<MonsterModule>();
        public List<MonsterModule> removeMonsterPool { get; set; } = new List<MonsterModule>();
    }
}
