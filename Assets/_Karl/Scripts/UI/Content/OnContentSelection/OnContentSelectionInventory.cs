using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnContentSelectionInventory : OnContentSelection
{

    protected override void OnContentSelectionEvent(UIContentElement previous, UIContentElement element)
    {
        OnContentSelectionInventoryEvent((UIContentElementInventorySlot)previous, (UIContentElementInventorySlot)element);
    }

    protected virtual void OnContentSelectionInventoryEvent(UIContentElementInventorySlot previous, UIContentElementInventorySlot element)
    {
        
    }
}
