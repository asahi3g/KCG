using KMath;
using Enums;

namespace Node
{
    public class ToolActionScanner : NodeBase
    {
        public override NodeType Type { get { return NodeType.ToolActionScanner; } }

        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);

            var entities = planet.EntitasContext.mech.GetGroup(MechMatcher.AllOf(MechMatcher.MechPosition2D));
            foreach (var entity in entities)
            {
                if (Vec2f.Distance(new Vec2f(agentEntity.agentPhysicsState.Position.X, agentEntity.agentPhysicsState.Position.Y), 
                    new Vec2f(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y)) < 1.0f)
                {
                    if (entity.hasMechType)
                    {
                        if (entity.mechType.mechType == Mech.MechType.Planter)
                        {
                            if (entity.hasMechPlanter)
                            {
                                GameState.GUIManager.AddScannerText("Got Seed: " + entity.mechPlanter.GotSeed + " \n" + "Light Status: " + 
                                    entity.mechPlanter.LightLevel + " \n" + "Water Status: " + (int)entity.mechPlanter.WaterLevel + " \n" + 
                                    "Growth Status: " + (int)entity.mechPlanter.PlantGrowth, new Vec2f(-160f, 90.0f), new Vec2f(350, 120), 3.0f);
                            }
                        }
                    }
                }
            }
            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }
    }
}
