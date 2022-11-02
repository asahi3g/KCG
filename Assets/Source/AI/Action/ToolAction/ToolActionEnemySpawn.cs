using Entitas;
using UnityEngine;
using KMath;
using Enums;

namespace Action
{
    public class ToolActionEnemySpawn
    {
        public void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;
            planet.AddEnemy(new Vec2f(x, y));

            nodeEntity.nodeExecution.State =  Enums.NodeState.Success;
        }
    }
}
