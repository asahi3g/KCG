using Item;
using TMPro;
using UnityEngine;

public class OnContentSelectionInventoryItemInformation : OnContentSelectionInventory
{
    [Header(H_A + "Item Information" + H_B)]
    [SerializeField] private TMP_Text _tmp;
    
    protected override void OnContentSelectionInventoryEvent(UIContentElementInventorySlot previous, UIContentElementInventorySlot element)
    {
        base.OnContentSelectionInventoryEvent(previous, element);
        
        if (element == null)
        {
            ClearLook();
            AlterGroup(false);
            return;
        }

        if (element.GetItem(out ItemInventoryEntity itemInventoryEntity))
        {
            ItemProperties itemProperties = GameState.ItemCreationApi.Get(itemInventoryEntity.itemType.Type);

            _tmp.SetText($"{itemProperties.Group.ToStringPretty()} {itemProperties.ItemType.ToStringPretty()} {itemProperties.ItemLabel}");
        }
        else
        {
            _tmp.SetText("<None>");
        }

        AlterGroup(true);
    }

    private void ClearLook()
    {
        _tmp.Clear();
    }
    
}
