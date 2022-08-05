using UnityEngine;
using Enums.Tile;
using KMath;
using Item;
using Animancer;
using HUD;
using PlanetTileMap;
using Mech;
using System.Linq;

namespace TGen
{
    public class Grid
    {
        public GridTile[,] gridTiles;

        public void InitStage1(Vec2i mapSize)
        {
            gridTiles = new GridTile[mapSize.X, mapSize.Y];
        }

        public void InitStage2()
        {

        }

        public void Update()
        {

        }

        public void Draw()
        {

        }

    }
}
