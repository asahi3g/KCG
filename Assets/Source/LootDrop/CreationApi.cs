﻿using Enums;
using System;
using Utility;

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

        public void InitializeResources()
        {
            GameState.LootTableCreationAPI.Create(Enums.LootTableType.SlimeEnemyDrop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Slime, 1);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Slime, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 30);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Food, 4);
            GameState.LootTableCreationAPI.SetEntry(1, 25);
            GameState.LootTableCreationAPI.SetEntry(2, 40);
            GameState.LootTableCreationAPI.SetEntry(3, 25);
            GameState.LootTableCreationAPI.SetEntry(4, 5);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Bone, 4);
            GameState.LootTableCreationAPI.SetEntry(3, 50);
            GameState.LootTableCreationAPI.SetEntry(4, 25);
            GameState.LootTableCreationAPI.SetEntry(5, 15);
            GameState.LootTableCreationAPI.SetEntry(6, 10);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.ChestDrop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Chest, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.PlanterDrop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Planter, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.LightDrop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Light, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.SmashableBoxDrop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.SmashableBox, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.SmashableEggDrop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.SmashableEgg, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.MoonTileDrop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Moon, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.DirtTileDrop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Dirt, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.DirtTileDrop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Dirt, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.BedrockTileDrop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Bedrock, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.PipeTileDrop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Pipe, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.WireTileDrop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Wire, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.LapisBlock_0Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Lapis_0, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.LapisBlock_1Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Lapis_1, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.LapisBlock_2Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Lapis_2, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.LapisBlock_3Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Lapis_3, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.LapisBlock_4Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Lapis_4, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.LapisBlock_5Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Lapis_5, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.LapisBlock_6Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Lapis_6, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.LapisBlock_7Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Lapis_7, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.CoalBlock_0Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Coal_0, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.CoalBlock_1Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Coal_1, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.CoalBlock_2Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Coal_2, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.CoalBlock_3Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Coal_3, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.CoalBlock_4Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Coal_4, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.CoalBlock_5Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Coal_5, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.CoalBlock_6Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Coal_6, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.CoalBlock_7Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Coal_7, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.PinkDiaBlock_0Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.PinkDia_0, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.PinkDiaBlock_1Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.PinkDia_1, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.PinkDiaBlock_2Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.PinkDia_2, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.PinkDiaBlock_3Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.PinkDia_3, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.PinkDiaBlock_4Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.PinkDia_4, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.PinkDiaBlock_5Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.PinkDia_5, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.PinkDiaBlock_6Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.PinkDia_6, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.PinkDiaBlock_7Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.PinkDia_7, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.IronBlock_0Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Iron_0, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.IronBlock_1Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Iron_1, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.IronBlock_2Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Iron_2, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.IronBlock_3Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Iron_3, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.IronBlock_4Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Iron_4, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.IronBlock_5Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Iron_5, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.IronBlock_6Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Iron_6, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.IronBlock_7Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Iron_7, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.EmeraldBlock_0Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Emerald_0, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.EmeraldBlock_1Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Emerald_1, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.EmeraldBlock_2Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Emerald_2, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.EmeraldBlock_3Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Emerald_3, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.EmeraldBlock_4Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Emerald_4, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.EmeraldBlock_5Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Emerald_5, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.EmeraldBlock_6Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Emerald_6, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.EmeraldBlock_7Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Emerald_7, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.RedStoneBlock_0Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.RedStone_0, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.RedStoneBlock_1Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.RedStone_1, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.RedStoneBlock_2Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.RedStone_2, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.RedStoneBlock_3Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.RedStone_3, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.RedStoneBlock_4Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.RedStone_4, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.RedStoneBlock_5Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.RedStone_5, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.RedStoneBlock_6Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.RedStone_6, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.RedStoneBlock_7Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.RedStone_7, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.GoldBlock_0Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Gold_0, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.GoldBlock_1Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Gold_1, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.GoldBlock_2Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Gold_2, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.GoldBlock_3Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Gold_3, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.GoldBlock_4Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Gold_4, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.GoldBlock_5Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Gold_5, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.GoldBlock_6Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Gold_6, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.GoldBlock_7Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Gold_7, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.DiamondBlock_0Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Diamond_0, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.DiamondBlock_1Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Diamond_1, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.DiamondBlock_2Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Diamond_2, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.DiamondBlock_3Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Diamond_3, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.DiamondBlock_4Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Diamond_4, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.DiamondBlock_5Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Diamond_5, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.DiamondBlock_6Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Diamond_6, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();

            GameState.LootTableCreationAPI.Create(Enums.LootTableType.DiamondBlock_7Drop);
            GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Diamond_7, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
        }
    }
}
