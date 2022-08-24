﻿using Enums;
using System;
using System.Collections.Generic;

namespace LootDrop
{
    public class CreationApi
    {
        public int CurrentID;
        LootDropEntry[] DropTableEntries;

        // Auxiliar data.
        LootDrop[] DropTable;
        int ItemDropTableLength;
        int CurrentEntry;

        public CreationApi()
        {
            DropTableEntries = new LootDropEntry[Enum.GetNames(typeof(Enums.LootTableType)).Length];
            DropTable = new LootDrop[Enum.GetNames(typeof(Enums.ItemType)).Length];

            ItemDropTableLength = 0;
            CurrentEntry = 0;
            CurrentID = (int)LootTableType.None;

            Create(LootTableType.None);
            End();
        }

        public LootDropEntry Get(LootTableType ID)
        { 
            return DropTableEntries[(int)ID];
        }

        public void Create(LootTableType type)
        {
            if (type == LootTableType.None)
            {
                
                return;
            }
            CurrentID = (int)type;
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
            CurrentID = (int)LootTableType.None;
        }
    }
}
