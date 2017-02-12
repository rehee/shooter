using Core.Services.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Module.Sprite.Players;
using Core.Module.Sprite.Status;
using Core.Module.Rooms;

namespace GameServices
{
    public class GameUnit : IRoomProcessUnit
    {
        private Room room;
        public GameUnit(Room room)
        {
            this.room = room;
        }
        public void AddPlayer(PlayerModule player)
        {
            throw new NotImplementedException();
        }

        public void AddPlayerLevel(PlayerModule player)
        {
            throw new NotImplementedException();
        }

        public void AddPlayerScore(PlayerModule player, int score)
        {
            throw new NotImplementedException();
        }

        public void ChangePlayerType(PlayerModule player, BulletTypeOption bulletType)
        {
            throw new NotImplementedException();
        }

        public void MapLevelUp()
        {
            throw new NotImplementedException();
        }

        public void MovePlayer(PlayerModule player)
        {
            throw new NotImplementedException();
        }

        public void NextTick()
        {
            throw new NotImplementedException();
        }

        public string PushMessage()
        {
            throw new NotImplementedException();
        }
    }
}
