using Core.Logic.Controllers;
using Core.Module.Sprite.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Module.Sprite.Status;
using Core.Services.Games;

namespace GameLogic.GameController
{
    public class PlayerControl : IPlayerController
    {
        private PlayerModule player;
        private IGameService gameService;
        private IRoomProcessUnit gameRoom;
        public PlayerControl(IGameService gameService)
        {
            this.gameService = gameService;
        }
        public PlayerModule Player { get { return player; } } 
        public IRoomProcessUnit Room { get { return gameRoom; } }
        public void AddScore(int score)
        {

            player.TotalScore = player.TotalScore + score;
        }
        public void BulletClear()
        {
            player.Attributes.BulletLevel = 1;
        }
        public void BulletLevelUp()
        {
            player.Attributes.BulletLevel++;
        }
        public void ChangeBullet(BulletTypeOption bulletType)
        {
            player.Attributes.BulletType = bulletType;
        }
        public void CreateRoom()
        {
            gameRoom=gameService.NewRoom();
            gameRoom.Room.Players.Add(player.Id,player);
        }
        public void JoinRoom(string roomId)
        {
            gameRoom = gameService.GetRoomById(roomId);
            gameRoom.Room.Players.Add(player.Id, player);
        }
        public void LeftRoom()
        {
            try
            {
                gameRoom.Room.Players.Remove(player.Id);
            }
            catch { }
        }
        public void NewPlayer(string name)
        {
            this.player = new PlayerModule();
            player.Name = name;
            player.Id = Guid.NewGuid().ToString();
            this.gameService.Players.Add(player);
        }

        public void SetPlayer(string id)
        {
            var count = this.gameService.Players.Count;
            for(var i = 0; i < count; i++)
            {
                if (this.gameService.Players[i].Id == id)
                {
                    player = this.gameService.Players[i];
                }
            }
        }

        public void SetPlayerX(int x)
        {
            if (player == null)
                return;
            player.PlayerX = x;
        }

        public void SetPlayerY(int y)
        {
            if (player == null)
                return;
            player.PlayerY = y;
        }
    }
}
