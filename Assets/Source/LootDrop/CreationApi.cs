using Enums;
using System;
using System.Collections.Generic;

namespace LootDrop
{
    public class CreationApi
    {
        public int Length;
        LootDropEntry[] DropTableEntries;
        public Dictionary<string, int> NameToID = new Dictionary<string, int>();

        // Auxiliar data.
        LootDrop[] DropTable;
        int ItemDropTableLength;
        int CurrentEntry;

        public CreationApi()
        {
            DropTableEntries = new LootDropEntry[256];
            NameToID.EnsureCapacity(256);
            DropTable = new LootDrop[Enum.GetNames(typeof(Enums.ItemType)).Length];

            ItemDropTableLength = 0;
            CurrentEntry = 0;
            Length = 0;
        }

        public LootDropEntry Get(int ID)
        { 
            return DropTableEntries[ID];
        }

        public LootDropEntry Get(string name)
        {
            return DropTableEntries[NameToID[name]];
        }

        public int GetID(string name)
        {
            return NameToID[name];
        }


        public void Create(string name)
        {
            if (Length >= DropTableEntries.Length)
                Expand();
            DropTableEntries[Length].ID = Length;
            Utils.Assert(NameToID.TryAdd(name, Length), "Failed to create Drop table with existing name");
        }

        public void Expand()
        {
            Array.Resize<LootDropEntry>(ref DropTableEntries, DropTableEntries.Length + 256);
            NameToID.EnsureCapacity(DropTableEntries.Length);
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
            DropTableEntries[Length].ItemDrops = new LootDrop[ItemDropTableLength];
            Array.Copy(DropTable, DropTableEntries[Length].ItemDrops, ItemDropTableLength);
            ItemDropTableLength = 0;
            CurrentEntry = 0;
            Length++;
        }
    }
}
