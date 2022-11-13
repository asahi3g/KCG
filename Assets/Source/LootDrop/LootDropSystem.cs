using KMath;

namespace LootDrop
{
    // If there is any entity in the DropEntities list. Create item particles and delete entity.
    // Entities have 1 frame of life spam.
    public class LootDropSystem
    {
        public LootDropEntity[] DropEntities = new LootDropEntity[64];
        public int length = 0;

        public void Add(int tableEntryID, Vec2f pos)
        {
            DropEntities[length].TableID = tableEntryID;
            DropEntities[length].DropPos = pos;
            DropEntities[length++].InventoryID = -1;
        }

        public void Add(int tableEntryID, int inventoryID)
        {
            DropEntities[length].TableID = tableEntryID;
            DropEntities[length++].InventoryID = inventoryID;
        }

        public void Update()
        {
            while (length > 0)
            {
                LootDropEntity entity = DropEntities[--length];
                LootDropEntry tableEntry = GameState.LootTableCreationAPI.Get(entity.TableID);


                int[] itemCountList = new int[tableEntry.ItemDrops.Length];

                for (int i = 0; i < tableEntry.ItemDrops.Length; i++)
                {
                    int probabilidade = UnityEngine.Random.Range(0, 100);
                    for (int j = 0; j < tableEntry.ItemDrops[i].DropProbability.Length; j++)
                    {
                        if (probabilidade <= tableEntry.ItemDrops[i].DropProbability[j])
                        {
                            itemCountList[i] = tableEntry.ItemDrops[i].DropNum[j];
                            break;
                        }
                    }
                }

                for (int i = 0; i < itemCountList.Length; i++)
                { 
                    if (entity.InventoryID < 0)
                    {
                        for(int j = 0; j < itemCountList[i]; j++)
                            GameState.Planet.AddItemParticle(tableEntry.ItemDrops[i].Type, entity.DropPos);
                    }
                    else
                    {
                        for (int j = 0; j < itemCountList[i]; j++)
                            Admin.AdminAPI.AddItem(GameState.InventoryManager, entity.InventoryID, tableEntry.ItemDrops[i].Type);
                    }
                }
            }

            length = 0;
        }
    }
}
