using Entitas;
using Enums.Tile;
using Mech;
using UnityEngine;
using PlanetTileMap;

namespace Action
{ 
    public class InitializeSystem
    {
        public int CreatePickUpAction(Contexts entitasContext, int agentID, int itemID)
        {
            int actionID = GameState.ActionCreationSystem.CreateAction(entitasContext, Enums.ActionType.PickUpAction, 
                agentID, itemID);
            return actionID;
        }
        
        private static void CreateToolActionPlaceTile(Contexts entitasContext, TileID tileID, MapLayerType layer)
        {
            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.PlaceTilOre1Action + 
                (int)tileID - (int)TileID.Ore1);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionPlaceTileCreator());

            var data = new Enums.Tile.Data
            {
                TileID = tileID,
                Layer = layer
            };

            GameState.ActionCreationApi.SetData(data);
            GameState.ActionCreationApi.EndActionPropertyType();
        }

        public void Initialize(Contexts entitasContext, Material material)
        {
            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.DropAction);

            GameState.ActionCreationApi.SetLogicFactory(new DropActionCreator());
            GameState.ActionCreationApi.SetTime(2.0f);
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.PickUpAction);
            GameState.ActionCreationApi.SetLogicFactory(new PickUpActionCreator());
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.MoveAction);
            GameState.ActionCreationApi.SetLogicFactory(new MoveActionCreator());
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.OpenChestAction);
            GameState.ActionCreationApi.SetDescription("open chest.");
            GameState.ActionCreationApi.SetLogicFactory(new ChestActionCreator());
            GameState.ActionCreationApi.EndActionPropertyType();

            CreateToolActionPlaceTile(entitasContext, TileID.Ore1, MapLayerType.Front);
            CreateToolActionPlaceTile(entitasContext, TileID.Ore2, MapLayerType.Front);
            CreateToolActionPlaceTile(entitasContext, TileID.Ore3, MapLayerType.Front);
            CreateToolActionPlaceTile(entitasContext, TileID.Glass, MapLayerType.Front);
            CreateToolActionPlaceTile(entitasContext, TileID.Moon, MapLayerType.Front);
            CreateToolActionPlaceTile(entitasContext, TileID.Background, MapLayerType.Back);
            CreateToolActionPlaceTile(entitasContext, TileID.Pipe, MapLayerType.Mid);

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.ToolActionFireWeapon);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionFireWeaponCreator());
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.ReloadAction) ;
            GameState.ActionCreationApi.SetLogicFactory(new ReloadActionCreator());
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.ShieldAction);
            GameState.ActionCreationApi.SetLogicFactory(new ShieldActionCreator());
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.ToolActionPlaceParticle);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionPlaceParticleCreator());
            ToolActionPlaceParticleEmitter.Data placeParticleEmitterData = new ToolActionPlaceParticleEmitter.Data();
            placeParticleEmitterData.emitterType = Particle.ParticleEmitterType.ExplosionEmitter;
            GameState.ActionCreationApi.SetData(placeParticleEmitterData);
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.ToolActionSpawnExplosion);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionPlaceParticleCreator());
            placeParticleEmitterData = new ToolActionPlaceParticleEmitter.Data();
            placeParticleEmitterData.emitterType = Particle.ParticleEmitterType.ExplosionEmitter;
            GameState.ActionCreationApi.SetData(placeParticleEmitterData);
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.FragGrenade);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionFragGrenadeCreator());
            GameState.ActionCreationApi.EndActionPropertyType();


            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.ToolActionPlaceChest);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionPlaceChestCreator());
            ToolActionPlaceChest.Data placeChestData = new ToolActionPlaceChest.Data
            {
                Material = material
            };
            GameState.ActionCreationApi.SetData(placeChestData);
            GameState.ActionCreationApi.EndActionPropertyType();
            
            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.ToolActionPlaceSmahableBox);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionSmashableBoxCreator());
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.ToolActionEnemySpawn);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionEnemySpawnCreator());
            ToolActionEnemySpawn.Data data = new ToolActionEnemySpawn.Data();
            data.CharacterSpriteId = GameState.ItemCreationApi.SlimeSpriteSheet;
            GameState.ActionCreationApi.SetData(data);
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.ToolActionEnemyGunnerSpawn);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionEnemyGunnerSpawnCreator());
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.ToolActionEnemySwordmanSpawn);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionEnemySwordmanSpawnCreator());
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.ToolActionMiningLaser);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionMiningLaserCreator());
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.ToolActionRemoveTile);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionRemoveTileCreator());
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.ToolActionThrowGrenade);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionThrowableGrenadeCreator());
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.ToolActionMeleeAttack);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionMeleeAttackCreator());
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.ToolActionPulseWeapon);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionPulseWeaponCreator());
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.ToolActionShield);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionShieldCreator());
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.ToolActionPlanter);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionPlanterCreator());
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.ToolActionWater);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionWaterCreator());
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.ToolActionHarvest);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionHarvestCreator());
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.ToolActionConstruction);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionConstructionCreator());
            Mech.Data constructionToolData = new Mech.Data();
            constructionToolData.MechID = MechType.Storage;
            GameState.ActionCreationApi.SetData(constructionToolData);
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.ToolActionScanner);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionScannerCreator());
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.ToolActionRemoveMech);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionRemoveMechCreator());
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.ToolActionMechPlacement);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionMechPlacementCreator());
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.DrinkPotion);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionUsePotionCreator());
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.ToolActionMaterialPlacement);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionPlaceMaterialCreator());
            var MaterialData = new Enums.Tile.Data
            {
                TileID = TileID.Error,
                Layer = MapLayerType.Front
            };
            GameState.ActionCreationApi.SetData(MaterialData);
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.ToolActionGasBomb);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionGasBombCreator());
            GameState.ActionCreationApi.EndActionPropertyType();

            GameState.ActionCreationApi.CreateActionPropertyType(Enums.ActionType.ToolActionPotion);
            GameState.ActionCreationApi.SetLogicFactory(new ToolActionPotionCreator());
            GameState.ActionCreationApi.EndActionPropertyType();
            GameState.ActionPropertyManager.EndActionPropertyType();

            GameState.ActionPropertyManager.CreateActionPropertyType(Enums.ActionType.ToolActionGeometryPlacement);
            GameState.ActionPropertyManager.SetLogicFactory(new ToolActionGeometryPlacementCreator());
            GameState.ActionPropertyManager.EndActionPropertyType();
        }
    }
}
