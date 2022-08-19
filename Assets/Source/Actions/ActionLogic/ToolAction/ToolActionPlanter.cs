using Entitas;
using UnityEngine;
using System.Collections.Generic;
using KMath;
using Enums.Tile;
using Action;

namespace Action
{
    public class ToolActionPlanter : ActionBase
    {
        // Mech Property
        private Mech.MechProperties MechProperty;

        // Item Entity
        private ItemInventoryEntity ItemEntity;

        // Mech Entity
        private MechEntity Plant;

        // Plant To Add
        private bool PlantToAdd = false;

        // Planter Position
        private Vec2f PlanterPosition = Vec2f.Zero;


        // Constructor
        public ToolActionPlanter(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
            // Item Entity
            ItemEntity = EntitasContext.itemInventory.GetEntityWithItemID(ActionEntity.actionTool.ItemID);

            // Get Cursor Position
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;

            // Get Mech Entities
            var entities = EntitasContext.mech.GetGroup(MechMatcher.AllOf(MechMatcher.MechPosition2D));
            foreach (var entity in entities)
            {
                // Mesure to Understand Cursor Inside the Mech
                if (Vec2f.Distance(new Vec2f(x, y), new Vec2f(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y)) < 1.5f)
                {
                    // Has Mech Type Component?
                    if (entity.hasMechType)
                    {
                        // Is Mech Planter?
                        if (entity.mechType.mechType == Mech.MechType.Planter)
                        {
                            PlanterPosition = entity.mechPosition2D.Value;

                            // Has Planter Component?
                            if (entity.hasMechPlanter)
                            {
                                // Plant Growth Reached Maximum?
                                if (entity.mechPlanter.PlantGrowth < 100.0f)
                                {
                                    // Is seed placed?
                                    if (!entity.mechPlanter.GotSeed)
                                    {
                                        PlantToAdd = true;
                                        if(Plant != null)
                                        entity.mechPlanter.PlantMechID = Plant.mechID.ID;

                                        // Mech Property
                                        entity.mechPlanter.GotSeed = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (PlantToAdd)
            {
                if(ItemEntity.itemType.Type == Enums.ItemType.MajestyPalm)
                    Plant = planet.AddMech(new Vec2f(PlanterPosition.X, PlanterPosition.Y + 0.85f), Mech.MechType.MajestyPalm);
                else if(ItemEntity.itemType.Type == Enums.ItemType.SagoPalm)
                    Plant = planet.AddMech(new Vec2f(PlanterPosition.X, PlanterPosition.Y + 0.85f), Mech.MechType.SagoPalm);
                else if (ItemEntity.itemType.Type == Enums.ItemType.DracaenaTrifasciata)
                    Plant = planet.AddMech(new Vec2f(PlanterPosition.X, PlanterPosition.Y + 0.85f), Mech.MechType.DracaenaTrifasciata);
            }

            foreach (var entity in entities)
            {
                // Mesure to Understand Cursor Inside the Mech
                if (Vec2f.Distance(new Vec2f(x, y), new Vec2f(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y)) < 1.5f)
                {
                    // Has Mech Type Component?
                    if (entity.hasMechType)
                    {
                        // Is Mech Planter?
                        if (entity.mechType.mechType == Mech.MechType.Planter)
                        {
                            entity.mechPlanter.PlantMechID = Plant.mechID.ID;
                        }
                    }
                }
            }

            // Return True
            ActionEntity.actionExecution.State = Enums.ActionState.Success;
        }

        public override void OnExit(ref Planet.PlanetState planet)
        {
            if (ItemEntity != null)
            {
                GameState.InventoryManager.RemoveItem(planet.EntitasContext, AgentEntity.agentInventory.InventoryID, ItemEntity.itemInventory.SlotID);
                ItemEntity.Destroy();
            }
            base.OnExit(ref planet);
        }
    }

    // Factory Method
    public class ToolActionPlanterCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            // Creation Action Tool
            return new ToolActionPlanter(entitasContext, actionID);
        }
    }
}
