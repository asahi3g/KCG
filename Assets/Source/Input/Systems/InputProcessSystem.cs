using KMath;
using UnityEngine;
using Agent;
using Enums;

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
                    gun.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

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
                    GameState.AgentProcessPhysicalState.JumpVelocity(player, 16f); // 16f Intial velocity necessary to jump 3.2 tiles. at 40 tiles/seconds gravity
                }
                // Dash
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameState.AgentProcessPhysicalState.Dash(player, x);
                }

                // Attack
                if (Input.GetKeyDown(KeyCode.K))
                {
                    GameState.AgentProcessPhysicalState.SwordSlash(player);
                }

                if (Input.GetKeyDown(KeyCode.L))
                {
                    GameState.AgentProcessPhysicalState.FireGun(player);
                }

                if (Input.GetKeyDown(KeyCode.C))
                {
                    SetAgentWeapon(player, Model3DWeapon.Sword);
                }

                if (Input.GetKeyDown(KeyCode.B))
                {
                    SetAgentWeapon(player, Model3DWeapon.Gun);
                }

                if (Input.GetKeyDown(KeyCode.V))
                {
                    SetAgentWeapon(player, Model3DWeapon.None);
                }

                // Running
                if (Input.GetKey(KeyCode.LeftAlt))
                {
                    GameState.AgentProcessPhysicalState.Run(player, x);
                }
                else
                    GameState.AgentProcessPhysicalState.Walk(player, x);

                // JetPack
                if (Input.GetKey(KeyCode.F))
                    GameState.AgentProcessPhysicalState.JetPackFlying(player);

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    GameState.AgentProcessPhysicalState.Walk(player, x);
                }

                // JetPack
                if (Input.GetKey(KeyCode.F))
                {
                    GameState.AgentProcessPhysicalState.JetPackFlying(player);
                }

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    player.agentPhysicsState.Droping = true;
                    player.agentPhysicsState.WantToDrop = true;
                }


                /*
<<<<<<< HEAD

                

                // we can start jumping only if the jump counter is 0
                if (movementState.JumpCounter == 0)
                {
    
                    // first jump
                    if (jump && movementState.MovementState != MovementState.Dashing)
                    {
                        // if we are sticking to a wall 
                        // throw the agent in the opposite direction
                        if (movable.SlidingLeft)
                        {
                            movable.SlidingLeft = false;
                            movable.Acceleration.X = 1.0f * movable.Speed * 400.0f * 2;
                            movable.Acceleration.Y = -1.0f * movable.Speed * 400.0f * 2;
                        }
                        else if (movable.SlidingRight)
                        {
                            movable.SlidingRight = false;
                            movable.Acceleration.X = -1.0f * movable.Speed * 400.0f * 2;
                            movable.Acceleration.Y = -1.0f * movable.Speed * 400.0f * 2;
                        }


                        // jumping
                        movable.OnGrounded = false;
                        movable.Acceleration.Y = 100.0f;
                        movable.Velocity.Y = 11.5f;
                        movable.AffectedByGroundFriction = false;
                        movementState.JumpCounter++;
                    }

                }
                else
                {
                    // double jump
                    if (jump && movementState.JumpCounter <= 1)
                    {
                        movable.Acceleration.Y = 100.0f;
                        movable.Velocity.Y = 8.5f;
                        movementState.JumpCounter++;
                    }
                }

                // if the fly button is pressed
                if (flying && stats.Fuel > 0.0f)
                {
                    movementState.MovementState = MovementState.Flying;
                }
                else if (movementState.MovementState == MovementState.Flying)
                {
                    // if no fuel is left we change to movement state to none
                    movementState.MovementState = MovementState.None;
                }

                // if we are using the jetpack
                // set the Y velocity to a given value
                if (movementState.MovementState == MovementState.Flying)
                {
                    movable.Acceleration.Y = 0;
                    movable.Velocity.Y = 3.5f;
                }


                // the end of dashing
                // we can do this using a fixed amount of time
                if (System.Math.Abs(movable.Velocity.X) <= 6.0f && 
                movementState.MovementState == MovementState.Dashing)
                {
                    movementState.MovementState = MovementState.None;

                    // if the agent is dashing it becomes invulnerable to damage
                    movable.Invulnerable = movementState.MovementState == MovementState.Dashing;
                    
                    movementState.MovementState = MovementState.None;   
                    movable.Invulnerable = false; 
                }

                // if the agent is dashing the gravity will not affect him
                movable.AffectedByGravity = !(movementState.MovementState == MovementState.Dashing);


                if (x == 1.0f)
                {
                    // if we move to the right
                    // that means we are no longer sliding down on the left
                    movable.SlidingLeft = false;
                }
                else if (x == -1.0f)
                {
                    // if we move to the left
                    // that means we are no longer sliding down on the right
                    movable.SlidingRight = false;
                }


                // if we are on the ground we reset the jump counter
                if (movable.OnGrounded)
                {
                    movementState.JumpCounter = 0;
                    movable.AffectedByGroundFriction = true;

                    movable.SlidingRight = false;
                    movable.SlidingLeft = false;
                    if (movementState.MovementState == MovementState.None)
                    {
                        movementState.MovementState = MovementState.Idle;
                    }
                }

                
                // if we are sliding
                // spawn some particles and limit vertical movement
                if (movable.SlidingLeft)
                {
                    movementState.JumpCounter = 0;
                    movable.Acceleration.Y = 0.0f;
                    movable.Velocity.Y = -1.75f;
                    planet.AddParticleEmitter(pos.Value + new Vec2f(0.0f, -0.5f), Particle.ParticleEmitterType.DustEmitter);
                }
                else if (movable.SlidingRight)
                {
                    movementState.JumpCounter = 0;
                    movable.Acceleration.Y = 0.0f;
                    movable.Velocity.Y = -1.75f;
                    planet.AddParticleEmitter(pos.Value + new Vec2f(0.5f, -0.5f), Particle.ParticleEmitterType.DustEmitter);
                }

                // if we are flying, reduce the fuel and spawn particles
                if (movementState.MovementState == MovementState.Flying)
                {
                    stats.Fuel -= 1.0f;
                    if (stats.Fuel <= 1.0f)
                    {
                        stats.Fuel -= 20.0f;
                    }
                    planet.AddParticleEmitter(pos.Value, Particle.ParticleEmitterType.DustEmitter);
                }
                else
                {
                    // if we are not flying, add fuel to the tank
                    stats.Fuel += 1.0f;
                }

                // make sure the fuel never goes up more than it should
                if (stats.Fuel > 100) 
                {
                    stats.Fuel = 100;
                }

                // if we are dashing we add some particles
                if (movementState.MovementState == MovementState.Dashing)
                {
                    planet.AddParticleEmitter(pos.Value, Particle.ParticleEmitterType.DustEmitter);
                }


                float epsilon = 2.0f;

                if (movable.Velocity.Y <= -epsilon)
                {
                    movementState.MovementState = MovementState.Falling;
                }
                else if (movementState.MovementState != MovementState.Dashing)
                {
                    movementState.MovementState = MovementState.None;
                }


                if (movementState.MovementState == MovementState.Idle || 
                movementState.MovementState == MovementState.None)
                {
                    if (movable.Velocity.X >= epsilon ||
                    movable.Velocity.X <= -epsilon)
                    {
                        movementState.MovementState = MovementState.Move;
                    }
                    else
                    {
                        movementState.MovementState = MovementState.Idle;
                    }
                }



                //if (movable.Droping && movable.OnGrounded)
                //{
                   
                //        movable.OnGrounded = false;
                //        movable.Acceleration.Y = 100.0f;
                    

                //}
=======
>>>>>>> 14d9b3d614e10ed08f93113bb153a5b4c2489826*/
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                var players = contexts.agent.GetGroup(AgentMatcher.AgentPlayer);

                foreach (var player in players)
                {
                    InventoryEntity Inventory = null;
                    float smallestDistance = 0.5f;
                    var corpses = contexts.agent.GetGroup(AgentMatcher.AgentCorpse);
                    foreach (var corpse in corpses)
                    {
                        var physicsState = corpse.agentPhysicsState;
                        float distance = Vec2f.Distance(physicsState.Position, player.agentPhysicsState.Position);

                        if (!corpse.hasAgentInventory || !(distance < smallestDistance))
                            continue;

                        smallestDistance = distance;

                        Inventory = contexts.inventory.GetEntityWithInventoryID(corpse.agentInventory.InventoryID);
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
                    
                    Inventory.hasInventoryDraw = !Inventory.hasInventoryDraw;

                    int inventoryID = player.agentInventory.InventoryID;
                    InventoryEntity playerInventory = contexts.inventory.GetEntityWithInventoryID(inventoryID);
                    playerInventory.hasInventoryDraw = !playerInventory.hasInventoryDraw;

                    inventoryID = player.agentInventory.EquipmentInventoryID;
                    InventoryEntity equipmentInventory = contexts.inventory.GetEntityWithInventoryID(inventoryID);
                    equipmentInventory.hasInventoryDraw = !equipmentInventory.hasInventoryDraw;
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
                    inventory.hasInventoryDraw = !inventory.hasInventoryDraw;

                    inventoryID = player.agentInventory.EquipmentInventoryID;
                    InventoryEntity equipmentInventory = contexts.inventory.GetEntityWithInventoryID(inventoryID);
                    equipmentInventory.hasInventoryDraw = !equipmentInventory.hasInventoryDraw;

                    if (!inventory.hasInventoryDraw)    // If inventory was open close all open inventories.
                    {
                        var inventories = contexts.inventory.GetGroup(InventoryMatcher.InventoryDraw);
                        foreach (var openInventory in inventories)
                        {
                            openInventory.hasInventoryDraw = false;
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

                for (int i = 0; i < inventoryModel.Width; i++)
                {
                    KeyCode keyCode = KeyCode.Alpha1 + i;
                    if (Input.GetKeyDown(keyCode))
                    {
                        inventory.inventoryEntity.SelectedSlotID = i;
                        ItemInventoryEntity item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, i);
                        Item.ItemProprieties itemProperty = GameState.ItemCreationApi.Get(item.itemType.Type);

                        switch(itemProperty.Group)
                        {
                            case Enums.ItemGroups.Gun:
                            {
                                SetAgentWeapon(entity, Model3DWeapon.Gun);
                                break;
                            }
                            case Enums.ItemGroups.Weapon:
                            {
                                SetAgentWeapon(entity, Model3DWeapon.Sword);
                                break;
                            }
                            default:
                            {
                                SetAgentWeapon(entity, Model3DWeapon.None);
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