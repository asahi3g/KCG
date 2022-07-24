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
        public Vec2f SpriteSize;

        public TileMaterialType[] TileMaterialTypes;

        public int XMin, XMax, YMin, YMax;

    }
}
