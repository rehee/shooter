using Core.Module.Sprite.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Logic.Controllers
{
    public interface IPlayerController
    {
        void NewPlayer(string name);
        void SetPlayer(string id);
        void AddScore(int score);
        void ChangeBullet(BulletTypeOption bulletType);
        void BulletLevelUp();
        void BulletClear();
        void SetPlayerX(int x);
        void SetPlayerY(int y);
        void JoinRoom(string roomId);
        void CreateRoom();
    }
}
