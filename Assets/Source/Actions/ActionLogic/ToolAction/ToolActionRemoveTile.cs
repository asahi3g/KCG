using Entitas;
using UnityEngine;
using KMath;
using Enums.Tile;

namespace Action
{
    public class ToolActionRemoveTile : ActionBase
    {
        public ToolActionRemoveTile(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)worldPosition.x;
            int y = (int)worldPosition.y;
            
            if (x >= 0 && x < planet.TileMap.MapSize.X &&
            y >= 0 && y < planet.TileMap.MapSize.Y)
            {
                ref PlanetTileMap.TileProperty tileProprieties = ref GameState.TileCreationApi.GetTileProperty(planet.TileMap.GetFrontTileID(x, y));
                GameState.LootDropSystem.Add(tileProprieties.DropTableID, new Vec2f(x, y));

                planet.TileMap.RemoveFrontTile(x, y);
            }
            
            ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);

        }
    }

    // Factory Method
    public class ToolActionRemoveTileCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            return new ToolActionRemoveTile(entitasContext, actionID);
        }
    }
}
