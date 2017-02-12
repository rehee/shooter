using Core.Module.Sprite.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services.Games
{
    public interface IGameService
    {
        List<IRoomProcessUnit> ProcessPool { get; }
        List<PlayerModule> Players { get; }
        IRoomProcessUnit NewRoom();
        IRoomProcessUnit GetRoomById(string roomId);
    }
}
