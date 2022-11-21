using Enums;
using KMath;
using Enums.PlanetTileMap;

namespace Item
{
    public class SpawnerSystem
    {
        private static int ItemID;

        public ItemParticleEntity SpawnItemParticle(ItemType itemType, Vec2f position)
        {
            ItemProprieties itemProperty = GameState.ItemCreationApi.Get(itemType);
            FireWeaponPropreties weaponProperty = GameState.ItemCreationApi.GetWeapon(itemType);

            Vec2f size = GameState.ItemCreationApi.Get(itemType).SpriteSize;

            var entity = GameState.Planet.EntitasContext.itemParticle.CreateEntity();
            entity.AddItemID(ItemID, -1);
            entity.AddItemType(itemType);
            entity.AddItemPhysicsState(position, position, Vec2f.Zero, Vec2f.Zero, false);
            entity.AddPhysicsBox2DCollider(size, Vec2f.Zero);

            if (weaponProperty.HasClip())
                entity.AddItemFireWeaponClip(weaponProperty.ClipSize);

            if (weaponProperty.HasCharge())
                entity.AddItemFireWeaponCharge(weaponProperty.CanCharge, weaponProperty.ChargeRate, weaponProperty.ChargeRatio,
                    weaponProperty.ChargePerShot, weaponProperty.ChargeMin, weaponProperty.ChargeMax);

            ItemID++;
            return entity;
        }

        // Spawn Item particle from item inventory. Used in dropItem action or after enemy dies.
        // Destroy itemParticle.
        public ItemParticleEntity SpawnItemParticle(ItemInventoryEntity itemInventoryEntity, Vec2f pos)
        {
            var entity = SpawnItemParticle(itemInventoryEntity.itemType.Type, pos);

            if(itemInventoryEntity.hasItemLabel)
                entity.AddItemLabel(itemInventoryEntity.itemLabel.ItemName);

            if(itemInventoryEntity.hasItemStack)
                entity.AddItemStack(itemInventoryEntity.itemStack.Count);

            if (itemInventoryEntity.hasItemFireWeaponClip)
            {
                entity.itemFireWeaponClip.NumOfBullets = itemInventoryEntity.itemFireWeaponClip.NumOfBullets;
            }

            itemInventoryEntity.Destroy();

            return entity;
        }

        public ItemInventoryEntity SpawnInventoryItem(ItemType itemType)
        {
            ItemProprieties itemProperty = GameState.ItemCreationApi.Get(itemType);
            FireWeaponPropreties weaponProperty = GameState.ItemCreationApi.GetWeapon(itemType);

            var entity = GameState.Planet.EntitasContext.itemInventory.CreateEntity();
            entity.AddItemID(ItemID, -1);
            entity.AddItemType(itemType);

            if (itemProperty.IsPlacementTool())
            {
                if (entity.itemType.Type == ItemType.PlacementTool || entity.itemType.Type == ItemType.PlacementMaterialTool ||
                        entity.itemType.Type == ItemType.GeometryPlacementTool)
                {
                    entity.AddItemTile(TileID.Error, MapLayerType.Front, true);
                }

                if (entity.itemType.Type == ItemType.ConstructionTool ||
                    entity.itemType.Type == ItemType.RemoveMech ||
                    itemProperty.Group == ItemGroups.Mech)
                {
                    entity.AddItemMech(itemProperty.MechType, true);
                }
            }
            
            if (entity.itemType.Type == ItemType.PotionTool || entity.itemType.Type == ItemType.HealthPotion)
            {
                PotionType potionType = PotionType.Error;
                entity.AddItemPotion(potionType);
            }

            if (weaponProperty.HasClip())
                entity.AddItemFireWeaponClip(weaponProperty.ClipSize);

            ItemID++;
            return entity;
        }

        public ItemInventoryEntity SpawnInventoryItem(ItemParticleEntity itemParticleEntity)
        {
            var entity = SpawnInventoryItem(itemParticleEntity.itemType.Type);

            if (itemParticleEntity.hasItemLabel)
                entity.AddItemLabel(itemParticleEntity.itemLabel.ItemName);

            if (itemParticleEntity.hasItemStack)
                entity.AddItemStack(itemParticleEntity.itemStack.Count);

            GameState.Planet.RemoveItemParticle(itemParticleEntity.itemID.Index);

            return entity;
        }
    }
}

