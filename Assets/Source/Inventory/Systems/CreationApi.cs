﻿using System;
using System.Collections.Generic;
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
        bool Init = false;
        InventoryModel[] InventoryModels;
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
            InventoryModels = new InventoryModel[16];
            for (int i = 0; i < InventoryModels.Length; i++)
            {
                InventoryModels[i] = new InventoryModel();
                InitializeInventory(ref InventoryModels[i]);
            }
            const int MAX_SIZE_INVENTORY = 256;

            ActiveSlots = new BitSet(MAX_SIZE_INVENTORY);
            Restrictions = new Enums.ItemGroups[MAX_SIZE_INVENTORY];
            RestrictionSlotsTextures = new int[Enum.GetNames(typeof(Enums.ItemGroups)).Length];

            RestoreState();
        }

        public void Expand()
        {
            Array.Resize<InventoryModel>(ref InventoryModels, InventoryModels.Length * 2);
            for (int i = ArrayLenth; i < InventoryModels.Length; i++)
            {
                InventoryModels[i] = new InventoryModel();
                InitializeInventory(ref InventoryModels[i]);
            }
        }

        public void Initialize()
        {
            Init = true;
            CreateDefaultPlayerInventoryModel();
            CreateDefaultRestrictionInventoryModel();
            CreateDefaultChestInventoryModel();
            CreateDefaultCorpseInventoryModel();
        }

        public ref InventoryModel Get(int id)
        {
            if (id < 0 || id >= InventoryModels.Length)
            {
                throw new ArgumentOutOfRangeException();
                Utils.Assert(false);
            }
            
            return ref InventoryModels[id];
        }

        public int GetArrayLength()
        {
            return ArrayLenth;
        }

        public int Create()
        {
            ID = ArrayLenth;
            if (ArrayLenth++ >= InventoryModels.Length)
                Expand();
            return ID;
        }

        public void End()
        {
            InventoryModels[ID].ID = ID;
            ref Window window = ref InventoryModels[ID].MainWindow;
            int width = InventoryModels[ID].Width;
            int height = InventoryModels[ID].Height;

            window.GridSize = new Vec2f(width * window.TileSize, height * window.TileSize);

            if (InventoryModels[ID].HasToolBar)
            {
                // Set tool bar position at the upmost row of inventory.
                InventoryModels[ID].ToolBarWindow = window;
                InventoryModels[ID].ToolBarWindow.GridSize.Y = window.TileSize;
                InventoryModels[ID].ToolBarWindow.Size = InventoryModels[ID].ToolBarWindow.GridSize;
                InventoryModels[ID].ToolBarWindow.Position.Y = window.TileSize / 2f;
                InventoryModels[ID].ToolBarWindow.Position.X = (1920 - window.GridSize.X) / 2f; // Centralize toolbar.
                InventoryModels[ID].ToolBarWindow.GridPosition = InventoryModels[ID].ToolBarWindow.Position;
            }

            window.GridPosition = new Vec2f(window.Position.X + window.RightBorderOffSet, 
                window.Position.Y + window.DownBorderOffSet);
            window.Size = new Vec2f(window.GridSize.X + window.RightBorderOffSet + window.LeftBorderOffSet,
                window.GridSize.Y + window.UpBorderOffSet + window.LeftBorderOffSet);

            int length = width * height;
            InventoryModels[ID].Slots = new GridSlot[length];

            int RealSlotCount = 0;
            for (int i = 0; i < length; i++)
            {
                Enums.ItemGroups restriction = Restrictions[i];
                int slotBackGroundIcon = -1;
                if ((int)restriction >= 0)
                    slotBackGroundIcon = RestrictionSlotsTextures[(int)restriction];

                InventoryModels[ID].Slots[i] = new GridSlot
                {
                    SlotID = ActiveSlots[i] ? RealSlotCount++ : -1,
                    Restriction = restriction,
                    SlotBackgroundIcon = slotBackGroundIcon
                };
            }
            InventoryModels[ID].SlotCount = RealSlotCount;

            RestoreState();
        }

        private void CreateDefaultPlayerInventoryModel()
        {
            Create();
            SetInventoryPos(460f, 240f);
            SetSize(10, 5);
            SetAllSlotsAsActive();
            SetBackgroundColor(new Color(0.2f, 0.2f, 0.2f, 1.0f));
            SetSelectedtSlotColor(Color.yellow);
            SetDefaultSlotColor(Color.gray);
            SetToolBar();
            SetTileSize(100);
            SetSlotBorderOffset(10);
            SetSlotOffset(20);
            SetInventoryBoderOffset(0, 0, 30, 0);
            End();
        }

        private void CreateDefaultRestrictionInventoryModel()
        {
            Create();
            SetInventoryPos(1_670f, 240f);
            SetSize(2, 5);
            SetAllSlotsAsActive();
            SetDefaultRestrictionTexture();
            SetRestriction(0, Enums.ItemGroups.Dye);
            SetRestriction(2, Enums.ItemGroups.Dye);
            SetRestriction(4, Enums.ItemGroups.Dye);
            SetRestriction(6, Enums.ItemGroups.Dye);
            SetRestriction(8, Enums.ItemGroups.Dye);
            SetRestriction(1, Enums.ItemGroups.Helmet);
            SetRestriction(3, Enums.ItemGroups.Ring);
            SetRestriction(5, Enums.ItemGroups.Armour);
            SetRestriction(7, Enums.ItemGroups.Belt);
            SetRestriction(9, Enums.ItemGroups.Gloves);
            SetBackgroundColor(new Color(0.2f, 0.2f, 0.2f, 1.0f));
            SetSelectedtSlotColor(Color.yellow);
            SetDefaultSlotColor(Color.gray);
            SetTileSize(100);
            SetSlotBorderOffset(10);
            SetSlotOffset(20);
            End();
        }

        private void CreateDefaultChestInventoryModel()
        {
            Create();
            SetInventoryPos(460f, 240f);
            SetSize(10, 5);
            SetAllSlotsAsActive();
            SetBackgroundColor(new Color(0.2f, 0.2f, 0.2f, 1.0f));
            SetSelectedtSlotColor(Color.yellow);
            SetDefaultSlotColor(Color.gray);
            SetTileSize(100);
            SetSlotBorderOffset(10);
            SetSlotOffset(20);
            End();
        }

        private void CreateDefaultCorpseInventoryModel()
        {
            Create();
            SetInventoryPos(460f, 810f);
            SetSize(10, 2);
            SetAllSlotsAsActive();
            SetBackgroundColor(new Color(0.2f, 0.2f, 0.2f, 1.0f));
            SetSelectedtSlotColor(Color.yellow);
            SetDefaultSlotColor(Color.gray);
            SetTileSize(100);
            SetSlotOffset(20);
            SetSlotBorderOffset(10);
            SetInventoryBoderOffset(0, 0, 30, 0);
            End();
        }

        public int GetDefaultPlayerInventoryModelID()
        {
            if (!Init)
                Initialize();
            return 0;
        }
        public int GetDefaultRestrictionInventoryModelID()
        {
            if (!Init)
                Initialize();
            return 1;
        }

        public int GetDefaultChestInventoryModelID()
        {
            if (!Init)
                Initialize();
            return 2;
        }
        public int GetDefaultCorpseInventoryModelID()
        {
            if (!Init)
                Initialize();
            return 3;
        }

        public void SetBackgroundTexture(int spriteID)
        {
            InventoryModels[ID].RenderProprieties.BackGroundSpriteID = spriteID;
            InventoryModels[ID].RenderProprieties.InventoryFlags |= RenderProprieties.Flags.HasBackgroundTexture;
        }

        public void SetBackgroundColor(Color color)
        {
            InventoryModels[ID].RenderProprieties.BackgroundColor = color;
            InventoryModels[ID].RenderProprieties.InventoryFlags |= RenderProprieties.Flags.HasBackground;
        }

        public void SetSelectedtSlotColor(Color whenSelected)
            => InventoryModels[ID].RenderProprieties.SelectedColor = whenSelected;

        public void SetDefaultSlotColor(Color defaultBorder) 
            => InventoryModels[ID].RenderProprieties.SlotColor = defaultBorder;

        public void SetToolBar()
            => InventoryModels[ID].HasToolBar = true;

        public void SetSlotTexture(int textureID)
        {
            InventoryModels[ID].RenderProprieties.InventoryFlags |= RenderProprieties.Flags.HasDefaultSlotTexture;
            InventoryModels[ID].RenderProprieties.DefaultSlotTextureID = textureID;
        }

        public void SetDefaultRestrictionTexture()
        {
            SetTextureRestriction(Enums.ItemGroups.Helmet, GameResources.HelmetSlotIcon);
            SetTextureRestriction(Enums.ItemGroups.Armour, GameResources.ArmourSlotIcon);
            SetTextureRestriction(Enums.ItemGroups.Gloves, GameResources.GlovesSlotIcon);
            SetTextureRestriction(Enums.ItemGroups.Ring, GameResources.RingSlotIcon);
            SetTextureRestriction(Enums.ItemGroups.Belt, GameResources.BeltSlotIcon);
            SetTextureRestriction(Enums.ItemGroups.Dye, GameResources.DyeSlotIcon);
        }

        public void SetTextureRestriction(Enums.ItemGroups itemGroup, int textureRestriction)
            => RestrictionSlotsTextures[(int)itemGroup] = textureRestriction;

        /// <summary>
        /// Distance from start of the background texture to grid.
        /// </summary>
        public void SetInventoryBoderOffset(float leftBorder, float rightBorder, float upBorder, float downBorder)
        {
            InventoryModels[ID].MainWindow.UpBorderOffSet = upBorder;
            InventoryModels[ID].MainWindow.DownBorderOffSet = downBorder;
            InventoryModels[ID].MainWindow.LeftBorderOffSet = leftBorder;
            InventoryModels[ID].MainWindow.RightBorderOffSet = rightBorder;
        }

        public void SetSize(int width, int height) => SetSize(new Vec2i(width, height));
        public void SetSize(Vec2i size)
        {
            InventoryModels[ID].Width = size.X;
            InventoryModels[ID].Height = size.Y;
            InventoryModels[ID].Slots = new GridSlot[size.X * size.Y];

        }

        public void SetInventoryPos(Vec2f pos) => InventoryModels[ID].MainWindow.Position = pos;
        public void SetInventoryPos(float x, float y) => InventoryModels[ID].MainWindow.Position = new Vec2f(x,y);
        public void SetTileSize(float tileSize) => InventoryModels[ID].MainWindow.TileSize = tileSize;
        public void SetSlotBorderOffset(float slotBorderOffset)
        { 
            InventoryModels[ID].MainWindow.SlotBorderOffset = slotBorderOffset;
            InventoryModels[ID].RenderProprieties.InventoryFlags |= RenderProprieties.Flags.HasBorder;
        }
        public void SetSlotOffset(float slotOffset) => InventoryModels[ID].MainWindow.SlotOffset = slotOffset;

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