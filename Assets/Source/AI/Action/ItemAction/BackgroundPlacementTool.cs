using Enums.PlanetTileMap;
using UnityEngine;

namespace Action
{
    public class BackgroundPlacementTool
    {
        // Action used by either player and AI
        public void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            // Action For Background Placement TOOL

            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }
    }
}
