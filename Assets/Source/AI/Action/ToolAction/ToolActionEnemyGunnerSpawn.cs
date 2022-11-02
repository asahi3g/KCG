using UnityEngine;
using KMath;
using Enums;

namespace Action
{
    public class ToolActionEnemyGunnerSpawn
    {
        public void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;
            planet.AddAgent(new Vec2f(x, y), Enums.AgentType.EnemyHeavy);

            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }
    }
}
