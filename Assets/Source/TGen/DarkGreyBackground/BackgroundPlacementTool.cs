//imports UnityEngine

using KMath;
using System;
using UnityEngine;
using Utility;

namespace TGen.DarkGreyBackground
{
    public class BackgroundPlacementTool
    {
        public void Initialize(Vec2i mapSize, Material Atlas, Transform transform)
        {
            GameState.Planet.InitializePlaceableBackground(Atlas, transform);

            GameState.BackgroundGrid.InitStage1(mapSize);

            GameState.BackgroundGridOverlay.Initialize(Atlas, transform, mapSize.X, mapSize.Y, 30);

            GameState.BackgroundRenderMapBorder.Initialize(Atlas, transform, mapSize.X - 1, mapSize.Y - 1, 31);
        }

        public void UpdateToolGrid(PlanetTileMap.TileMap tileMap)
        {
            var item = GameState.Planet.Player.GetItem();
            //if(item != null)
            //{
                //if(item.itemType.Type == Enums.ItemType.PlaceableBackgroundTool)
                //{
                    if(UnityEngine.Input.GetMouseButtonUp(0))
                    {
                        var worldPosition = ECSInput.InputProcessSystem.GetCursorWorldPosition();

                        int x = (int)worldPosition.X;
                        int y = (int)worldPosition.Y;

                        tileMap.SetBackTile(x, y, Enums.PlanetTileMap.TileID.Planet1);
                    }
                    else if (UnityEngine.Input.GetMouseButtonDown(1))
                    {
                        var worldPosition = ECSInput.InputProcessSystem.GetCursorWorldPosition();

                        int x = (int)worldPosition.X;
                        int y = (int)worldPosition.Y;

                        tileMap.RemoveBackTile(x, y);
                    }
                //}
           // }
        }
    }
}