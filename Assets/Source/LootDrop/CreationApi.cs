using Enums;
using System;
using Utility;

namespace LootDrop
{
    public class CreationApi
    {
        public int CurrentID;
        LootDropEntry[] DropTableEntries;
        int Length;

        // Auxiliar data.
        LootDrop[] DropTable;
        int ItemDropTableLength;
        int CurrentEntry;

        public CreationApi()
        {
            DropTableEntries = new LootDropEntry[1024];
            DropTable = new LootDrop[Enum.GetNames(typeof(ItemType)).Length];

            ItemDropTableLength = 0;
            CurrentEntry = 0;
            Length = 0;

            Create();
            End();
        }

        public LootDropEntry Get(int id)
        { 
            return DropTableEntries[id];
        }

        public int Create()
        {
            CurrentID = Length++;
            return CurrentID;
        }

        public void AddItem(ItemType itemType, int numEntries)
        {
            DropTable[ItemDropTableLength].Type = itemType;
            DropTable[ItemDropTableLength].DropNum = new int[numEntries];
            DropTable[ItemDropTableLength].DropProbability = new int[numEntries];

            ItemDropTableLength++;
            CurrentEntry = 0;
        }

        public void SetEntry(int num, int probability)
        {
            if (CurrentEntry >= DropTable[ItemDropTableLength - 1].DropNum.Length)
                Utils.Assert(false, "More entries than specified were given for this drop item.");

            probability = CurrentEntry < 1 ? probability : DropTable[ItemDropTableLength - 1].DropProbability[CurrentEntry - 1] + probability;
            
            Utils.Assert(probability <= 100, "Compound probability bigger than 100.");

            DropTable[ItemDropTableLength - 1].DropNum[CurrentEntry] = num;
            DropTable[ItemDropTableLength - 1].DropProbability[CurrentEntry] = probability;
            CurrentEntry++;
        }

        public void End()
        {
            DropTableEntries[CurrentID].ItemDrops = new LootDrop[ItemDropTableLength];
            Array.Copy(DropTable, DropTableEntries[CurrentID].ItemDrops, ItemDropTableLength);
            ItemDropTableLength = 0;
            CurrentEntry = 0;
            CurrentID = 0;
        }

        public void InitializeResources()
        {
            GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Slime, 1);
            GameState.LootTableCreationAPI.AddItem(ItemType.Slime, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 30);
            GameState.LootTableCreationAPI.AddItem(ItemType.Food, 4);
            GameState.LootTableCreationAPI.SetEntry(1, 25);
            GameState.LootTableCreationAPI.SetEntry(2, 40);
            GameState.LootTableCreationAPI.SetEntry(3, 25);
            GameState.LootTableCreationAPI.SetEntry(4, 5);
            GameState.LootTableCreationAPI.AddItem(ItemType.Bone, 4);
            GameState.LootTableCreationAPI.SetEntry(3, 50);
            GameState.LootTableCreationAPI.SetEntry(4, 25);
            GameState.LootTableCreationAPI.SetEntry(5, 15);
            GameState.LootTableCreationAPI.SetEntry(6, 10);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Moon, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Dirt, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
        }
    }
}

