using System;
using Utility;
using KMath;
using UnityEngine;

namespace Inventory
{

    /// <summary>
    /// Exemple on how to use the api in default functions and in InventoryTest script.
    /// </summary>
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

            window.GridSize = new Vec2f(width * window.TileSize, height * window.TileSize);

            if (Inventories[ID].HasTooBar())
            {
                // Draw toolbar by default.
                Inventories[ID].InventoryFlags |= InventoryModel.Flags.DrawToolBar;

                // Set tool bar position at the upmost row of inventory.
                Inventories[ID].ToolBarWindow = window;
                Inventories[ID].ToolBarWindow.GridSize.Y = window.TileSize;
                Inventories[ID].ToolBarWindow.Size = Inventories[ID].ToolBarWindow.GridSize;
                Inventories[ID].ToolBarWindow.Position.Y = window.TileSize / 2f;
                Inventories[ID].ToolBarWindow.Position.X = (1920 - window.GridSize.X) / 2f; // Centralize toolbar.
                Inventories[ID].ToolBarWindow.GridPosition = Inventories[ID].ToolBarWindow.Position;
            }

            window.GridPosition = new Vec2f(window.Position.X + window.RightBorderOffSet, 
                window.Position.Y + window.DownBorderOffSet);
            window.Size = new Vec2f(window.GridSize.X + window.RightBorderOffSet + window.LeftBorderOffSet,
                window.GridSize.Y + window.UpBorderOffSet + window.LeftBorderOffSet);

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
            SetInventoryPos(460f, 240f);
            SetSize(10, 6);
            SetAllSlotsAsActive();
            SetBackgroundColor(new Color(0.2f, 0.2f, 0.2f, 1.0f));
            SetSelectedtSlotColor(Color.yellow);
            SetDefaultSlotColor(Color.gray);
            SetToolBar();
            SetTileSize(100);
            SetSlotBorderOffset(10);
            SetSlotOffset(20);
            End();
            return ID;
        }

        public int CreateDefaultRestrictionInventory()
        {
            int ID = Create();
            SetInventoryPos(1_670f, 240f);
            SetSize(2, 5);
            SetAllSlotsAsActive();
            SetDefaultRestrictionTexture();
            SetRestriction(0, Enums.ItemGroups.DYE);
            SetRestriction(2, Enums.ItemGroups.DYE);
            SetRestriction(4, Enums.ItemGroups.DYE);
            SetRestriction(6, Enums.ItemGroups.DYE);
            SetRestriction(8, Enums.ItemGroups.DYE);
            SetRestriction(1, Enums.ItemGroups.HELMET);
            SetRestriction(3, Enums.ItemGroups.RING);
            SetRestriction(4, Enums.ItemGroups.ARMOUR);
            SetRestriction(5, Enums.ItemGroups.BELT);
            SetRestriction(7, Enums.ItemGroups.GLOVES);
            SetBackgroundColor(new Color(0.2f, 0.2f, 0.2f, 1.0f));
            SetSelectedtSlotColor(Color.yellow);
            SetDefaultSlotColor(Color.gray);
            SetTileSize(100);
            SetSlotBorderOffset(10);
            SetSlotOffset(20);
            End();
            return ID;
        }

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
            SetTextureRestriction(Enums.ItemGroups.HELMET, GameResources.HelmetSlotIcon);
            SetTextureRestriction(Enums.ItemGroups.ARMOUR, GameResources.ArmourSlotIcon);
            SetTextureRestriction(Enums.ItemGroups.GLOVES, GameResources.GlovesSlotIcon);
            SetTextureRestriction(Enums.ItemGroups.RING, GameResources.RingSlotIcon);
            SetTextureRestriction(Enums.ItemGroups.BELT, GameResources.BeltSlotIcon);
            SetTextureRestriction(Enums.ItemGroups.DYE, GameResources.DyeSlotIcon);
        }

        public void SetTextureRestriction(Enums.ItemGroups itemGroup, int textureRestriction)
            => RestrictionSlotsTextures[(int)itemGroup] = textureRestriction;

        /// <summary>
        /// Distance from start of the background texture to grid.
        /// </summary>
        public void SetInventoryBoderOffset(float leftBorder, float rightBorder, float upBorder, float downBorder)
        {
            Inventories[ID].MainWindow.UpBorderOffSet = upBorder;
            Inventories[ID].MainWindow.DownBorderOffSet = downBorder;
            Inventories[ID].MainWindow.LeftBorderOffSet = leftBorder;
            Inventories[ID].MainWindow.RightBorderOffSet = rightBorder;
        }

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
        public void SetSlotBorderOffset(float slotBorderOffset)
        { 
            Inventories[ID].MainWindow.SlotBorderOffset = slotBorderOffset;
            Inventories[ID].RenderProprieties.InventoryFlags |= RenderProprieties.Flags.HasBorder;
        }
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
