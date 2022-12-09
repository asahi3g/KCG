using Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

public class UIContentElementTileGeometryAndRotation : UIContentElement
{
    [Header(H_A + "Geometry And Rotation" + H_B)]
    [SerializeField] private TileGeometryAndRotation _tile;
    [SerializeField] private Image _icon;
    [SerializeField] private SpriteAtlas _spriteAtlas;
    [SerializeField] private GameObject _selection;

    public TileGeometryAndRotation GetTile() => _tile;
    
#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        UpdateLook();
    }
#endif
    
    protected override bool CanSelect()
    {
        if (App.Instance.GetPlayer().GetCurrentPlayerAgent(out AgentRenderer agentRenderer))
        {
            ItemInventoryEntity currentItem = agentRenderer.GetAgent().GetItem();
                
            if (currentItem != null)
            {
                return currentItem.hasItemTile;
            }
        }

        return false;
    }

    public override void SetIsSelected(bool isSelected)
    {
        base.SetIsSelected(isSelected);
        _selection.SetActive(isSelected);
    }
    
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        App.Instance.GetUI().GetView<UIViewItemHoverInfo>().SetInfo(this);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        App.Instance.GetUI().GetView<UIViewItemHoverInfo>().Clear();
    }

    public void SetTile(TileGeometryAndRotation value)
    {
        _tile = value;
        UpdateLook();
    }
    

    private void UpdateLook()
    {
        // Update sprite
        if (_spriteAtlas != null)
        {
            if (_icon != null)
            {
                Sprite sprite = _spriteAtlas.GetSprite($"tile_{_tile}");
                _icon.sprite = sprite;
                _icon.color = sprite == null ? Color.magenta : Color.white;
            }
        }
    }
}
