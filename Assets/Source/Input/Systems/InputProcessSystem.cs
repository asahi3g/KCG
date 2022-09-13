using KMath;
using UnityEngine;
using Agent;
using Enums;
using UnityEngine.Animations.Rigging;

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

        public void SetAgentWeapon(AgentEntity agentEntity, Model3DWeapon weapon)
        {
            if (!agentEntity.hasAgentModel3D)
                return;

            var model3d = agentEntity.agentModel3D;
            if (weapon == Model3DWeapon.Sword)
            {        
                if (model3d.CurrentWeapon != Model3DWeapon.Sword)
                {
                    if (model3d.Weapon != null)
                    {
                        GameObject.Destroy(model3d.Weapon);
                    }

                    GameObject hand = model3d.LeftHand;

                    GameObject rapierPrefab = Engine3D.AssetManager.Singelton.GetModel(Engine3D.ModelType.Rapier);
                    GameObject rapier = GameObject.Instantiate(rapierPrefab);

                    rapier.transform.parent = hand.transform;
                    rapier.transform.position = hand.transform.position;
                    rapier.transform.rotation = hand.transform.rotation;
                    rapier.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                    model3d.Weapon = rapier;
                }
            }
            else if (weapon == Model3DWeapon.Gun)
            {        
                if (model3d.CurrentWeapon != Model3DWeapon.Gun)
                {
                    if (model3d.Weapon != null)
                    {
                        GameObject.Destroy(model3d.Weapon);
                    }

                    GameObject hand = model3d.RightHand;

                    GameObject prefab = Engine3D.AssetManager.Singelton.GetModel(Engine3D.ModelType.Pistol);
                    GameObject gun = GameObject.Instantiate(prefab);

                    gun.transform.parent = hand.transform;
                    gun.transform.position = hand.transform.position;
                    gun.transform.rotation = hand.transform.rotation;
                    gun.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

                    model3d.Weapon = gun;
                }
            }
            else
            {
                if (model3d.Weapon != null)
                {
                    GameObject.Destroy(model3d.Weapon);
                }
            }

            model3d.CurrentWeapon = weapon;
        }

        public void Update(ref Planet.PlanetState planet)
        {
            Contexts contexts = planet.EntitasContext;

            var AgentsWithXY = contexts.agent.GetGroup(AgentMatcher.AllOf(
                AgentMatcher.ECSInput, AgentMatcher.ECSInputXY));

            int x = 0;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                x += 1;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                x -= 1;
            }

            foreach (var player in AgentsWithXY)
            {

                var physicsState = player.agentPhysicsState;

                // Jump
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    GameState.AgentProcessPhysicalState.Jump(player);
                }
                // Dash
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    player.Dash(x);
                }

                // Attack
                if (Input.GetKeyDown(KeyCode.K))
                {
                    player.Roll(x);
                }


                // Running
                if (Input.GetKey(KeyCode.LeftAlt))
                {
                    player.Run(x);
                }
                else
                {
                    player.Walk(x);
                }

                if (Input.GetKey(KeyCode.LeftControl))
                {
                    player.Crouch(x);
                }
                else
                {
                    player.UnCrouch(x);
                }

                // JetPack
                if (Input.GetKey(KeyCode.F))
                    GameState.AgentProcessPhysicalState.JetPackFlying(player);

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    player.Walk(x);
                }

                if (Input.GetKeyDown(KeyCode.L))
                {
                    var vehicles = contexts.vehicle.GetGroup(VehicleMatcher.VehicleID);

                    foreach (var vehicle in vehicles)
                    {
                        if(Vec2f.Distance(player.agentPhysicsState.Position, vehicle.vehiclePhysicsState2D.Position) < 3.0f || vehicle.vehicleType.HasAgent)
                        {
                            if(player.agentModel3D.GameObject.gameObject.active == true)
                            {
                                GameState.VehicleAISystem.Initialize(vehicle, new Vec2f(1.1f, -2.8f), new Vec2f(0f, 3.0f));

                                // Player Gets inside of Rocket
                                // Hide Agent/Player
                                player.agentModel3D.GameObject.gameObject.SetActive(false);
                                player.agentState.State = AgentState.Dead;
                                vehicle.vehicleType.HasAgent = true;

                                GameState.VehicleAISystem.RunAI(vehicle, new Vec2f(1.1f, -2.8f), new Vec2f(0f, 3.0f));
                            }
                            else
                            {
                                vehicle.vehiclePhysicsState2D.AffectedByGravity = true;
                                GameState.VehicleAISystem.StopAI();

                                vehicle.vehicleType.HasAgent = false;
                                player.agentPhysicsState.Position = vehicle.vehiclePhysicsState2D.Position;
                                player.agentModel3D.GameObject.gameObject.SetActive(true);
                                player.agentState.State = AgentState.Alive;

                            }
                            
                        }
                    }
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

                foreach (var player in players)
                {
                    InventoryEntity Inventory = null;
                    float smallestDistance = 2.0f;
                    var agents = planet.AgentList;
                    for (int i =0; i < agents.Length; i++)
                    {
                        AgentEntity corpse = agents.Get(i);
                        var state = corpse.agentState;
                        if (state.State == AgentState.Dead)
                        {
                            
                            var physicsState = corpse.agentPhysicsState;
                            float distance = Vec2f.Distance(physicsState.Position, player.agentPhysicsState.Position);

                            if (!corpse.hasAgentInventory || !(distance < smallestDistance))
                                continue;

                            smallestDistance = distance;

                            Inventory = contexts.inventory.GetEntityWithInventoryID(corpse.agentInventory.InventoryID);
                        }
                    }


                    MechEntity[] meches = contexts.mech.GetEntities();
                    foreach (var mech in meches)
                    {
                        float distance = Vec2f.Distance(mech.mechPosition2D.Value, player.agentPhysicsState.Position);
                        if (!(distance < smallestDistance))
                            continue;

                        distance = smallestDistance;
                        Inventory = null;

                        if (mech.hasMechInventory)
                            Inventory = contexts.inventory.GetEntityWithInventoryID(mech.mechInventory.InventoryID);

                        // Get proprietis.
                        ref Mech.MechProperties mechProperties = ref GameState.MechCreationApi.GetRef((int)mech.mechType.mechType);
                        if (mechProperties.Action != Enums.ActionType.None)
                            GameState.ActionCreationSystem.CreateAction(planet.EntitasContext, mechProperties.Action, player.agentID.ID);
                    }

                    if (Inventory == null)
                        continue;

                    int inventoryID = player.agentInventory.InventoryID;
                    InventoryEntity playerInventory = contexts.inventory.GetEntityWithInventoryID(inventoryID);

                    inventoryID = player.agentInventory.EquipmentInventoryID;
                    InventoryEntity equipmentInventory = contexts.inventory.GetEntityWithInventoryID(inventoryID);

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

            // Enable tile collision isotype rendering.
            if (Input.GetKeyDown(KeyCode.F4))
            {
                PlanetTileMap.TileMapRenderer.TileCollisionDebugging = !PlanetTileMap.TileMapRenderer.TileCollisionDebugging;
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
                    ref Inventory.InventoryModel inventoryModel = ref GameState.InventoryCreationApi.Get(inventory.inventoryEntity.InventoryModelID);
                    if (!inventoryModel.HasToolBar)
                        return;

                    var item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, inventory.inventoryEntity.SelectedSlotID);

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
                InventoryEntity inventory = contexts.inventory.GetEntityWithInventoryID(inventoryID);
                ref Inventory.InventoryModel inventoryModel = ref GameState.InventoryCreationApi.Get(
                    inventory.inventoryEntity.InventoryModelID);
                if (!inventoryModel.HasToolBar)
                    return;

                // Get Inventory
                ItemInventoryEntity item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, inventory.inventoryEntity.SelectedSlotID);
                if (item == null)
                    return;
                Item.ItemProprieties itemProperty = GameState.ItemCreationApi.Get(item.itemType.Type);

                // If, Item is a weapon or gun.
                if(itemProperty.Group == ItemGroups.Gun || itemProperty.Group == ItemGroups.Weapon)
                {
                    // Has Action Component?
                    if(entity.hasAgentAgentAction)
                    {
                        // Set Action Mode To Alert
                        entity.agentAgentAction.Action = AgentAction.Alert;
                    }
                }
                else
                {
                    // Has Action Component?
                    if (entity.hasAgentAgentAction)
                    {
                        // Set Action Mode To Un Alert
                        entity.agentAgentAction.Action = AgentAction.UnAlert;
                    }
                }
                

                for (int i = 0; i < inventoryModel.Width; i++)
                {
                    KeyCode keyCode = KeyCode.Alpha1 + i;
                    if (Input.GetKeyDown(keyCode))
                    {
                        inventory.inventoryEntity.SelectedSlotID = i;
                        item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, i);
                        if (item == null)
                            return;
                        itemProperty = GameState.ItemCreationApi.Get(item.itemType.Type);

                        switch(itemProperty.Group)
                        {
                            case Enums.ItemGroups.Gun:
                            {
                                entity.SetAgentWeapon(Model3DWeapon.Gun);
                                break;
                            }
                            case Enums.ItemGroups.Weapon:
                            {
                                entity.SetAgentWeapon(Model3DWeapon.Sword);
                                break;
                            }
                            default:
                            {
                                entity.SetAgentWeapon(Model3DWeapon.None);
                                break;
                            }
                        }
                        
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