using System;
using Utility;
using KMath;
using UnityEngine;

namespace Inventory
{
    public class CreationApi
    {
        InventoryModel[] Inventories;
        int ArrayLenth = 0;
        int ID = -1;

        BitSet ActiveSlots;
        Enums.ItemGroups[] Restrictions;
        int[] RestrictionSlotsTextures;       // One for each restriction.

        private void InitializeInventory(ref InventoryModel inventory)
        {
            inventory.RenderProprieties = new RenderProprieties();
            inventory.MainWindow = new Window();
            inventory.ToolBarWindow = new Window();
        }

        private void RestoreState()
        {
            ID = -1;
            ActiveSlots.Clear();

            for (int i = 0; i < Restrictions.Length; i++)
                Restrictions[i] = Enums.ItemGroups.None;

            for (int i = 0; i < RestrictionSlotsTextures.Length; i++)
                RestrictionSlotsTextures[i] = -1;
        }

        public CreationApi()
        {
            Inventories = new InventoryModel[16];
            for (int i = 0; i < Inventories.Length; i++)
            {
                Inventories[i] = new InventoryModel();
                InitializeInventory(ref Inventories[i]);
            }
            const int MAX_SIZE_INVENTORY = 256;

            ActiveSlots = new BitSet(MAX_SIZE_INVENTORY);
            Restrictions = new Enums.ItemGroups[MAX_SIZE_INVENTORY];
            RestrictionSlotsTextures = new int[Enum.GetNames(typeof(Enums.ItemGroups)).Length - 1];

            RestoreState();
        }

        public void Expand()
        {
            Array.Resize<InventoryModel>(ref Inventories, Inventories.Length * 2);
            for (int i = ArrayLenth; i < Inventories.Length; i++)
            {
                Inventories[i] = new InventoryModel();
                InitializeInventory(ref Inventories[i]);
            }
        }

        public ref InventoryModel Get(int id)
        {
            if (id < 0 || id >= Inventories.Length)
            {
                throw new ArgumentOutOfRangeException();
                Utils.Assert(false);
            }
            
            return ref Inventories[id];
        }

        public int GetArrayLength()
        {
            return ArrayLenth;
        }

        public int Create()
        {
            ID = ArrayLenth;
            if (ArrayLenth++ >= Inventories.Length)
                Expand();
            return ID;
        }

        public void End()
        {
            Inventories[ID].ID = ID;
            ref Window window = ref Inventories[ID].MainWindow;
            int width = Inventories[ID].Width;
            int height = Inventories[ID].Height;

            window.Size = new Vec2f(width * window.TileSize, height * window.TileSize);

            window.X -= window.W / 2f;
            window.Y -= window.H / 2f;

            if (Inventories[ID].HasTooBar())
            {
                Inventories[ID].ToolBarWindow   = window;
                Inventories[ID].ToolBarWindow.Y = window.TileSize / 2f;
                Inventories[ID].ToolBarWindow.H = window.TileSize;
            }

            int length = width * height;
            Inventories[ID].SlotsMask = new BitSet((uint)length);
            Inventories[ID].Slots = new Slot[length];

            for (int i = 0; i < length; i++)
            {
                Enums.ItemGroups restriction = Restrictions[i];
                bool isOn = ActiveSlots[i];
                int slotBackGroundIcon = -1;
                if ((int)restriction >= 0)
                    slotBackGroundIcon = RestrictionSlotsTextures[(int)restriction];

                Inventories[ID].Slots[i] = new Slot
                {
                    ID = i,
                    IsOn = isOn,
                    ItemID = -1,
                    Restriction = restriction,
                    SlotBackgroundIcon = slotBackGroundIcon
                };
            }

            RestoreState();
        }

        public int CreateDefaultInventory()
        {
            int ID = Create();
            SetInventoryPos(960f, 540f);
            SetSize(10, 6);
            SetAllSlotsAsActive();
            SetBackgroundColor(new Color(0.2f, 0.2f, 0.2f, 1.0f));
            SetSelectedtSlotColor(Color.yellow);
            SetDefaultSlotColor(Color.gray);
            SetBorder();
            SetToolBar();
            SetTileSize(100);
            SetBorderOffset(10);
            SetSlotOffset(20);
            End();
            return ID;
        }

