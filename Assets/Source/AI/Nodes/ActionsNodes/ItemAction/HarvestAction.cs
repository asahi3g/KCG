//import UnityEngine

using KMath;
using Enums;

namespace Node.Action
{
    public class HarvestAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.MechPlacementAction; } }
        public override NodeGroup NodeGroup { get { return NodeGroup.ActionNode; } }


        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;

            var entities = planet.EntitasContext.mech.GetGroup(MechMatcher.AllOf(MechMatcher.MechPosition2D));
            foreach (var entity in entities)
            {
                // Mesure to Understand Cursor Inside the Mech
                if (Vec2f.Distance(new Vec2f(x, y), new Vec2f(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y)) < 1.5f)
                {
                    if (entity.hasMechType)
                    {
                        if (entity.mechType.mechType == Mech.MechType.Planter)
                        {
                            if (entity.mechPlanter.PlantGrowth >= 100)
                            {
                                MechEntity plant = planet.EntitasContext.mech.GetEntityWithMechID(entity.mechPlanter.PlantMechID);
                                if (plant.mechType.mechType == Mech.MechType.MajestyPalm)
                                    GameState.ItemSpawnSystem.SpawnItemParticle(planet.EntitasContext, Enums.ItemType.MajestyPalm, entity.mechPosition2D.Value);
                                else if (plant.mechType.mechType == Mech.MechType.MajestyPalm)
                                    GameState.ItemSpawnSystem.SpawnItemParticle(planet.EntitasContext, Enums.ItemType.SagoPalm, entity.mechPosition2D.Value);
                                else if (plant.mechType.mechType == Mech.MechType.DracaenaTrifasciata)
                                    GameState.ItemSpawnSystem.SpawnItemParticle(planet.EntitasContext, Enums.ItemType.DracaenaTrifasciata, entity.mechPosition2D.Value);

                                plant.Destroy();
                                entity.mechPlanter.GotSeed = false;
                                entity.mechPlanter.PlantGrowth = 0;
                                entity.mechPlanter.WaterLevel = 0;
                                entity.mechPlanter.LightLevel = 0;
                                nodeEntity.nodeExecution.State = Enums.NodeState.Success;
                                break;
                            }
                        }
                    }
                }
            }

            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }
    }
}
