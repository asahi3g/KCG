using Agent;
using Enums;
using Inventory;
using KGUI.Statistics;
using KMath;
using Mech;
using Planet;
using PlanetTileMap;
using UnityEngine;
using UnityEngine.UI;

namespace ECSInput
{
    public class InputProcessSystem
    {
        private Mode mode = Mode.Camera;

        private void UpdateMode(ref PlanetState planetState, AgentEntity agentEntity)
        {
            agentEntity.agentPhysicsState.Invulnerable = false;
            Camera.main.gameObject.GetComponent<CameraMove>().enabled = false;
            planetState.cameraFollow.canFollow = false;

            if (mode == Mode.Agent)
            {
                Camera.main.gameObject.GetComponent<CameraMove>().enabled = false;
                planetState.cameraFollow.canFollow = true;

            }
            else if (mode == Mode.Camera)
            {
                Camera.main.gameObject.GetComponent<CameraMove>().enabled = true;
                planetState.cameraFollow.canFollow = false;

            }
        }

       

        public void Update(ref PlanetState planet)
        {
            Contexts contexts = planet.EntitasContext;

            var AgentsWithXY = contexts.agent.GetGroup(AgentMatcher.AllOf(
                AgentMatcher.ECSInput, AgentMatcher.ECSInputXY));

            int x = 0;
            if (Input.GetKey(KeyCode.D) && mode == Mode.Agent)
            {
                x += 1;
            }
            if (Input.GetKey(KeyCode.A) && mode == Mode.Agent)
            {
                x -= 1;
            }

            foreach (var player in AgentsWithXY)
            {

                var physicsState = player.agentPhysicsState;

                

                // Jump
                if (Input.GetKeyDown(KeyCode.W) && mode == Mode.Agent)
                {
                    player.Jump();
                }
                // Dash
                if (Input.GetKeyDown(KeyCode.Space) && mode == Mode.Agent)
                {
                    player.Dash(x);
                }

                // Attack
                if (Input.GetKeyDown(KeyCode.K) && mode == Mode.Agent)
                {
                    player.Roll(x);
                }


                // Running
                if (Input.GetKey(KeyCode.LeftAlt))
                {
                    if(mode == Mode.Agent)
                    player.Run(x);
                }
                else
                {
                    if(mode == Mode.Agent)
                    player.Walk(x);
                }

                if (Input.GetKey(KeyCode.LeftControl) && mode == Mode.Agent)
                {
                    if(mode == Mode.Agent)
                    player.Crouch(x);
                }
                else
                {
                    if(mode == Mode.Agent)
                    player.UnCrouch(x);
                }

                // JetPack
                if (Input.GetKey(KeyCode.F) && player.agentStats.Fuel > 0)
                {
                    GameState.AgentProcessPhysicalState.JetPackFlying(player);
                }
                else
                {
                    if (player.agentPhysicsState.MovementState == AgentMovementState.JetPackFlying)
                    {
                        player.agentPhysicsState.MovementState = AgentMovementState.None;
                    }
                }

                if (Input.GetKeyDown(KeyCode.S))
                {
                    if(mode == Mode.Agent)
                    player.Walk(x);
                }


                var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

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
                if (Input.GetKey(KeyCode.F))
                {
                    GameState.AgentProcessPhysicalState.JetPackFlying(player);
                }

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    player.agentPhysicsState.Droping = true;
                }
            }


            if (Input.GetKeyDown(KeyCode.E))
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
                            if (mech.mechType.mechType == Mech.MechType.CraftingTable)
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
                                    GameState.InventoryManager.OpenInventory(planet.InventoryList, playerInventory);
                                    GameState.InventoryManager.OpenInventory(planet.InventoryList, equipmentInventory);

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
                                    GameState.VehicleAISystem.Initialize(vehicle, new Vec2f(1.1f, -2.8f), new Vec2f(0f, 3.0f));

                                    // Player Gets inside of Rocket
                                    // Hide Agent/Player
                                    player.agentModel3D.GameObject.gameObject.SetActive(false);
                                    player.isAgentAlive = false;
                                    vehicle.vehicleType.HasAgent = true;

