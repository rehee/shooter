using Core.Module.Sprite.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services.Games
{
    public interface IGameService
    {
        Dictionary<int,IRoomProcessUnit> ProcessPool { get; }
        List<PlayerModule> Players { get; }
        void JoinGame(PlayerModule player);
        void NewRoom(PlayerModule player);
        PlayerModule GetPlayerById(string id);
    }
}
