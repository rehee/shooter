using Core.Module.Rooms;
using Core.Module.Sprite.Players;
using Core.Services.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServices
{
    public class GameService:IGameService
    {
        private List<PlayerModule> playerPool { get; set; } = new List<PlayerModule>();
        public List<IRoomProcessUnit> ProcessPool { get; set; } = new List<IRoomProcessUnit>();

        public List<PlayerModule> Players { get { return playerPool; } }

        public void NewRoom(PlayerModule player)
        {
            var room = new Room();
            
        }

        public void JoinGame(PlayerModule player)
        {
            
        }

        public PlayerModule GetPlayerById(string id)
        {
            try
            {
                var count = playerPool.Count;
                for (var i = 0; i < count; i++)
                {
                    if (playerPool[i].Id == id)
                    {
                        return playerPool[i];
                    }
                }
                return null;
            }
            catch { return null; }
        }

        public IRoomProcessUnit NewRoom()
        {
            var newRoom = new Room();
            newRoom.Id = Guid.NewGuid().ToString();
            var unit = new GameUnit(newRoom);
            ProcessPool.Add(unit);
            return unit;
        }

        public IRoomProcessUnit GetRoomById(string roomId)
        {
            var count = ProcessPool.Count;
            for(var i = 0; i < count; i++)
            {
                if(ProcessPool[i].room.Id == roomId)
                {
                    return ProcessPool[i];
                }
            }
            return null;
        }
    }
}
