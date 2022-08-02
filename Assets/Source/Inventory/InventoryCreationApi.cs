using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryCreationApi
    {
        private InventoryProprieties[] ProprietiesArray;
        int InventorySize = 0;
        int InventoryID = -1;

        public InventoryCreationApi()
        {
            ProprietiesArray = new InventoryProprieties[16];
            for (int i = 0; i < ProprietiesArray.Length; i++)
                ProprietiesArray[i] = new InventoryProprieties();

            CreateDefaultInventory();
        }

        public void Expand()
        {
            Array.Resize<InventoryProprieties>(ref ProprietiesArray, ProprietiesArray.Length * 2);
            for (int i = InventorySize; i < ProprietiesArray.Length; i++)
                ProprietiesArray[i] = new InventoryProprieties();
            InventorySize = ProprietiesArray.Length;
        }

        public InventoryProprieties Get(int id)
        {
            if (id < 0 || id >= ProprietiesArray.Length)
            {
                throw new ArgumentOutOfRangeException();
                Utils.Assert(false);
            }
            
            return ProprietiesArray[id];
        }


        public void Create()
        {
            InventoryID = InventorySize;
            if (InventorySize++ >= ProprietiesArray.Length)
                Expand();
        }

        public void End() => InventoryID = -1;

        private void CreateDefaultInventory()
        {
            Create();
            SetBackgroundColor(Color.black);
            SetSelectedtSlotColor(Color.yellow);
            SetDefaultSlotColor(Color.gray);
            SetToolBar();
            SetTileSize(54);
            SetBorderOffset(5);
            SetSlotOffset(10);
            End();
        }

        public int GetDefaultInventory() => 0;

        public void SetBackgroundTexture(int spriteID)
        {
            ProprietiesArray[InventoryID].BackGroundSpriteID = spriteID;
            ProprietiesArray[InventoryID].InventoryFlags |= InventoryProprieties.Flags.HasBackgroundTexture;
        }

        public void SetBackgroundColor(Color color)
        {
            ProprietiesArray[InventoryID].BackgroundColor = color;
            ProprietiesArray[InventoryID].InventoryFlags |= InventoryProprieties.Flags.HasBackground;
        }

        public void SetSelectedtSlotColor(Color whenSelected)
        {
            ProprietiesArray[InventoryID].SelectedColor = whenSelected;
        }

        public void SetDefaultSlotColor(Color defaultBorder)
        {
            ProprietiesArray[InventoryID].SlotColor = defaultBorder;
            ProprietiesArray[InventoryID].InventoryFlags |= InventoryProprieties.Flags.HasBorder;
        }

        public void SetToolBar()
        {
            ProprietiesArray[InventoryID].InventoryFlags |= InventoryProprieties.Flags.HasTooBar;
        }

        public void SetTileSize(float width) =>
            ProprietiesArray[InventoryID].TileSize = width;
        public void SetBorderOffset(float borderOffset)
            => ProprietiesArray[InventoryID].BorderOffset = borderOffset;
        public void SetSlotOffset(float SlotOffset)
            => ProprietiesArray[InventoryID].SlotOffset = SlotOffset;
    }
}
