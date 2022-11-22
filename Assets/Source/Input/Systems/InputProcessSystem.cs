//imports UnityEngine

using Agent;
using Enums;
using Inventory;
using KGUI.Statistics;
using KMath;
using Mech;
using PlanetTileMap;
using UnityEngine;
using UnityEngine.UI;

namespace ECSInput
{
    public class InputProcessSystem
    {
        private Mode mode = Mode.Agent;

        private void UpdateMode(AgentEntity agentEntity)
        {
            ref var planet = ref GameState.Planet;
            agentEntity.agentPhysicsState.Invulnerable = false;
            UnityEngine.Camera.main.gameObject.GetComponent<CameraMove>().enabled = false;
            planet.CameraFollow.canFollow = false;

            if (mode == Mode.Agent)
            {
                UnityEngine.Camera.main.gameObject.GetComponent<CameraMove>().enabled = false;
                planet.CameraFollow.canFollow = true;

            }
            else if (mode == Mode.Camera)
            {
                UnityEngine.Camera.main.gameObject.GetComponent<CameraMove>().enabled = true;
                planet.CameraFollow.canFollow = false;
            }
        }

        public float scale = 1.0f;

        private void UpdateMainCameraZoom()
        {
            scale += UnityEngine.Input.GetAxis("Mouse ScrollWheel") * 0.5f * scale;
            Camera.main.orthographicSize = 20.0f / scale;
        }

