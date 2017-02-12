using Core.Module.Sprite;
using Core.Module.Sprite.Players;
using Core.Module.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Module.Rooms
{
    public class Room
    {
        public string Id { get; set; }
        public DateTime Date { get; } = DateTime.Now;
        public List<PlayerModule> Players { get; set; } = new List<PlayerModule>();
        public StageModule Stage { get; set; }
    }
}
