using Core;
using Core.Module.Rooms;
using Core.Module.Sprite.Monster;
using Core.Module.Sprite.Players;
using Core.Services.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServices
{
    public class GameService : IGameService
    {
        private const int gameTick = 1000/60;
        private List<PlayerModule> playerPool { get; set; } = new List<PlayerModule>();
        private static List<MonsterModule> monsterPool { get; set; } = new List<MonsterModule>();
        private IEnveroment env;
        public Dictionary<string, IRoomProcessUnit> ProcessPool { get; set; } = new Dictionary<string, IRoomProcessUnit>();
        public List<PlayerModule> Players { get { return playerPool; } }
        public GameService(IEnveroment env)
        {
            this.env = env;
            InitMonster();
            GamePlay();
        }

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
            var unit = new GameUnit(newRoom,monsterPool, gameTick);
            ProcessPool.Add(newRoom.Id, unit);
            return unit;
        }

        public IRoomProcessUnit GetRoomById(string roomId)
        {
            if (ProcessPool.ContainsKey(roomId))
                return ProcessPool[roomId];
            return null;
        }
        private void InitMonster()
        {
            var monster = new MonsterModule();
            monster.Name = "xfj";
            monster.Attributes.BulletLevel = 1;
            monster.Attributes.MaxHp = 10;
            monster.Attributes.Hp = 10;
            monsterPool.Add(monster);
        }
        private void GamePlay()
        {
            Task gameLoop = new Task(()=> {
                while (true)
                {
                    System.Threading.Thread.Sleep(gameTick);
                    try
                    {
                        var gameList = ProcessPool.Select(b => b.Value).ToList();
                        var count = gameList.Count;
                        for(var i = 0; i < count; i++)
                        {
                            if (gameList[i].Room.Players.Count <= 0)
                                continue;
                            gameList[i].NextTick();
                        }
                    }
                    catch { }
                }
            });
            gameLoop.Start();
        }
    }
}
