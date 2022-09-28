﻿using UnityEngine;
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

            Vec2f size = GameState.ItemCreationApi.Get(itemType).SpriteSize;

            var entity = entitasContext.itemParticle.CreateEntity();
            entity.AddItemID(ItemID);
            entity.AddItemType(itemType);
            entity.AddItemPhysicsState(position, position, Vec2f.Zero, Vec2f.Zero, false);
            entity.AddPhysicsBox2DCollider(size, Vec2f.Zero);

            if (weaponProperty.HasClip())
                entity.AddItemFireWeaponClip(weaponProperty.ClipSize);

            ItemID++;
            return entity;
        }

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

            if (itemInventoryEntity.hasItemFireWeaponClip)
            {
                entity.itemFireWeaponClip.NumOfBullets = itemInventoryEntity.itemFireWeaponClip.NumOfBullets;
            }

            itemInventoryEntity.Destroy();

            return entity;
        }

        public ItemInventoryEntity SpawnInventoryItem(Contexts entitasContext, ItemType itemType)
        {
            ItemProprieties itemProperty = GameState.ItemCreationApi.Get(itemType);

            var entity = entitasContext.itemInventory.CreateEntity();
            entity.AddItemID(ItemID);
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

                if(entity.itemType.Type == ItemType.PotionTool)
                {
                    Enums.PotionType potionType = PotionType.Error;
                    entity.AddItemPotion(potionType);
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

            itemParticleEntity.Destroy();

            return entity;
        }
    }
}

