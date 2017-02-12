using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Module.Sprite.Status
{
    public class AttributeModule
    {
        public int level { get; set; }
        public int MaxHp { get; set; }
        public int Hp { get; set; }
        public BulletTypeOption BulletType { get; set; }
        public int BulletLevel { get; set; }
    }
}
