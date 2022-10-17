using Enums;
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
        }
    }
}
