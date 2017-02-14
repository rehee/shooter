using Core.Module.Sprite.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Module.Sprite
{
    public abstract class SpriteBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public AttributeModule Attributes { get; set; } = new AttributeModule();
        public SpriteTypeOption Type { get; set; }
    }
}
