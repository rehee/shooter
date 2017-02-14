using Core.Services.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Module.Sprite.Players;
using Core.Module.Sprite.Status;
using Core.Module.Rooms;
using Core.Module.Stage;
using Core.Module.Sprite.Monster;

namespace GameServices
{
    public class GameUnit : IRoomProcessUnit
    {
        private int secondCount = 0;
        private int gameTick;
        private Room room;
        private List<MonsterModule> monsterPool;
        public GameUnit(Room room, List<MonsterModule> monsterPool,int gameTick)
        {
            this.room = room;
            this.monsterPool = monsterPool;
            this.gameTick = gameTick;
        }

        public Room Room { get { return room; } }

        public void AddPlayer(PlayerModule player)
        {
            if (room.Players.ContainsKey(player.Id))
                return;
            try
            {
                room.Players.Add(player.Id, player);
            }
            catch { }
        }

        public void NextTick()
        {
            secondCount++;
            if (secondCount >= room.Stage.stageLevel.GetRefreshTime()*(1000/gameTick))
            {
                var monster = new MonsterModule();
                monster.Id = Guid.NewGuid().ToString();
                monster.Name = monsterPool[0].Name;
                monster.Attributes.Hp = monsterPool[0].Attributes.Hp;
                monster.Attributes.MaxHp = monsterPool[0].Attributes.MaxHp;
                monster.Attributes.BulletLevel= monsterPool[0].Attributes.BulletLevel;
                monster.MonsterX = G.GetRandomInt(330, 30);
                room.Stage.AddMonster(monster);
                secondCount = 0;
                try
                {
                    foreach(var item in room.Players)
                    {
                        item.Value.newMonsterPool.Add(monster);
                    }
                }
                catch { }
            }
            var count = room.Stage.monsters.Count();
            var deadMonster = new List<MonsterModule>();
            for (var i = 0; i < count; i++)
            {
                if (room.Stage.monsters[i].Attributes.Hp <= 0)
                {
                    deadMonster.Add(room.Stage.monsters[i]);
                }

            }
            count = deadMonster.Count();
            for (var i = 0; i < count; i++)
            {
                try
                {
                    foreach (var item in room.Players)
                    {
                        item.Value.removeMonsterPool.Add(deadMonster[i]);
                    }
                }
                catch { }
                room.Stage.monsters.Remove(deadMonster[i]);
            }
        }

        public string PushMessage()
        {
            return "";
        }
    }
}
