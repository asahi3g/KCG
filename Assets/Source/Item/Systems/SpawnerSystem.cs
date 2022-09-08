using UnityEngine;
using System.Collections.Generic;

using Enums;
using KMath;
using UnityEngine.UIElements;
using Enums.Tile;

namespace Item
{
    public class SpawnerSystem
    {
        private static int ItemID;

        public ItemParticleEntity SpawnItemParticle(Contexts entitasContext, ItemType itemType, Vec2f position)
        {
            ItemProprieties itemProperty = GameState.ItemCreationApi.Get(itemType);
            FireWeaponPropreties weaponProperty = GameState.ItemCreationApi.GetWeapon(itemType);

            Vec2f size = itemProperty.SpriteSize;

            var entity = entitasContext.itemParticle.CreateEntity();
            entity.AddItemID(ItemID);
            entity.AddItemType(itemType);
            entity.AddItemPhysicsState(position, position, Vec2f.Zero, Vec2f.Zero, false);
            entity.AddPhysicsBox2DCollider(size, Vec2f.Zero);

            if (weaponProperty.HasClip())
                entity.AddItemFireWeaponClip(weaponProperty.ClipSize, weaponProperty.BulletsPerShot);

            if (weaponProperty.ShouldSpread())
                entity.AddItemFireWeaponSpread(weaponProperty.SpreadAngle);

            if (weaponProperty.IsPulse())
                entity.AddItemPulseWeaponPulse(weaponProperty.IsLaunchGreanade, weaponProperty.NumberOfGrenades);

            ItemID++;
            return entity;
        }

        // Todo(João): This create more work as we expand item funcionalities, Do code generation for this function.
        /// <summary>
        /// Spawn Item particle from item inventory. Used in dropItem action or after enemy dies.
        /// Destroy itemParticle.
        /// </summary>
        public ItemParticleEntity SpawnItemParticle(Contexts entitasContext, ItemInventoryEntity itemInventoryEntity, Vec2f pos)
        {
            var entity = SpawnItemParticle(entitasContext, itemInventoryEntity.itemType.Type, pos);

            if(itemInventoryEntity.hasItemLabel)
                entity.AddItemLabel(itemInventoryEntity.itemLabel.ItemName);

            if(itemInventoryEntity.hasItemStack)
                entity.AddItemStack(itemInventoryEntity.itemStack.Count);

            if(itemInventoryEntity.hasItemFireWeaponClip)
                entity.ReplaceItemFireWeaponClip(itemInventoryEntity.itemFireWeaponClip.NumOfBullets, itemInventoryEntity.itemFireWeaponClip.BulletsPerShot);

            if (itemInventoryEntity.hasItemFireWeaponSpread)
                entity.ReplaceItemFireWeaponSpread(itemInventoryEntity.itemFireWeaponSpread.SpreadAngle);

            itemInventoryEntity.Destroy();

            return entity;
        }

        public ItemInventoryEntity SpawnInventoryItem(Contexts entitasContext, ItemType itemType)
        {
            ItemProprieties itemProperty = GameState.ItemCreationApi.Get(itemType);
            FireWeaponPropreties weaponProperty = GameState.ItemCreationApi.GetWeapon(itemType);

            var entity = entitasContext.itemInventory.CreateEntity();
            entity.AddItemID(ItemID);
            entity.AddItemType(itemType);

            if (weaponProperty.HasClip())
                entity.AddItemFireWeaponClip(weaponProperty.ClipSize, weaponProperty.BulletsPerShot);

            if (weaponProperty.HasCharge())
                entity.AddItemFireWeaponCharge(weaponProperty.CanCharge, weaponProperty.ChargeMin, weaponProperty.ChargeRatio, weaponProperty.ChargePerShot, weaponProperty.ChargeMin,
                    weaponProperty.ChargeMax);

            if (weaponProperty.ShouldSpread())
                entity.AddItemFireWeaponSpread(weaponProperty.SpreadAngle);

            if (weaponProperty.IsPulse())
                entity.AddItemPulseWeaponPulse(weaponProperty.IsLaunchGreanade, weaponProperty.NumberOfGrenades);

            if (itemProperty.IsPlacementTool())
            {
                if (entity.itemType.Type == ItemType.PlacementTool || entity.itemType.Type == ItemType.PlacementMaterialTool)
                {
                    Enums.Tile.Data data = new Data();
                    entity.AddItemCastData(data, true);
                }

                if (entity.itemType.Type == ItemType.ConstructionTool ||
                    entity.itemType.Type == ItemType.RemoveMech ||
                    itemProperty.Group == ItemGroups.Mech)
                {
                    Mech.Data data = new Mech.Data();
                    entity.AddItemMechCastData(data, true);

                    if(itemProperty.MechType != Mech.MechType.Error)
                        entity.itemMechCastData.data.MechID = itemProperty.MechType;
                }

                if(entity.itemType.Type == ItemType.PotionTool)
                {
                    Enums.PotionType potionType = PotionType.Error;
                    entity.AddItemPotionCastData(potionType);
                }
            }

            ItemID++;
            return entity;
        }

        public ItemInventoryEntity SpawnInventoryItem(Contexts entitasContext, ItemParticleEntity itemParticleEntity)
        {
            var entity = SpawnInventoryItem(entitasContext, itemParticleEntity.itemType.Type);

            if (itemParticleEntity.hasItemLabel)
                entity.AddItemLabel(itemParticleEntity.itemLabel.ItemName);

            if (itemParticleEntity.hasItemStack)
                entity.AddItemStack(itemParticleEntity.itemStack.Count);

            if (itemParticleEntity.hasItemFireWeaponClip)
                entity.ReplaceItemFireWeaponClip(itemParticleEntity.itemFireWeaponClip.NumOfBullets, itemParticleEntity.itemFireWeaponClip.BulletsPerShot);

            if (itemParticleEntity.hasItemFireWeaponSpread)
                entity.ReplaceItemFireWeaponSpread(itemParticleEntity.itemFireWeaponSpread.SpreadAngle);

            itemParticleEntity.Destroy();

            return entity;
        }
    }
}

