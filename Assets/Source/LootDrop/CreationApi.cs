using Enums;
using System;
using System.Collections.Generic;

namespace LootDrop
{
    public class CreationApi
    {
        public int Length;
        LootDropTable[] DropTables;
        public Dictionary<string, int> NameToID = new Dictionary<string, int>();

        // Auxiliar data.
        LootDrop[] Drops;
        int ItemDropsLength;
        int CurrentEntry;

        public CreationApi()
        {
            DropTables = new LootDropTable[256];
            NameToID.EnsureCapacity(256);
            Drops = new LootDrop[Enum.GetNames(typeof(Enums.ItemType)).Length];

            ItemDropsLength = 0;
            CurrentEntry = 0;
            Length = 0;
        }

        public LootDropTable Get(int ID)
        { 
            return DropTables[ID];
        }

        public LootDropTable Get(string name)
        {
            return DropTables[NameToID[name]];
        }

        public int GetID(string name)
        {
            return NameToID[name];
        }


        public void Create(string name)
        {
            if (Length >= DropTables.Length)
                Expand();
            DropTables[Length].ID = Length;
            Utils.Assert(NameToID.TryAdd(name, Length), "Failed to create Drop table with existing name");
        }

        public void Expand()
        {
            Array.Resize<LootDropTable>(ref DropTables, DropTables.Length + 256);
            NameToID.EnsureCapacity(DropTables.Length);
        }

        public void AddItem(ItemType itemType, int numEntries)
        {
            Drops[ItemDropsLength].Type = itemType;
            Drops[ItemDropsLength].DropNum = new int[numEntries];
            Drops[ItemDropsLength].DropProbability = new int[numEntries];

            ItemDropsLength++;
            CurrentEntry = 0;
        }

        public void SetEntry(int num, int probability)
        {
            if (CurrentEntry >= Drops[ItemDropsLength - 1].DropNum.Length)
                Utils.Assert(false, "More entries than specified were given for this drop item.");

            probability = CurrentEntry < 1 ? probability : Drops[ItemDropsLength - 1].DropProbability[CurrentEntry - 1] + probability;
            
            Utils.Assert(probability <= 100, "Compound probability bigger than 100.");

            Drops[ItemDropsLength - 1].DropNum[CurrentEntry] = num;
            Drops[ItemDropsLength - 1].DropProbability[CurrentEntry] = probability;
            CurrentEntry++;
        }

        public void End()
        {
            DropTables[Length].ItemDrops = new LootDrop[ItemDropsLength];
            Array.Copy(Drops, DropTables[Length].ItemDrops, ItemDropsLength);
            ItemDropsLength = 0;
            CurrentEntry = 0;
            Length++;
        }
    }
}
