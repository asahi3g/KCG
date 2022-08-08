using KMath;
using UnityEngine;
using Agent;

namespace ECSInput
{
    public class InputProcessSystem
    {
        private Enums.Mode mode = Enums.Mode.CameraOnly;

        private void UpdateMode(ref Planet.PlanetState planetState, AgentEntity agentEntity)
        {
            agentEntity.agentPhysicsState.Invulnerable = false;
            Camera.main.gameObject.GetComponent<CameraMove>().enabled = false;
            planetState.cameraFollow.canFollow = false;

            if (mode == Enums.Mode.Agent)
            {
                Camera.main.gameObject.GetComponent<CameraMove>().enabled = false;
                planetState.cameraFollow.canFollow = true;

            }
            else if (mode == Enums.Mode.Camera)
            {
                Camera.main.gameObject.GetComponent<CameraMove>().enabled = true;
                planetState.cameraFollow.canFollow = false;

            }
            else if(mode == Enums.Mode.CameraOnly)
            {
                Camera.main.gameObject.GetComponent<CameraMove>().enabled = true;
                Camera.main.gameObject.GetComponent<CameraMove>().enabled = false;
            }
            else if (mode == Enums.Mode.Creative)
            {
                Camera.main.gameObject.GetComponent<CameraMove>().enabled = true;
                planetState.cameraFollow.canFollow = false;
                agentEntity.agentPhysicsState.Invulnerable = true;
            }
        }

        public void Update(ref Planet.PlanetState planet)
        {
            Contexts contexts = planet.EntitasContext;

            var AgentsWithXY = contexts.agent.GetGroup(AgentMatcher.AllOf(
                AgentMatcher.ECSInput, AgentMatcher.ECSInputXY));

            float x = 0.0f;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                x += 1.0f;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                x -= 1.0f;
            }

            foreach (var player in AgentsWithXY)
            {

                // Jump
                if (Input.GetKeyDown(KeyCode.UpArrow))
                    GameState.AgentProcessPhysicalState.JumpVelocity(player, 16f); // 16f Intial velocity necessary to jump 3.2 tiles. at 40 tiles/seconds gravity

                // Dash
                if (Input.GetKeyDown(KeyCode.Space))
                    GameState.AgentProcessPhysicalState.Dash(player, x);

                // Running
                if (Input.GetKey(KeyCode.LeftAlt))
                    GameState.AgentProcessPhysicalState.Run(player, x);
                else
                    GameState.AgentProcessPhysicalState.Walk(player, x);

                // JetPack
                if (Input.GetKey(KeyCode.F))
                    GameState.AgentProcessPhysicalState.JetPackFlying(player);

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    player.agentPhysicsState.Droping = true;
                    player.agentPhysicsState.WantToDrop = true;
                }
            }