        public void Update()
        {
            ref var planet = ref GameState.Planet;
            Contexts contexts = planet.EntitasContext;

            var AgentsWithXY = contexts.agent.GetGroup(AgentMatcher.AllOf(
                AgentMatcher.ECSInput, AgentMatcher.ECSInputXY));

            //UpdateMainCameraZoom();

            int x = 0;
            if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.D) && mode == Mode.Agent)
            {
                x += 1;
            }
            if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.A) && mode == Mode.Agent)
            {
                x -= 1;
            }

            foreach (var player in AgentsWithXY)
            {

                var physicsState = player.agentPhysicsState;

                

                // Jump
                if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.W) && mode == Mode.Agent)
                {
                    player.Jump();
                }
                // Dash
                if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Space) && mode == Mode.Agent)
                {
                    player.Dash(x);
                }

                // Attack
                if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.K) && mode == Mode.Agent)
                {
                    player.Roll(x);
                }


                // Running
                if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.LeftAlt))
                {
                    if(mode == Mode.Agent)
                    player.Run(x);
                }
                else
                {
                    if(mode == Mode.Agent)
                    player.Walk(x);
                }

                if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.LeftControl) && mode == Mode.Agent)
                {
                    if(mode == Mode.Agent)
                    player.CrouchBegin(x);
                }
                else
                {
                    if(mode == Mode.Agent)
                    player.CrouchEnd(x);
                }

                // JetPack
                if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.F) && player.agentStats.Fuel.GetValue() > 0)
                {
                    player.JetPackFlyingBegin();
                }
                else
                {
                    player.JetPackFlyingEnd();
                }

                if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.S))
                {
                    if(mode == Mode.Agent)
                    player.Walk(x);
                }

                var mouseWorldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);

                if (player.CanFaceMouseDirection())
                {
                    if (mouseWorldPosition.x >= physicsState.Position.X)
                    {
                        physicsState.FacingDirection = 1;
                    }
                    else
                    {
                        physicsState.FacingDirection = -1;
                    }
                }
                else
                {
                    physicsState.FacingDirection = physicsState.MovingDirection;
                }

                // JetPack
                if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.F))
                {
                    player.JetPackFlyingBegin();
                }

                if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.DownArrow))
                {
                    player.agentPhysicsState.Droping = true;
                }
            }


            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.E))
            {
                var players = contexts.agent.GetGroup(AgentMatcher.AgentPlayer);
                var mechEntities = contexts.mech.GetGroup(MechMatcher.MechID);

                int inventoryID;
                InventoryEntity playerInventory;
                InventoryEntity equipmentInventory;

                foreach (var player in players)
                {
                    if (player.isAgentPlayer)
                    {
                        inventoryID = player.agentInventory.InventoryID;
                        playerInventory = contexts.inventory.GetEntityWithInventoryID(inventoryID);

                        inventoryID = player.agentInventory.EquipmentInventoryID;
                        equipmentInventory = contexts.inventory.GetEntityWithInventoryID(inventoryID);

                        foreach (var mech in mechEntities)
                        {
                            if (mech.mechType.mechType == MechType.CraftingTable)
                            {
                                if (mech.mechCraftingTable.InputInventory.hasInventoryDraw ||
                                    mech.mechCraftingTable.OutputInventory.hasInventoryDraw)
                                {
                                    mech.mechCraftingTable.InputInventory.hasInventoryDraw = false;
                                    mech.mechCraftingTable.OutputInventory.hasInventoryDraw = false;

                                    GameState.InventoryManager.CloseInventory(planet.InventoryList, playerInventory);
                                    GameState.InventoryManager.CloseInventory(planet.InventoryList, equipmentInventory);
                                }

                                if (Vec2f.Distance(player.agentPhysicsState.Position, mech.mechPosition2D.Value) < 2.0f)
                                {
                                    GameState.InventoryManager.OpenInventory(playerInventory);
                                    GameState.InventoryManager.OpenInventory(equipmentInventory);

                                    mech.mechCraftingTable.InputInventory.hasInventoryDraw = true;
                                    mech.mechCraftingTable.OutputInventory.hasInventoryDraw = true;
                                }
                            }
                        }
                    }

                    var vehicles = contexts.vehicle.GetGroup(VehicleMatcher.VehicleID);
                    foreach (var vehicle in vehicles)
                    {
                        // Scan near vehicles.
                        // Hop in when keycode is called.

                        if (Vec2f.Distance(player.agentPhysicsState.Position, vehicle.vehiclePhysicsState2D.Position) < 3.0f || vehicle.vehicleType.HasAgent)
                        {
                            // If player is outside the vehicle.
                            // Get in, turn on the jet and ignition.

                            // If player is inside the vehicle,
                            // Get out, turn off the jet and ignition.

                            if (player.agentModel3D.GameObject.gameObject.activeSelf)
                            {
                                // Set custom events for different vehicle types.
                                // Spew out smoke when accelerate.

                                if (vehicle.vehicleType.Type == VehicleType.DropShip)
                                {
                                    GameState.VehicleAISystem.Initialize(vehicle, new Vec2f(1.1f, -0.6f), new Vec2f(0f, 3.0f));

                                    // Player Gets inside of Rocket
                                    // Hide Agent/Player
                                    player.agentModel3D.GameObject.gameObject.SetActive(false);
                                    player.isAgentAlive = false;
                                    vehicle.vehicleType.HasAgent = true;

                                    GameState.VehicleAISystem.RunAI(vehicle, new Vec2f(1.1f, -0.6f), new Vec2f(0f, 3.0f));

                                    vehicle.vehiclePhysicsState2D.angularVelocity = new Vec2f(0, 3.0f);
                                    vehicle.vehicleThruster.Jet = true;
                                    vehicle.vehiclePhysicsState2D.AffectedByGravity = false;
                                }
                                else
                                {
                                    GameState.VehicleAISystem.Initialize(vehicle, new Vec2f(1.1f, -2.8f), new Vec2f(0f, 3.0f));

                                    // Player Gets inside of Rocket
                                    // Hide Agent/Player
                                    player.agentModel3D.GameObject.gameObject.SetActive(false);
                                    player.isAgentAlive = false;
                                    vehicle.vehicleType.HasAgent = true;

                                    if (vehicle.hasVehicleThruster)
                                    {
                                        vehicle.vehicleThruster.Jet = false;
                                        vehicle.vehiclePhysicsState2D.AffectedByGravity = true;
                                        vehicle.vehiclePhysicsState2D.angularVelocity = new Vec2f(0, 0.0f);
                                    }

                                    GameState.VehicleAISystem.RunAI(vehicle, new Vec2f(1.1f, -2.8f), new Vec2f(0f, 3.0f));
                                }

                            }
                            else
                            {
                                vehicle.vehiclePhysicsState2D.AffectedByGravity = true;
                                GameState.VehicleAISystem.StopAI();

                                vehicle.vehicleType.HasAgent = false;
                                player.agentPhysicsState.Position = vehicle.vehiclePhysicsState2D.Position;
                                player.agentModel3D.GameObject.gameObject.SetActive(true);
                                player.isAgentAlive = true;

                            }

                        }
                    }


                    InventoryEntity inventory = null;
                    float smallestDistance = 2.0f;
                    var agents = planet.AgentList;
                    for (int i =0; i < agents.Length; i++)
                    {
                        AgentEntity corpse = agents.Get(i);
                        if (!corpse.isAgentAlive)
                        {
                            
                            var physicsState = corpse.agentPhysicsState;
                            float distance = Vec2f.Distance(physicsState.Position, player.agentPhysicsState.Position);

                            if (!corpse.hasAgentInventory || !(distance < smallestDistance))
                                continue;

                            smallestDistance = distance;

                            inventory = contexts.inventory.GetEntityWithInventoryID(corpse.agentInventory.InventoryID);
                        }
                    }


                    var mechs = contexts.mech.GetEntities();
                    foreach (var mech in mechs)
                    {
                        float distance = Vec2f.Distance(mech.mechPosition2D.Value, player.agentPhysicsState.Position);
                        if (!(distance < smallestDistance))
                            continue;

                        distance = smallestDistance;
                        inventory = null;

                        if (mech.hasMechInventory)
                            inventory = contexts.inventory.GetEntityWithInventoryID(mech.mechInventory.InventoryID);

                        // Get proprietis.
                        MechProperties mechProperties = mech.GetProperties();
                        if (mechProperties.Action != NodeType.None)
                            GameState.ActionCreationSystem.CreateAction(mechProperties.Action, player.agentID.ID);
                    }

                    if (inventory == null)
                        continue;

                    inventoryID = player.agentInventory.InventoryID;
                    playerInventory = contexts.inventory.GetEntityWithInventoryID(inventoryID);

                    inventoryID = player.agentInventory.EquipmentInventoryID;
                    equipmentInventory = contexts.inventory.GetEntityWithInventoryID(inventoryID);

                    if (!inventory.hasInventoryDraw)
                    {
                        GameState.InventoryManager.OpenInventory(inventory);
                        GameState.InventoryManager.OpenInventory(playerInventory);
                        GameState.InventoryManager.OpenInventory(equipmentInventory);
                    }
                    else 
                    {
                        GameState.InventoryManager.CloseInventory(planet.InventoryList, inventory);
                        GameState.InventoryManager.CloseInventory(planet.InventoryList, playerInventory);
                        GameState.InventoryManager.CloseInventory(planet.InventoryList, equipmentInventory);
                    }
                }
            }

            // Recharge Weapon.
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Q))
            {
                var players = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer));
                foreach (var player in players) 
                    GameState.ActionCreationSystem.CreateAction(NodeType.ChargeAction, player.agentID.ID);
            }

            // Drop Action. 
            if (UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.T))
            {
                var players = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer));
                foreach (var player in players)
                    GameState.ActionCreationSystem.CreateAction(NodeType.DropAction, player.agentID.ID);
            }

            // Reload Weapon.
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.R))
            {
                var players = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer));
                foreach (var player in players)
                    GameState.ActionCreationSystem.CreateAction(NodeType.ReloadAction, player.agentID.ID);
            }

            // Shield Action.
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Mouse1))
            {
                var players = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer));
                foreach (var player in players)
                    GameState.ActionCreationSystem.CreateAction(NodeType.ShieldAction, player.agentID.ID);

            }

            // Show/Hide Statistics
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.F1))
            {
                if (StatisticsDisplay.TextWrapper.GetGameObject().GetComponent<Text>().enabled)
                    StatisticsDisplay.TextWrapper.GetGameObject().GetComponent<Text>().enabled = false;
                else if (!StatisticsDisplay.TextWrapper.GetGameObject().GetComponent<Text>().enabled)
                    StatisticsDisplay.TextWrapper.GetGameObject().GetComponent<Text>().enabled = true;

            }

            // Remove Tile Front At Cursor Position.
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.F2))
            {
                UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                planet.TileMap.RemoveFrontTile((int)worldPosition.x, (int)worldPosition.y);
            }

            // Remove Tile Back At Cursor Position.
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.F3))
            {
                UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                planet.TileMap.RemoveBackTile((int)worldPosition.x, (int)worldPosition.y);
            }

            // Enable tile collision isotype rendering.
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.F4))
            {
                TileMapRenderer.TileCollisionDebugging = !TileMapRenderer.TileCollisionDebugging;
            }

            //  Open Inventory with Tab.        
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Tab))
            {
                var players = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer, AgentMatcher.AgentInventory));
                foreach (var player in players)
                {
                    int inventoryID = player.agentInventory.InventoryID;
                    InventoryEntity inventory = contexts.inventory.GetEntityWithInventoryID(inventoryID);

                    inventoryID = player.agentInventory.EquipmentInventoryID;
                    InventoryEntity equipmentInventory = contexts.inventory.GetEntityWithInventoryID(inventoryID);

                    if (!inventory.hasInventoryDraw)
                    {
                        GameState.InventoryManager.OpenInventory(inventory);
                        GameState.InventoryManager.OpenInventory(equipmentInventory);
                    }
                    else
                    {
                        GameState.InventoryManager.CloseInventory(planet.InventoryList, inventory);
                        GameState.InventoryManager.CloseInventory(planet.InventoryList, equipmentInventory);
                        if (!inventory.hasInventoryDraw)    // If inventory was open close all open inventories.
                        {
                            for (int i = 0; i < planet.InventoryList.Length; i++)
                            {
                                if (planet.InventoryList.Get(i).hasInventoryDraw)
                                    GameState.InventoryManager.CloseInventory(planet.InventoryList, planet.InventoryList.Get(i));
                            }
                        }
                    }
                }
            }

            // Change Pulse Weapon Mode.
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.N))
            {
                var PlayerWithToolBarPulse = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer));
                foreach (var entity in PlayerWithToolBarPulse)
                {
                    int inventoryID = entity.agentInventory.InventoryID;
                    InventoryEntity inventory = contexts.inventory.GetEntityWithInventoryID(inventoryID);
                    ref InventoryTemplateData inventoryModel = ref GameState.InventoryCreationApi.Get(inventory.inventoryInventory.InventoryModelID);
                    if (!inventoryModel.HasToolBar)
                        return;

                    var item = GameState.InventoryManager.GetItemInSlot(inventoryID, inventory.inventoryInventory.SelectedSlotID);

                    if (item.itemType.Type == ItemType.PulseWeapon)
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
            var playerWithToolBar = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer));
            foreach (var entity in playerWithToolBar)
            {
                int inventoryID = entity.agentInventory.InventoryID;
                InventoryEntity inventory = contexts.inventory.GetEntityWithInventoryID(inventoryID);
                ref InventoryTemplateData inventoryModel = ref GameState.InventoryCreationApi.Get(
                    inventory.inventoryInventory.InventoryModelID);
                if (!inventoryModel.HasToolBar)
                    return;

                // Get Inventory
                var item = GameState.InventoryManager.GetItemInSlot(inventoryID, inventory.inventoryInventory.SelectedSlotID);
                if (item == null) return;
                var itemProperty = GameState.ItemCreationApi.Get(item.itemType.Type);

                // If, Item is a weapon or gun.
                if(itemProperty.Group is ItemGroups.Gun or ItemGroups.Weapon)
                {
                    if(entity.hasAgentAction)
                    {
                        entity.agentAction.Action = AgentAlertState.Alert;
                    }
                }
                else
                {
                    if (entity.hasAgentAction)
                    {
                        entity.agentAction.Action = AgentAlertState.UnAlert;
                    }
                }
                

                for (int i = 0; i < inventoryModel.Width; i++)
                {
                    var keyCode = UnityEngine.KeyCode.Alpha1 + i;
                    if (UnityEngine.Input.GetKeyDown(keyCode))
                    {
                        if (inventory.inventoryInventory.SelectedSlotID != i)
                        {
                            entity.HandleItemDeselected(item);
                        }
                        inventory.inventoryInventory.SelectedSlotID = i;
                        item = GameState.InventoryManager.GetItemInSlot(inventoryID, i);
                        GameState.GUIManager.SelectedInventoryItem = item;
                        if (item == null) return;

                        entity.HandleItemSelected(item);
                        
                        planet.AddFloatingText(item.itemType.Type.ToString(), 2.0f, Vec2f.Zero, new Vec2f(entity.agentPhysicsState.Position.X + 0.4f,
                                    entity.agentPhysicsState.Position.Y));
                    }
                }


                int selectedSlot = inventory.inventoryInventory.SelectedSlotID;
                var selectedItem = GameState.InventoryManager.GetItemInSlot(inventoryID, selectedSlot);
                var selectedItemProperty = GameState.ItemCreationApi.Get(selectedItem.itemType.Type);


                if (selectedItemProperty.IsTool())
                {
                    switch (selectedItemProperty.KeyUsage)
                    {
                        case ItemKeyUsage.KeyPressed:
                        {
                            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Mouse0) && entity.IsStateFree())
                            {
                                if (!InventorySystemsState.MouseDown)
                                {
                                    GameState.ActionCreationSystem.CreateAction(selectedItemProperty.ToolActionType, entity.agentID.ID, item.itemID.ID);
                                }
                            }

                            break;
                        }
                        case ItemKeyUsage.KeyDown:
                        {
                            if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.Mouse0) && entity.IsStateFree())
                            {
                                if (!InventorySystemsState.MouseDown)
                                {
                                    GameState.ActionCreationSystem.CreateAction(selectedItemProperty.ToolActionType, entity.agentID.ID, item.itemID.ID);
                                }
                            }

                            break;
                        }
                    }
                }

                // Remove Tile Back At Cursor Position.
                if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.BackQuote))
                {
                    if (mode == Mode.Agent)
                        mode = Mode.Camera;
                    else if (mode == Mode.Camera)
                        mode = Mode.Agent;

                    UpdateMode(entity);
                }
            }
        }
    }
}