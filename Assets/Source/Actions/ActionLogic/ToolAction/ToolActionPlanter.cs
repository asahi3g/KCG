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

        private MechEntity Plant;

        private bool PlantToAdd = false;

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
                if (Vector2.Distance(new Vector2(x, y), new Vector2(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y)) < 1.5f)
                {
                    // Has Mech Type Component?
                    if (entity.hasMechType)
                    {
                        // Is Mech Planter?
                        if (entity.mechType.mechType == Mech.MechType.Planter)
                        {
                            PlanterPosition = entity.mechPosition2D.Value;

                            // Has Planter Component?
                            if (entity.hasMechPlanterPlanter)
                            {
                                // Plant Growth Reached Maximum?
                                if (entity.mechPlanterPlanter.PlantGrowth < 100.0f)
                                {
                                    // Is seed placed?
                                    if (!entity.mechPlanterPlanter.GotSeed)
                                    {
                                        PlantToAdd = true;
                                        entity.mechPlanterPlanter.Plant = Plant;

                                        // Mech Property
                                        entity.mechPlanterPlanter.GotSeed = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (PlantToAdd)
            {
                Plant = planet.AddMech(new Vec2f(PlanterPosition.X, PlanterPosition.Y + 0.85f), Mech.MechType.Plant);
            }

            foreach (var entity in entities)
            {
                // Mesure to Understand Cursor Inside the Mech
                if (Vector2.Distance(new Vector2(x, y), new Vector2(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y)) < 1.5f)
                {
                    // Has Mech Type Component?
                    if (entity.hasMechType)
                    {
                        // Is Mech Planter?
                        if (entity.mechType.mechType == Mech.MechType.Planter)
                        {
                            entity.mechPlanterPlanter.Plant = Plant;
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
