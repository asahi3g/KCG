using UnityEngine;
using UnityEngine.UI;

public class UIContentElementWeaponBullet : UIContentElement
{
    [SerializeField] private Image _image;
    
    protected override bool CanSelect()
    {
        return false;
    }

    public void SetVisibility(bool isVisible)
    {
        _image.enabled = isVisible;
    }
}
