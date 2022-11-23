using UnityEngine;
using UnityEngine.UI;

public class UIContentElementInventory : UIContentElement
{
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _selection;
    
    protected override bool CanSelect()
    {
        return true;
    }
    
    public override void SetIsSelected(bool isSelected)
    {
        base.SetIsSelected(isSelected);
        _selection.SetActive(isSelected);
    }

    private void SetIcon(Sprite sprite)
    {
        _icon.sprite = sprite;
        _icon.enabled = _icon.sprite != null;
    }
}
