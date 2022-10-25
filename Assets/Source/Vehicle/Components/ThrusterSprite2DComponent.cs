using Entitas;
using Enums;
using KMath;
using System.Collections.Generic;
using UnityEngine;

namespace Vehicle
{
    [Vehicle]
    public class ThrusterSprite2DComponent : IComponent
    {
        public int SpriteId;
        public Vec2f Size;

        public Vec2f Position1;
        public Vec2f Position2;
    }
}
