using Core.Services.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Logic.Controllers;
using Core.Module.Sprite.Players;
using Core.Services.Games;

namespace Hubs
{
    public class HubProcessUnit : IHubProcessUnit
    {
        private static int count = 0;
        private IPlayerController playerControl;
        private List<string> command = new List<string>();
        private List<string> deleteCommand = new List<string>();
        private IGameService gameService;
        private bool showRoomList = false;
        public HubProcessUnit(IPlayerController playerControl, IGameService gameService)
        {
            this.playerControl = playerControl;
            this.gameService = gameService;
        }
        public IPlayerController player
        {
            get
            {
                return playerControl;
            }
        }
        public bool UnitActive { get; set; } = false;
        public void DoCommand(List<string> command)
        {
            ProcessCommand(command);
        }
        public string PushMessage()
        {
            if (!showRoomList)
            {
                return PushNormanData();
            }
            else
            {
                return PushGameRoom();
            }

        }

        private string PushNormanData()
        {
            string playerId = "";
            if (playerControl.Player != null)
                playerId = playerControl.Player.Id;
            string roomId = "";
            int roomLevel = 0;
            try
            {
                var room = playerControl.Room.Room;
                roomId = room.Id;
                roomLevel = room.Stage.stageLevel;
            }
            catch { }
            string destoryID = "";
            try
            {
                var destoryCount = player.Player.removeMonsterPool.Count();
                if (destoryCount > 0)
                    destoryID = player.Player.removeMonsterPool[0].Id;
                player.Player.removeMonsterPool.RemoveAt(0);

            }
            catch { }


            var monsterId = "";
            var monsterX = 100;
            try
            {
                var newMonster = player.Player.newMonsterPool.FirstOrDefault();
                if (newMonster != null)
                {
                    monsterId = newMonster.Id;
                    monsterX = newMonster.MonsterX;
                }
                    

                player.Player.newMonsterPool.RemoveAt(0);
            }
            catch { }
            var playerList = new List<List<List<string>>>();

            try
            {
                //playerList.Add(new List<List<string>>() { new List<string>() { player.Player.Id }, new List<string>() { player.Player.PlayerX.ToString() }, new List<string>() { player.Player.PlayerY.ToString() } });
                foreach (var item in player.Room.Room.Players)
                {
                    if (playerId == "")
                        break;
                    if (item.Value.Id == playerId)
                        continue;
                    playerList.Add(new List<List<string>>() { new List<string>() { item.Value.Id}, new List<string>() { item.Value.PlayerX.ToString() }, new List<string>() { item.Value.PlayerY.ToString() } });
                }
            }
            catch { }


            var c2Object = new
            {
                c2dictionary = true,
                data = new
                {
                    me_id = playerId,
                    roomId = roomId,
                    roomLevel = roomLevel,
                    monster_Id = monsterId,
                    monster_X = monsterX,
                    destory_Id = destoryID,
                    players = C2ArrayJson(playerList, 3)

                }
            };
            return c2Object.ToJson();
        }

        private string PushGameRoom()
        {
            try
            {
                var rooms = gameService.ProcessPool
                    .Select(b => b.Value)
                    .Where(b => b.Room.Players.Count > 0)
                    .OrderBy(b => b.Room.Players.Count).Take(10)
                    .ToList();
                var roomList = new List< List<List<string>>>();
                foreach(var item in rooms)
                {

                    roomList.Add(new List<List<string>>() { new List<string>() { item.Room.Id }, new List<string>() { item.Room.Players.Count.ToString() } });
                }
                var c2Object = new
                {
                    c2dictionary = true,
                    data = new
                    {
                        c=C2ArrayJson(roomList, 2)
                    }
                };
                return c2Object.ToJson();

            }
            catch
            {
                return "";
            }
        }


        public void SetPlayerControl(string PlayerId)
        {
            playerControl.SetPlayer(PlayerId);
        }

        public void LeftRoom()
        {
            if (playerControl == null)
                return;
            playerControl.LeftRoom();
        }


        private void ProcessCommand(List<string> command)
        {
            if (command == null || command.Count == 0)
                return;
            switch (command[0])
            {
                case "newGame":
                    CreateNewGame(command);
                    break;
                case "damage":
                    DamageMonster(command);
                    break;
                case "rooms":
                    DisplayRoom(command);
                    break;
                case "join":
                    JoinGameRoom(command);
                    break;
                case "setPlayer":
                    SetPlayer(command);
                    break;
                default:
                    return;
            }
        }

        private void SetPlayer(List<string> command)
        {
            if (command.Count < 3)
            {
                return;
            }
            int x;
            int y;
            if (!int.TryParse(command[1], out x) || !int.TryParse(command[2], out y))
                return;
            player.Player.PlayerX = x;
            player.Player.PlayerY = y;
        }

        private void DisplayRoom(List<string> command)
        {
            if (command == null || command.Count <= 1)
            {
                showRoomList = false;
            }
            else
            {
                showRoomList = true;
            }

        }

        private void DamageMonster(List<string> command)
        {
            try
            {
                var monster = playerControl.Room.Room.Stage.monsters.Where(b => b.Id == command[1]).FirstOrDefault();
                if (monster == null)
                    return;
                monster.Attributes.Hp = monster.Attributes.Hp - Convert.ToInt32(command[2]);
            }
            catch { }
        }

        private void CreateNewPlayer(List<string> command)
        {
            if (command == null)
                return;
            string playerName = "";
            if (command.Count <= 1)
            {
                playerName = "player";
            }
            else
            {
                playerName = command[1];
            }
            playerControl.NewPlayer(playerName);
        }

        
        
        private void CreateNewGame(List<string> command)
        {
            CreateNewPlayer(command);
            playerControl.CreateRoom();
            JoinGame();
        }
        private void JoinGameRoom(List<string> command)
        {
            if(command==null || command.Count < 2)
            {
                CreateNewGame(new List<string>() { "newGame" });
            }
            var name = "player";
            if (command.Count >= 3)
            {
                name = command[2];
            }
            CreateNewPlayer(new List<string>() { "newGame", name });
            playerControl.JoinRoom(command[1]);
        }

        private void JoinGame()
        {
            playerControl.JoinRoom(playerControl.Room.Room.Id);
        }


        private string C2ArrayJson<T>(List<T> list, int width=1, int deep=1)
        {
            var c2Array = new
            {
                c2array = true,
                size = new int[3] { list.Count, width, deep },
                data = list
            };
            return c2Array.ToJson();
        }

    }
}
