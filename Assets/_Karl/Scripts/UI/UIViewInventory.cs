using Inventory;
using UnityEngine;

public class UIViewInventory : UIView
{
    [SerializeField] private UIContent _content;
    [SerializeField] private UIContentSelection _selection;
    [SerializeField] private UIGroup _toggableInventoryGroup;

    private InventoryEntityComponent _inventoryEntityComponent;

    public UIContentSelection GetSelection() => _selection;
    public InventoryEntityComponent GetInventoryEntityComponent() => _inventoryEntityComponent;
    public UIGroup GetToggableInventoryGroup() => _toggableInventoryGroup;

    protected override void OnEnable()
    {
        base.OnEnable();
        _selection.onSelectWithPrevious.AddListener(OnSelectionSelect);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _selection.onSelectWithPrevious.RemoveListener(OnSelectionSelect);
    }
    
    protected override void OnGroupOpened()
    {
        
    }

    protected override void OnGroupClosed()
    {
        
    }

    public void SetInventoryEntityComponent(InventoryEntityComponent inventory)
    {
        _selection.ClearSelection();
        _inventoryEntityComponent = inventory;
        if (_inventoryEntityComponent == null) return;

        Slot[] slots = _inventoryEntityComponent.Slots;
        
        
        UIContentElementInventorySlot[] elements = _content.GetElements<UIContentElementInventorySlot>(true);
        int length = Mathf.Min(slots.Length, elements.Length);
        
        for (int i = 0; i < length; i++)
        {
            Slot slot = slots[i];
            UIContentElementInventorySlot entry = elements[i];
            entry.SetSlot(this, slot);
        }
    }
    
    public bool GetSlot(int index, out UIContentElementInventorySlot slot)
    {
        slot = null;
        UIContentElementInventorySlot[] slots = _content.GetElements<UIContentElementInventorySlot>(true);
        if (index >= 0 && index < slots.Length)
        {
            slot = slots[index];
        }
        return slot != null;
    }

    public void Clear()
    {
        _inventoryEntityComponent = null;
        _content.Clear();
        _selection.ClearSelection();
    }

    private void OnSelectionSelect(UIContentElement previous, UIContentElement selected)
    {
        ToggleUIViewTileGeometryAndRotation(false);
        ToggleUIViewMapLayerType(false);
        
        if (_inventoryEntityComponent == null) return;
        
        if (selected == null)
        {
            _inventoryEntityComponent.ClearSelectedSlotIndex();
            
        }
        else
        {
            UIContentElementInventorySlot impl = (UIContentElementInventorySlot)selected;
            
            // Update selected index
            _inventoryEntityComponent.SetSelectedSlotIndex(impl.GetSlot().Index);
            
            // Check if is tool (open geometry shapes if needed)
            if (impl.GetItem(out ItemInventoryEntity itemInventoryEntity))
            {
                if (itemInventoryEntity.hasItemTile)
                {
                    ToggleUIViewTileGeometryAndRotation(true);
                    ToggleUIViewMapLayerType(true);
                }
            }
        }

        void ToggleUIViewTileGeometryAndRotation(bool show)
        {
            App.Instance.GetUI().GetView<UIViewTileGeometryAndRotation>().GetGroup().GetIdentifier().Alter(GetIdentifier(), show);
        }

        void ToggleUIViewMapLayerType(bool show)
        {
            App.Instance.GetUI().GetView<UIViewMapLayerType>().GetGroup().GetIdentifier().Alter(GetIdentifier(), show);
        }
    }

}