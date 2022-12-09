using Item;
using Item.FireWeapon;
using Item.PulseWeapon;
using UnityEngine;

public class UIViewItemHoverInfo : UIView
{
    [SerializeField] private UIContent _content;
    [SerializeField] private RectTransform _pivot;
    [SerializeField] private GameObject _panel;

    private UIContentElement _current;



    private void Update()
    {
        UpdatePivot();
    }

    private void UpdatePivot()
    {
        if (_current == null) return;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle (rectTransform, Input.mousePosition, App.Instance.GetUI().GetCamera(), out Vector2 localPoint))
        {
            _pivot.anchoredPosition = localPoint;
        }
    }
    
    protected override void OnGroupOpened()
    {
        
    }

    protected override void OnGroupClosed()
    {
        
    }

    public void SetInfo(UIContentElementInventorySlot slot)
    {
        if (slot == null)
        {
            Clear();
            return;
        }
        if (slot.GetItem(out ItemInventoryEntity itemInventoryEntity))
        {
            
            SetInfo(slot, itemInventoryEntity);
        }
        else
        {
            Clear();
        }
    }

    private void SetInfo(UIContentElementInventorySlot slot, ItemInventoryEntity item)
    {
        Clear();
        _current = slot;
        if (_current == null) return;

        ItemProperties itemProperties = GameState.ItemCreationApi.GetItemProperties(item.itemType.Type);

        // Name + (Optional) Quantity
        _content.Create<UIContentElementItemInfoEntry>().SetInfo(itemProperties.ItemLabel, itemProperties.Group.Color(Color.gray));

        // Empty space
        _content.Create<UIContentElementItemInfoEntry>().SetInfo(null, null);

        if (item.hasItemType)
        {
            TypeComponent type = item.itemType;
            _content.Create<UIContentElementItemInfoEntry>().SetInfo("------Type------", null);
            _content.Create<UIContentElementItemInfoEntry>().SetInfo(nameof(type.Type), type.Type.ToStringPretty());
        }

        if (item.hasItemID)
        {
            IDComponent id = item.itemID;
            _content.Create<UIContentElementItemInfoEntry>().SetInfo("------ItemID------", null);
            _content.Create<UIContentElementItemInfoEntry>().SetInfo(nameof(id.ID), id.ID.ToStringPretty());
            _content.Create<UIContentElementItemInfoEntry>().SetInfo(nameof(id.Index), id.Index.ToStringPretty());
            _content.Create<UIContentElementItemInfoEntry>().SetInfo(nameof(id.ItemName), id.ItemName.ToStringPretty());
        }

        if (item.hasItemStack)
        {
            StackComponent stack = item.itemStack;
            
            _content.Create<UIContentElementItemInfoEntry>().SetInfo("------Stack------", null);
            _content.Create<UIContentElementItemInfoEntry>().SetInfo(nameof(stack.Count), stack.Count.ToStringPretty());
        }

        
        if (item.hasItemFireWeaponClip)
        {
            ClipComponent clip = item.itemFireWeaponClip;
            
            _content.Create<UIContentElementItemInfoEntry>().SetInfo("----Weapon Clip----", null);
            _content.Create<UIContentElementItemInfoEntry>().SetInfo(nameof(clip.NumOfBullets), clip.NumOfBullets.ToStringPretty());
        }

        if (item.hasItemFireWeaponChargedWeapon)
        {
            ChargedWeaponComponent weapon = item.itemFireWeaponChargedWeapon;
            
            _content.Create<UIContentElementItemInfoEntry>().SetInfo("---Charged Weapon---", null);
            _content.Create<UIContentElementItemInfoEntry>().SetInfo(nameof(weapon.CanCharge), weapon.CanCharge.ToStringPretty());
            _content.Create<UIContentElementItemInfoEntry>().SetInfo(nameof(weapon.ChargeMin), weapon.ChargeMin.ToStringPretty());
            _content.Create<UIContentElementItemInfoEntry>().SetInfo(nameof(weapon.ChargeMax), weapon.ChargeMax.ToStringPretty());
            _content.Create<UIContentElementItemInfoEntry>().SetInfo(nameof(weapon.ChargeRate), weapon.ChargeRate.ToStringPretty());
            _content.Create<UIContentElementItemInfoEntry>().SetInfo(nameof(weapon.ChargeRatio), weapon.ChargeRatio.ToStringPretty());
            _content.Create<UIContentElementItemInfoEntry>().SetInfo(nameof(weapon.ChargePerShot), weapon.ChargePerShot.ToStringPretty());
        }

        if (item.hasItemPulseWeaponPulse)
        {
            PulseComponent pulse = item.itemPulseWeaponPulse;
            
            _content.Create<UIContentElementItemInfoEntry>().SetInfo("---Pulse Weapon---", null);
            _content.Create<UIContentElementItemInfoEntry>().SetInfo(nameof(pulse.GrenadeMode), pulse.GrenadeMode.ToStringPretty());
            _content.Create<UIContentElementItemInfoEntry>().SetInfo(nameof(pulse.NumberOfGrenades), pulse.NumberOfGrenades.ToStringPretty());
        }

        if (item.hasItemPotion)
        {
            PotionComponent potion = item.itemPotion;
            
            _content.Create<UIContentElementItemInfoEntry>().SetInfo("------Potion------", null);
            _content.Create<UIContentElementItemInfoEntry>().SetInfo(nameof(potion.potionType), potion.potionType.ToStringPretty());
        }

        if (item.hasItemTile)
        {
            TileComponent tile = item.itemTile;
            
            _content.Create<UIContentElementItemInfoEntry>().SetInfo("------Tile------", null);
            _content.Create<UIContentElementItemInfoEntry>().SetInfo(nameof(tile.TileID), tile.TileID.ToStringPretty());
            _content.Create<UIContentElementItemInfoEntry>().SetInfo(nameof(tile.Layer), tile.Layer.ToStringPretty());
            _content.Create<UIContentElementItemInfoEntry>().SetInfo(nameof(tile.InputsActive), tile.InputsActive.ToStringPretty());
        }

        if (item.hasItemMechPlacement)
        {
            MechPlacementComponent mech = item.itemMechPlacement;
            
            _content.Create<UIContentElementItemInfoEntry>().SetInfo("------Mech------", null);
            _content.Create<UIContentElementItemInfoEntry>().SetInfo(nameof(mech.MechID), mech.MechID.ToStringPretty());
            _content.Create<UIContentElementItemInfoEntry>().SetInfo(nameof(mech.InputsActive), mech.InputsActive.ToStringPretty());
        }

        if (item.hasItemInventory)
        {
            InventoryComponent inventory = item.itemInventory;
            
            _content.Create<UIContentElementItemInfoEntry>().SetInfo("----Inventory----", null);
            _content.Create<UIContentElementItemInfoEntry>().SetInfo(nameof(inventory.InventoryID), inventory.InventoryID.ToStringPretty());
            _content.Create<UIContentElementItemInfoEntry>().SetInfo(nameof(inventory.SlotID), inventory.SlotID.ToStringPretty());
        }

        GetGroup().GetIdentifier().Alter(GetIdentifier(), true);
        _panel.SetActive(true);
    }

    public void SetInfo(UIContentElementTileGeometryAndRotation item)
    {
        Clear();
        _current = item;
        _content.Create<UIContentElementItemInfoEntry>().SetInfo(item.GetTile(), null);
        
        GetGroup().GetIdentifier().Alter(GetIdentifier(), true);
        _panel.SetActive(true);
    }
    
    public void SetInfo(UIContentElementMapLayerType item)
    {
        Clear();
        _current = item;
        _content.Create<UIContentElementItemInfoEntry>().SetInfo(item.GetLayer(), null);
        
        GetGroup().GetIdentifier().Alter(GetIdentifier(), true);
        _panel.SetActive(true);
    }

    public void Clear()
    {
        _panel.SetActive(false);
        _current = null;
        _content.Clear();
        GetGroup().GetIdentifier().Alter(GetIdentifier(), false);
    }

    public void Clear(UIContentElementInventorySlot slot)
    {
        if (_current == null) return;
        if (slot == null) return;
        if (slot.GetItem(out ItemInventoryEntity itemInventoryEntity))
        {
            Clear((UIContentElement)slot);
        }
    }
    
    public void Clear(UIContentElement element)
    {
        if (element == null) return;
        if (_current != element) return;
        Clear();
    }
}
