using Enums;
using KMath;
using System;
using System.Collections.Generic;
using UnityEngine;
using Entitas.Unity;
using PlanetTileMap;
namespace Mech
{
    public struct MechProperties
    {
        public int MechID;

        public string Name;

        // Mech's Sprite ID
        public int SpriteID;

        public Vec2f SpriteSize;

        public int XMin, XMax, YMin, YMax;
    }
}
