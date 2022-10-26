//import UnityEngine

using KMath;
using Enums;

namespace Node.Action
{
    public class HarvestAction : NodeBase
    {
        public override NodeType Type => NodeType.HarvestAction;
        public override NodeGroup NodeGroup => NodeGroup.ActionNode;

        public override void OnEnter(NodeEntity nodeEntity)
        {

            ItemInventoryEntity itemEntity = GameState.Planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;
   
            AgentEntity agentEntity = GameState.Planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            MechEntity plant = null;
            MechEntity planter = null;
            if (agentEntity.isAgentPlayer)
            {
                for (int i = 0; i < GameState.Planet.MechList.Length; i++)
                {
                    MechEntity mech = (GameState.Planet.MechList.Get(i));

                    if (mech.mechType.mechType == MechType.Planter)
                        planter = mech;

                    if (!planter.mechPlanter.GotPlant)
                    {
                        planter = null;
                        continue;
                    }
                    plant = GameState.Planet.EntitasContext.mech.GetEntityWithMechID(mech.mechPlanter.PlantMechID);
                    // Is mouse over mech?
                    Vec2f pos = planter.mechPosition2D.Value;
                    Vec2f size = planter.mechSprite2D.Size;
                    bool isOver = true;
                    if (x < pos.X || y < pos.Y)
                        isOver = false;
                    if (x > pos.X + size.X || y > pos.Y + size.Y)
                        isOver = false;
                    if (!isOver)
                    {
                        pos = plant.mechPosition2D.Value;
                        size = plant.mechSprite2D.Size;
                        if (x < pos.X || y < pos.Y)
                        {
                            plant = null;
                            planter = null;
                            continue;
                        }
                        if (x > pos.X + size.X || y > pos.Y + size.Y)
                        {
                            plant = null;
                            planter = null;
                            continue;
                        }
                    }
                    break;
                }
            }
            else
            {
                UnityEngine.Debug.LogError("AI can't use this action. Add blackboard entry with mech target");
                nodeEntity.nodeExecution.State = NodeState.Fail;
                return;
            }

            if (planter != null)
            {
                if (plant.mechPlant.PlantGrowth >= 100)
                {
                    Vec2f spawnPos = new Vec2f(plant.mechPosition2D.Value.X, plant.mechPosition2D.Value.Y + plant.mechSprite2D.Size.Y / 2.0f);
                    if (plant.mechType.mechType == MechType.MajestyPalm)
                        GameState.ItemSpawnSystem.SpawnItemParticle(GameState.Planet.EntitasContext, ItemType.MajestyPalm, spawnPos);
                    else if (plant.mechType.mechType == MechType.SagoPalm)
                        GameState.ItemSpawnSystem.SpawnItemParticle(GameState.Planet.EntitasContext, ItemType.SagoPalm, spawnPos);
                    else if (plant.mechType.mechType == MechType.DracaenaTrifasciata)
                        GameState.ItemSpawnSystem.SpawnItemParticle(GameState.Planet.EntitasContext, ItemType.DracaenaTrifasciata, spawnPos);
                }
                GameState.Planet.RemoveMech(plant.mechID.Index);
                planter.mechPlanter.GotPlant = false;
                planter.mechPlanter.PlantMechID = -1;
                nodeEntity.nodeExecution.State = NodeState.Success;
                return;
            }
            nodeEntity.nodeExecution.State = NodeState.Fail;
        }
    }
}
