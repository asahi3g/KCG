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
                            if (entity.hasMechPlanter)
                            {
                                // Plant Growth Reached Maximum?
                                if (entity.mechPlanter.PlantGrowth < 100.0f)
                                {
                                    // Is seed placed?
                                    if (!entity.mechPlanter.GotSeed)
                                    {
                                        PlantToAdd = true;
                                        entity.mechPlanter.Plant = Plant;

                                        // Mech Property
                                        entity.mechPlanter.GotSeed = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            int random = Random.Range(0, 4);
            Debug.Log("Random: " + random);

            if (PlantToAdd)
            {
                switch (random)
                {
                    case 0:
                        Plant = planet.AddMech(new Vec2f(PlanterPosition.X, PlanterPosition.Y + 0.85f), Mech.MechType.Plant);
                        break;
                    case 1:
                        Plant = planet.AddMech(new Vec2f(PlanterPosition.X, PlanterPosition.Y + 0.85f), Mech.MechType.Plant2);
                        break;
                    case 2:
                        Plant = planet.AddMech(new Vec2f(PlanterPosition.X, PlanterPosition.Y + 0.85f), Mech.MechType.Plant3);
                        break;
                    case 3:
                        Plant = planet.AddMech(new Vec2f(PlanterPosition.X, PlanterPosition.Y + 0.85f), Mech.MechType.Plant4);
                        break;
                }
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
                            entity.mechPlanter.Plant = Plant;
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