            // Recharge Weapon.
            if (Input.GetKeyDown(KeyCode.E))
            {
                var players = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer));
                foreach (var player in players) 
                    GameState.ActionCreationSystem.CreateAction(planet.EntitasContext,Enums.ActionType.ChargeAction, player.agentID.ID);
            }

            // Drop Action. 
            if (Input.GetKeyUp(KeyCode.T))
            {
                var players = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer));
                foreach (var player in players)
                    GameState.ActionCreationSystem.CreateAction(planet.EntitasContext, Enums.ActionType.DropAction, player.agentID.ID);
            }

            // Reload Weapon.
            if (Input.GetKeyDown(KeyCode.R))
            {
                var players = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer));
                foreach (var player in players)
                    GameState.ActionCreationSystem.CreateAction(planet.EntitasContext, Enums.ActionType.ReloadAction, player.agentID.ID);
            }

            // Shield Action.
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                var players = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer));
                foreach (var player in players)
                    GameState.ActionCreationSystem.CreateAction(planet.EntitasContext, Enums.ActionType.ShieldAction, player.agentID.ID);

            }

            // Show/Hide Statistics
            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (KGUI.Statistics.StatisticsDisplay.text.GetGameObject().GetComponent<UnityEngine.UI.Text>().enabled)
                    KGUI.Statistics.StatisticsDisplay.text.GetGameObject().GetComponent<UnityEngine.UI.Text>().enabled = false;
                else if (!KGUI.Statistics.StatisticsDisplay.text.GetGameObject().GetComponent<UnityEngine.UI.Text>().enabled)
                    KGUI.Statistics.StatisticsDisplay.text.GetGameObject().GetComponent<UnityEngine.UI.Text>().enabled = true;

            }

            // Remove Tile Front At Cursor Position.
            if (Input.GetKeyDown(KeyCode.F2))
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                planet.TileMap.RemoveFrontTile((int)worldPosition.x, (int)worldPosition.y);
            }

            // Remove Tile Back At Cursor Position.
            if (Input.GetKeyDown(KeyCode.F3))
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                planet.TileMap.RemoveBackTile((int)worldPosition.x, (int)worldPosition.y);
            }

            //  Open Inventory with Tab.        
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                var players = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer, AgentMatcher.AgentInventory));
                foreach (var player in players)
                {
                    int inventoryID = player.agentInventory.InventoryID;
                    InventoryEntity inventoryEntity = contexts.inventory.GetEntityWithInventoryIDID(inventoryID);
                    inventoryEntity.isInventoryDrawable = !inventoryEntity.isInventoryDrawable;
                }
            }

            // Change Pulse Weapon Mode.
            if (Input.GetKeyDown(KeyCode.N))
            {
                var PlayerWithToolBarPulse = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer));
                foreach (var entity in PlayerWithToolBarPulse)
                {
                    int inventoryID = entity.agentInventory.InventoryID;
                    InventoryEntity inventoryEntity = contexts.inventory.GetEntityWithInventoryIDID(inventoryID);
                    if (!GameState.InventoryCreationApi.Get(inventoryEntity.inventoryID.TypeID).HasTooBar())
                        return;

                    var SlotComponent = inventoryEntity.inventoryEntity;
                    var item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, SlotComponent.SelectedSlotID);

                    if (item.itemType.Type == Enums.ItemType.PulseWeapon)
                    {
                        if (!item.itemPulseWeaponPulse.GrenadeMode)
                        {
                            item.itemPulseWeaponPulse.GrenadeMode = true;
                            planet.AddFloatingText("Grenade Mode", 1.0f, Vec2f.Zero, entity.agentPhysicsState.Position);
                        }
                        else
                        {
                            item.itemPulseWeaponPulse.GrenadeMode = false;
                            planet.AddFloatingText("Bullet Mode", 1.0f, Vec2f.Zero, entity.agentPhysicsState.Position);
                        }
                    }
                }
            }

            // Change Item Selection with nums.
            var PlayerWithToolBar = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer));
            foreach (var entity in PlayerWithToolBar)
            {
                int inventoryID = entity.agentInventory.InventoryID;
                InventoryEntity inventoryEntity = contexts.inventory.GetEntityWithInventoryIDID(inventoryID);
                if (!GameState.InventoryCreationApi.Get(inventoryEntity.inventoryID.TypeID).HasTooBar())
                    return;

                for (int i = 0; i < inventoryEntity.inventoryEntity.Width; i++)
                {
                    KeyCode keyCode = KeyCode.Alpha1 + i;
                    if (Input.GetKeyDown(keyCode))
                    {
                        inventoryEntity.inventoryEntity.SelectedSlotID = i;
                        var item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, i);
                        
                        planet.AddFloatingText(item.itemType.Type.ToString(), 2.0f, Vec2f.Zero, new Vec2f(entity.agentPhysicsState.Position.X + 0.4f,
                                    entity.agentPhysicsState.Position.Y));
                    }
                }

            // Remove Tile Back At Cursor Position.
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                if (mode == Enums.Mode.Agent)
                    mode = Enums.Mode.Camera;
                else if (mode == Enums.Mode.Camera)
                    mode = Enums.Mode.CameraOnly;
                else if (mode == Enums.Mode.CameraOnly)
                    mode = Enums.Mode.Creative;
                else if (mode == Enums.Mode.Creative)
                    mode = Enums.Mode.Agent;

                UpdateMode(ref planet, entity);

            }
            }
        }
    }
}