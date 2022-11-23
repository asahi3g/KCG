using KMath;
using Enums;

namespace Mech
{
    public class MechSpawnSystem
    {
        static private int UniqueID;
        public MechEntity Spawn(Vec2f position, MechType mechType)
        {
            ref MechProperties mechProperties = ref GameState.MechCreationApi.GetRef(mechType);
            var spriteSize = mechProperties.SpriteSize;
            var spriteId = mechProperties.SpriteID;

            ref var planet = ref GameState.Planet;
            var entity = planet.EntitasContext.mech.CreateEntity();
            entity.AddMechID(UniqueID++, -1);
            entity.AddMechSprite2D(spriteId, spriteSize);
            entity.AddMechPositionLimits(mechProperties.XMin, mechProperties.XMax, mechProperties.YMin, mechProperties.YMax);
            entity.AddMechPosition2D(position);
            entity.AddMechType(mechType);

            if (mechProperties.Group == MechGroup.Plant)
                entity.AddMechPlant(0f, 100f, 0f);

            if (mechType == MechType.Planter)
                entity.AddMechPlanter(false, -1);

            if (mechProperties.HasInventory())
                entity.AddMechInventory(GameState.InventoryManager.CreateInventory(mechProperties.InventoryEntityTemplateID, InventoryEntityType.Default, "Chest").inventoryID.ID);

            if (mechProperties.IsBreakable())
                entity.AddMechDurability(mechProperties.Durability);

            if (mechType == MechType.CraftingTable)
            {
                entity.AddMechCraftingTable(planet.AddInventory(GameState.InventoryCreationApi.GetDefaultCraftingBenchInputInventoryModelID(), "Input"),
                    planet.AddInventory(GameState.InventoryCreationApi.GetDefaultCraftingBenchOutputInventoryModelID(), "Out"));
            }

            if (mechType == MechType.Tree)
            {
                entity.AddMechStatus(mechProperties.TreeHealth, mechProperties.TreeSize);
            }

            return entity;
        }

    }
}