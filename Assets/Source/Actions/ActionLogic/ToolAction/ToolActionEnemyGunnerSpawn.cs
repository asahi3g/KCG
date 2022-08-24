using Entitas;
using UnityEngine;
using KMath;
using Enums.Tile;
using Action;

namespace Action
{
    public class ToolActionEnemyGunnerSpawn : ActionBase
    {
        // Todo create methods to instatiate Agents.
        // Data should only have something like:
        // struct Data
        // {
        //      Enums.enemyType type
        // }

        public ToolActionEnemyGunnerSpawn(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;
            planet.AddAgent(new Vec2f(x, y), Enums.AgentType.EnemyGunner);

            ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);
        }
    }

    // Factory Method
    public class ToolActionEnemyGunnerSpawnCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            return new ToolActionEnemyGunnerSpawn(entitasContext, actionID);
        }
    }
}
