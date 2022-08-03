using Entitas;
using UnityEngine;
using System.Collections.Generic;
using KMath;
using Enums.Tile;
using Action;

namespace Action
{
    public class ToolActionScanner : ActionBase
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
        public ToolActionScanner(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
            // Item Entity
            ItemEntity = EntitasContext.itemInventory.GetEntityWithItemID(ActionEntity.actionTool.ItemID);

            // Get Mech Entities
            var entities = EntitasContext.mech.GetGroup(MechMatcher.AllOf(MechMatcher.MechPosition2D));
            foreach (var entity in entities)
            {
                // Mesure to Understand Cursor Inside the Mech
                if (Vector2.Distance(new Vector2(AgentEntity.agentPosition2D.Value.X, AgentEntity.agentPosition2D.Value.Y), new Vector2(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y)) < 1.0f)
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
                                HUD.HUDManager.guiManager.scannerToolGUI.ActivateGUIOverlay(entity.mechPlanter.GotSeed, entity.mechPlanter.LightLevel, entity.mechPlanter.WaterLevel,
                                     entity.mechPlanter.PlantGrowth, 3.0f);
                            }
                        }
                    }
                }
            }

            // Return True
            ActionEntity.actionExecution.State = Enums.ActionState.Success;
        }

        public override void OnExit(ref Planet.PlanetState planet)
        {
            base.OnExit(ref planet);
        }
    }

    // Factory Method
    public class ToolActionScannerCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            // Creation Action Tool
            return new ToolActionScanner(entitasContext, actionID);
        }
    }
}