        public int CreateDefaultRestrictionInventory()
        {
            int ID = Create();
            SetInventoryPos(1_795f, 540f);
            SetSize(2, 5);
            SetAllSlotsAsActive();
            SetDefaultRestrictionTexture();
            SetBackgroundColor(new Color(0.2f, 0.2f, 0.2f, 1.0f));
            SetSelectedtSlotColor(Color.yellow);
            SetDefaultSlotColor(Color.gray);
            SetBorder();
            SetTileSize(100);
            SetBorderOffset(10);
            SetSlotOffset(20);
            End();
            return ID;
        }

        public void SetBorder() 
            => Inventories[ID].RenderProprieties.InventoryFlags |= RenderProprieties.Flags.HasBorder;

        public void SetBackgroundTexture(int spriteID)
        {
            Inventories[ID].RenderProprieties.BackGroundSpriteID = spriteID;
            Inventories[ID].RenderProprieties.InventoryFlags |= RenderProprieties.Flags.HasBackgroundTexture;
        }

        public void SetBackgroundColor(Color color)
        {
            Inventories[ID].RenderProprieties.BackgroundColor = color;
            Inventories[ID].RenderProprieties.InventoryFlags |= RenderProprieties.Flags.HasBackground;
        }

        public void SetSelectedtSlotColor(Color whenSelected)
            => Inventories[ID].RenderProprieties.SelectedColor = whenSelected;

        public void SetDefaultSlotColor(Color defaultBorder) 
            => Inventories[ID].RenderProprieties.SlotColor = defaultBorder;

        public void SetToolBar()
            => Inventories[ID].InventoryFlags |= InventoryModel.Flags.HasToolBar;

        public void SetSlotTexture(int textureID)
        {
            Inventories[ID].RenderProprieties.InventoryFlags |= RenderProprieties.Flags.HasDefaultSlotTexture;
            Inventories[ID].RenderProprieties.DefaultSlotTextureID = textureID;
        }

        public void SetDefaultRestrictionTexture()
        {
            SetTextureRestriction(Enums.ItemGroups.HELMET, 0);
            SetTextureRestriction(Enums.ItemGroups.ARMOUR, 1);
            SetTextureRestriction(Enums.ItemGroups.GLOVES, 2);
            SetTextureRestriction(Enums.ItemGroups.RING, 3);
            SetTextureRestriction(Enums.ItemGroups.BELT, 4);
            SetTextureRestriction(Enums.ItemGroups.DYE, 5);
        }

        public void SetTextureRestriction(Enums.ItemGroups itemGroup, int textureRestriction)
            => RestrictionSlotsTextures[(int)itemGroup] = textureRestriction;

        public void SetSize(int width, int height) => SetSize(new Vec2i(width, height));
        public void SetSize(Vec2i size)
        {
            Inventories[ID].Width = size.X;
            Inventories[ID].Height = size.Y;
            Inventories[ID].SlotsMask = new BitSet((uint)(size.X * size.Y));
            Inventories[ID].Slots = new Slot[size.X * size.Y];

        }

        public void SetInventoryPos(Vec2f pos) => Inventories[ID].MainWindow.Position = pos;
        public void SetInventoryPos(float x, float y) => Inventories[ID].MainWindow.Position = new Vec2f(x,y);
        public void SetTileSize(float tileSize) => Inventories[ID].MainWindow.TileSize = tileSize;
        public void SetBorderOffset(float slotBorderOffset) => Inventories[ID].MainWindow.SlotBorderOffset = slotBorderOffset;
        public void SetSlotOffset(float slotOffset) => Inventories[ID].MainWindow.SlotOffset = slotOffset;

        /// <summary>
        /// These function set which slots on the grid can hold items and will be draw.
        /// </summary>
        public void SetActiveSlot(int index)=> ActiveSlots.Set(index);

        public void SetActiveSlots(int startPos, int endPos)
        {
            for(int i = startPos; i <= endPos; i++)
                ActiveSlots.Set(i);
        }

        /// <summary>
        /// BitSet will define which slots are on and off.
        /// </summary>
        public void SetActiveSlots(BitSet bitSet) => ActiveSlots = bitSet;

        public void SetAllSlotsAsActive()
        {
            ActiveSlots.SetAll();
        }

        public void SetRestriction(int index, Enums.ItemGroups restriction) => Restrictions[index] = restriction;

        public void SetRestriction(int startPos, int endPos, Enums.ItemGroups restriction)
        {
            for (int i = startPos; i <= endPos; i++)
                Restrictions[i] = restriction;
        }

        public void SetRestriction(BitSet bitSet, Enums.ItemGroups restriction)
        {
            for (int i = 0; i < Restrictions.Length; i++)
            { 
                if (bitSet[i])
                    Restrictions[i] = restriction;
            }
        }
    }
}