                                    GameState.VehicleAISystem.RunAI(vehicle, new Vec2f(1.1f, -2.8f), new Vec2f(0f, 3.0f));

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


                    InventoryEntity Inventory = null;
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

                            Inventory = contexts.inventory.GetEntityWithInventoryID(corpse.agentInventory.InventoryID);
                        }
                    }


                    var mechs = contexts.mech.GetEntities();
                    foreach (var mech in mechs)
                    {
                        float distance = Vec2f.Distance(mech.mechPosition2D.Value, player.agentPhysicsState.Position);
                        if (!(distance < smallestDistance))
                            continue;

                        distance = smallestDistance;
                        Inventory = null;

                        if (mech.hasMechInventory)
                            Inventory = contexts.inventory.GetEntityWithInventoryID(mech.mechInventory.InventoryID);

                        // Get proprietis.
                        ref MechProperties mechProperties = ref GameState.MechCreationApi.GetRef((int)mech.mechType.mechType);
                        if (mechProperties.Action != NodeType.None)
                            GameState.ActionCreationSystem.CreateAction(planet.EntitasContext, mechProperties.Action, player.agentID.ID);
                    }

                    if (Inventory == null)
                        continue;

                    inventoryID = player.agentInventory.InventoryID;
                    playerInventory = contexts.inventory.GetEntityWithInventoryID(inventoryID);

                    inventoryID = player.agentInventory.EquipmentInventoryID;
                    equipmentInventory = contexts.inventory.GetEntityWithInventoryID(inventoryID);

                    if (!Inventory.hasInventoryDraw)
                    {
                        GameState.InventoryManager.OpenInventory(planet.InventoryList, Inventory);
                        GameState.InventoryManager.OpenInventory(planet.InventoryList, playerInventory);
                        GameState.InventoryManager.OpenInventory(planet.InventoryList, equipmentInventory);
                    }
                    else 
                    {
                        GameState.InventoryManager.CloseInventory(planet.InventoryList, Inventory);
                        GameState.InventoryManager.CloseInventory(planet.InventoryList, playerInventory);
                        GameState.InventoryManager.CloseInventory(planet.InventoryList, equipmentInventory);
                    }
                }
            }

            // Recharge Weapon.
            if (Input.GetKeyDown(KeyCode.Q))
            {
                var players = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer));
                foreach (var player in players) 
                    GameState.ActionCreationSystem.CreateAction(planet.EntitasContext,NodeType.ChargeAction, player.agentID.ID);
            }

            // Drop Action. 
            if (Input.GetKeyUp(KeyCode.T))
            {
                var players = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer));
                foreach (var player in players)
                    GameState.ActionCreationSystem.CreateAction(planet.EntitasContext, NodeType.DropAction, player.agentID.ID);
            }

            // Reload Weapon.
            if (Input.GetKeyDown(KeyCode.R))
            {
                var players = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer));
                foreach (var player in players)
                    GameState.ActionCreationSystem.CreateAction(planet.EntitasContext, NodeType.ReloadAction, player.agentID.ID);
            }

            // Shield Action.
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                var players = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer));
                foreach (var player in players)
                    GameState.ActionCreationSystem.CreateAction(planet.EntitasContext, NodeType.ShieldAction, player.agentID.ID);

            }

            // Show/Hide Statistics
            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (StatisticsDisplay.text.GetGameObject().GetComponent<Text>().enabled)
                    StatisticsDisplay.text.GetGameObject().GetComponent<Text>().enabled = false;
                else if (!StatisticsDisplay.text.GetGameObject().GetComponent<Text>().enabled)
                    StatisticsDisplay.text.GetGameObject().GetComponent<Text>().enabled = true;

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

            // Enable tile collision isotype rendering.
            if (Input.GetKeyDown(KeyCode.F4))
            {
                TileMapRenderer.TileCollisionDebugging = !TileMapRenderer.TileCollisionDebugging;
            }

            //  Open Inventory with Tab.        
            if (Input.GetKeyDown(KeyCode.Tab))
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
                        GameState.InventoryManager.OpenInventory(planet.InventoryList, inventory);
                        GameState.InventoryManager.OpenInventory(planet.InventoryList, equipmentInventory);
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
            if (Input.GetKeyDown(KeyCode.N))
            {
                var PlayerWithToolBarPulse = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer));
                foreach (var entity in PlayerWithToolBarPulse)
                {
                    int inventoryID = entity.agentInventory.InventoryID;
                    InventoryEntity inventory = contexts.inventory.GetEntityWithInventoryID(inventoryID);
                    ref InventoryModel inventoryModel = ref GameState.InventoryCreationApi.Get(inventory.inventoryEntity.InventoryModelID);
                    if (!inventoryModel.HasToolBar)
                        return;

                    var item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, inventory.inventoryEntity.SelectedSlotID);

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
                ref InventoryModel inventoryModel = ref GameState.InventoryCreationApi.Get(
                    inventory.inventoryEntity.InventoryModelID);
                if (!inventoryModel.HasToolBar)
                    return;

                // Get Inventory
                var item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, inventory.inventoryEntity.SelectedSlotID);
                if (item == null) return;
                var itemProperty = GameState.ItemCreationApi.Get(item.itemType.Type);

                // If, Item is a weapon or gun.
                if(itemProperty.Group is ItemGroups.Gun or ItemGroups.Weapon)
                {
                    if(entity.hasAgentAction)
                    {
                        entity.agentAction.Action = AgentAction.Alert;
                    }
                }
                else
                {
                    if (entity.hasAgentAction)
                    {
                        entity.agentAction.Action = AgentAction.UnAlert;
                    }
                }
                

                for (int i = 0; i < inventoryModel.Width; i++)
                {
                    var keyCode = KeyCode.Alpha1 + i;
                    if (Input.GetKeyDown(keyCode))
                    {
                        if (inventory.inventoryEntity.SelectedSlotID != i)
                        {
                            entity.HandleItemDeselected(item);
                        }
                        inventory.inventoryEntity.SelectedSlotID = i;
                        item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, i);
                        if (item == null) return;

                        entity.HandleItemSelected(item);
                        
                        planet.AddFloatingText(item.itemType.Type.ToString(), 2.0f, Vec2f.Zero, new Vec2f(entity.agentPhysicsState.Position.X + 0.4f,
                                    entity.agentPhysicsState.Position.Y));
                    }
                }


                int selectedSlot = inventory.inventoryEntity.SelectedSlotID;
                var selectedItem = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
                var selectedItemProperty = GameState.ItemCreationApi.Get(selectedItem.itemType.Type);


                if (selectedItemProperty.IsTool())
                {
                    switch (selectedItemProperty.KeyUsage)
                    {
                        case ItemKeyUsage.KeyPressed:
                        {
                            if (Input.GetKeyDown(KeyCode.Mouse0) && entity.IsStateFree())
                            {
                                if (!InventorySystemsState.MouseDown)
                                {
                                    GameState.ActionCreationSystem.CreateAction(planet.EntitasContext, selectedItemProperty.ToolActionType, 
                                        entity.agentID.ID, item.itemID.ID);
                                }
                            }

                            break;
                        }
                        case ItemKeyUsage.KeyDown:
                        {
                            if (Input.GetKey(KeyCode.Mouse0) && entity.IsStateFree())
                            {
                                if (!InventorySystemsState.MouseDown)
                                {
                                    GameState.ActionCreationSystem.CreateAction(planet.EntitasContext, selectedItemProperty.ToolActionType, 
                                        entity.agentID.ID, item.itemID.ID);
                                }
                            }

                            break;
                        }
                    }
                }

                // Remove Tile Back At Cursor Position.
                if (Input.GetKeyDown(KeyCode.BackQuote))
                {
                    if (mode == Mode.Agent)
                        mode = Mode.Camera;
                    else if (mode == Mode.Camera)
                        mode = Mode.Agent;

                    UpdateMode(ref planet, entity);

                }
            }
        }
    }
}