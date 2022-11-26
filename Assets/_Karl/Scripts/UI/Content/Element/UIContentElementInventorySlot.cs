using Inventory;
using Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIContentElementInventorySlot : UIContentElement
{
    [SerializeField] private RawImage _icon;
    [SerializeField] private GameObject _selection;
    [SerializeField] private GameObject _quantity;
    [SerializeField] private TMP_Text _quantityText;

    private UIViewInventory _inventory;
    private Slot _slot;

    public Slot GetSlot() => _slot;

    protected override bool CanSelect()
    {
        return true;
    }
    
    public override void SetIsSelected(bool isSelected)
    {
        base.SetIsSelected(isSelected);
        _selection.SetActive(isSelected);
    }

    public void SetSlot(UIViewInventory inventory, Slot slot)
    {
        _inventory = inventory;
        _slot = slot;
        UpdateLook();
    }

    public bool GetItem(out ItemInventoryEntity itemInventoryEntity)
    {
        GameState.Planet.GetItemInventoryEntity(_slot, out itemInventoryEntity);
        return itemInventoryEntity != null;
    }

    private void UpdateLook()
    {
        if (GetItem(out ItemInventoryEntity itemInventoryEntity))
        {
            ItemProperties itemProprieties = GameState.ItemCreationApi.Get(itemInventoryEntity.itemType.Type);
            Sprites.Sprite sprite = GameState.SpriteAtlasManager.GetSprite(itemProprieties.InventorSpriteID, Enums.AtlasType.Particle);

            if (SetIcon(sprite.Texture))
            {
                _icon.SetUvRect(sprite.TextureCoords);
            }
            else
            {
                Debug.LogWarning("Item texture does not exist");
            }

            if (itemInventoryEntity.hasItemStack)
            {
                int quantity = itemInventoryEntity.itemStack.Count;
                SetQuantity(quantity);
            }
            else
            {
                SetQuantity(1);
            }
        }
        else
        {
            ClearLook();
        }
    }
    
    public void ClearLook()
    {
        SetIcon(null);
        SetQuantity(-1);
    }
    
    private bool SetIcon(Texture2D texture)
    {
        _icon.texture = texture;
        bool success = _icon.texture != null;
        _icon.enabled = success;
        return success;
    }

    private void SetQuantity(int quantity)
    {
        _quantity.SetActive(quantity > 1);
        _quantityText.SetText(quantity.ToString());
    }
}
