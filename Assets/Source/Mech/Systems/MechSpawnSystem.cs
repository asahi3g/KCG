using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using KMath;
using UnityEngine.UIElements;

namespace Mech
{
    public class MechSpawnSystem
    {
        static private int UniqueID;
        MechCreationApi MechCreationApi;

        public MechSpawnSystem(MechCreationApi mechCreationApi)
        {
            MechCreationApi = mechCreationApi;
        }
        
        public MechEntity Spawn(ref Planet.PlanetState planet, int spriteId, int width, int height, Vec2f position, MechType mechType)
        {
            var spriteSize = new Vec2f(width / 32f, height / 32f);

            ref MechProperties mechProperties = ref MechCreationApi.GetRef((int)mechType);

            var entity = planet.EntitasContext.mech.CreateEntity();
            entity.AddMechID(UniqueID++, -1);
            entity.AddMechSprite2D(spriteId, spriteSize);
            entity.AddMechPositionLimits(mechProperties.XMin, mechProperties.XMax, mechProperties.YMin, mechProperties.YMax);
            entity.AddMechPosition2D(position);
            entity.AddMechType(mechType);

            if (mechType == MechType.Planter)
                entity.AddMechPlanter(false, -1, 0.0f, 100.0f, 0.0f, 100.0f, 0);

            if (mechProperties.HasInventory())
                entity.AddMechInventory(GameState.InventoryManager.CreateInventory(planet.EntitasContext, mechProperties.InventoryModelID).inventoryID.ID);

            if (mechProperties.IsBreakable())
                entity.AddMechDurability(mechProperties.MechID);

            if (mechType == MechType.CraftingTable)
            {
                entity.AddMechCraftingTable(planet.AddInventory(GameState.InventoryCreationApi.GetDefaultCraftingBenchInputInventoryModelID(), "BenchInput"),
                    planet.AddInventory(GameState.InventoryCreationApi.GetDefaultCraftingBenchOutputInventoryModelID(), "BenchOutput"));
            }

            return entity;
        }

        public MechEntity Spawn(Contexts entitasContext, Vec2f position, MechType mechType)
        {
            ref MechProperties mechProperties = ref MechCreationApi.GetRef((int)mechType);

            var spriteSize = mechProperties.SpriteSize;

            var spriteId = mechProperties.SpriteID;

            var entity = entitasContext.mech.CreateEntity();
            entity.AddMechID(UniqueID++, -1);
            entity.AddMechSprite2D(spriteId, spriteSize);
            entity.AddMechPositionLimits(mechProperties.XMin, mechProperties.XMax, mechProperties.YMin, mechProperties.YMax);
            entity.AddMechPosition2D(position);
            entity.AddMechType(mechType);

            if (mechType == MechType.Planter)
                entity.AddMechPlanter(false, -1, 0.0f, 100.0f, 0.0f, 100.0f, 0);

            if (mechProperties.HasInventory())
                entity.AddMechInventory(GameState.InventoryManager.CreateInventory(entitasContext, mechProperties.InventoryModelID, "Chest").inventoryID.ID);

            if (mechProperties.IsBreakable())
                entity.AddMechDurability(mechProperties.MechID);

            return entity;
        }

        public MechEntity Spawn(ref Planet.PlanetState planet, Vec2f position, MechType mechType)
        {
            ref MechProperties mechProperties = ref MechCreationApi.GetRef((int)mechType);

            var spriteSize = mechProperties.SpriteSize;

            var spriteId = mechProperties.SpriteID;

            var entity = planet.EntitasContext.mech.CreateEntity();
            entity.AddMechID(UniqueID++, -1);
            entity.AddMechSprite2D(spriteId, spriteSize);
            entity.AddMechPositionLimits(mechProperties.XMin, mechProperties.XMax, mechProperties.YMin, mechProperties.YMax);
            entity.AddMechPosition2D(position);
            entity.AddMechType(mechType);

            if (mechType == MechType.Planter)
                entity.AddMechPlanter(false, -1, 0.0f, 100.0f, 0.0f, 100.0f, 0);

            if (mechProperties.HasInventory())
                entity.AddMechInventory(GameState.InventoryManager.CreateInventory(planet.EntitasContext, mechProperties.InventoryModelID, "Chest").inventoryID.ID);

            if (mechProperties.IsBreakable())
                entity.AddMechDurability(mechProperties.MechID);

            if (mechType == MechType.CraftingTable)
            {
                entity.AddMechCraftingTable(planet.AddInventory(GameState.InventoryCreationApi.GetDefaultCraftingBenchInputInventoryModelID(), "Input"),
                    planet.AddInventory(GameState.InventoryCreationApi.GetDefaultCraftingBenchOutputInventoryModelID(), "Out"));
            }

            return entity;
        }

    }
}