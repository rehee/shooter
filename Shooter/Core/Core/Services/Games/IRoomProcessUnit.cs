using Core.Module.Rooms;
using Core.Module.Sprite.Players;
using Core.Module.Sprite.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services.Games
{
    public interface IRoomProcessUnit
    {
        Room room { get; }
        void NextTick();
        string PushMessage();
        void MapLevelUp();
    }
}
