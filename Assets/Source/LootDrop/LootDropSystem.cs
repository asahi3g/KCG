using KMath;
using UnityEngine;

namespace LootDrop
{
    /// <summary>
    /// If there is any entity in the DropEntities list. Create item particles and delete entity.
    /// Entities have 1 frame of life spam.
    /// </summary>
    public class LootDropSystem
    {
        public LootDropEntity[] DropEntities = new LootDropEntity[64];
        public int length = 0;

        public void Add(int tableID, Vec2f pos)
        {
            DropEntities[length].TableID = tableID;
            DropEntities[length].DropPos = pos;
            DropEntities[length++].InventoryID = -1;
        }

        public void Add(int tableID, int inventoryID)
        {
            DropEntities[length].TableID = tableID;
            DropEntities[length++].InventoryID = inventoryID;
        }

        public void Update(Contexts contexts)
        {
            while (length > 0)
            {
                LootDropEntity entity = DropEntities[--length];
                LootDropTable table = GameState.LootTableCreationAPI.Get(entity.TableID);


                int[] itemCountList = new int[table.ItemDrops.Length];

                for (int i = 0; i < table.ItemDrops.Length; i++)
                {
                    int probabilidade = UnityEngine.Random.Range(0, 100);
                    for (int j = 0; j < table.ItemDrops[i].DropProbability.Length; j++)
                    {
                        if (probabilidade <= table.ItemDrops[i].DropProbability[j])
                        {
                            itemCountList[i] = table.ItemDrops[i].DropNum[j];
                            break;
                        }
                    }
                }

                for (int i = 0; i < itemCountList.Length; i++)
                { 
                    if (entity.InventoryID < 0)
                    {
                        for(int j = 0; j < itemCountList[i]; j++)
                            GameState.ItemSpawnSystem.SpawnItemParticle(contexts, table.ItemDrops[i].Type, entity.DropPos);
                    }
                    else
                    {
                        for (int j = 0; j < itemCountList[i]; j++)
                            Admin.AdminAPI.AddItem(GameState.InventoryManager, entity.InventoryID, table.ItemDrops[i].Type, contexts);
                    }
                }
            }

            length = 0;
        }
    }
}
